using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DT.JwtGenerator
{
    /// <inheritdoc cref="IJwtTokenBuilder"/>
    internal class JwtTokenBuilder : IJwtTokenBuilder
    {
        private readonly JwtOptions _options;
        private readonly Dictionary<string, object> _claims = new();
        private DateTime? _expires;
        private DateTime? _notBefore;
        private DateTime? _issuedAt;
        private SigningCredentials? _signingCredentials;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="JwtTokenBuilder"/>.
        /// </summary>
        /// <param name="options">Настройки JWT.</param>
        /// <exception cref="ArgumentNullException">Если options равен null.</exception>
        public JwtTokenBuilder(JwtOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            InitializeDefaults();
        }

        public IJwtTokenBuilder AddClaim(string type, object value)
        {
            _claims[type] = value;
            return this;
        }

        public IJwtTokenBuilder AddClaims(IEnumerable<Claim> claims)
        {
            foreach (var claim in claims)
            {
                _claims[claim.Type] = claim.Value;
            }
            return this;
        }

        public IJwtTokenBuilder WithIssuer(string issuer)
        {
            _options.Issuer = issuer;
            return this;
        }

        public IJwtTokenBuilder WithAudience(string audience)
        {
            _options.Audience = audience;
            return this;
        }

        public IJwtTokenBuilder WithExpiration(TimeSpan expiration)
        {
            _expires = DateTime.UtcNow.Add(expiration);
            return this;
        }

        public IJwtTokenBuilder WithExpiration(DateTime expires)
        {
            _expires = expires;
            return this;
        }

        public IJwtTokenBuilder WithNotBefore(TimeSpan notBeforeFromNow)
        {
            _notBefore = DateTime.UtcNow.Add(notBeforeFromNow);
            return this;
        }

        public IJwtTokenBuilder WithIssuedAt(DateTime issuedAt)
        {
            _issuedAt = issuedAt;
            return this;
        }

        public IJwtTokenBuilder WithSigningKey(string secretKey, string algorithm = SecurityAlgorithms.HmacSha256)
        {
            if (string.IsNullOrWhiteSpace(secretKey))
                throw new ArgumentException("Секретный ключ не может быть пустым.", nameof(secretKey));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            _signingCredentials = new SigningCredentials(key, algorithm);
            return this;
        }

        public string Build()
        {
            if (_signingCredentials == null)
                throw new InvalidOperationException("Необходимо указать ключ подписи с помощью метода WithSigningKey().");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _options.Issuer,
                Audience = _options.Audience,
                Expires = _expires ?? DateTime.UtcNow.Add(_options.AccessTokenLifetime),
                NotBefore = _notBefore,
                IssuedAt = _issuedAt ?? DateTime.UtcNow,
                SigningCredentials = _signingCredentials
            };

            var claims = new List<Claim>();
            foreach (var kvp in _claims)
            {
                claims.Add(new Claim(kvp.Key, kvp.Value?.ToString() ?? string.Empty));
            }

            tokenDescriptor.Subject = new ClaimsIdentity(claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        /// <summary>
        /// Инициализация по умолчанию
        /// </summary>
        private void InitializeDefaults()
        {
            WithSigningKey(_options.SecretKey);
            WithIssuer(_options.Issuer);
            WithAudience(_options.Audience);
            WithExpiration(_options.AccessTokenLifetime);
        }
    }
}
