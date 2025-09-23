using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DT.JwtGenerator
{
    /// <inheritdoc cref="IJwtTokenValidator"/>
    internal class JwtTokenValidator : IJwtTokenValidator
    {
        private readonly JwtOptions _options;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="JwtTokenValidator"/>.
        /// </summary>
        /// <param name="options">Настройки JWT.</param>
        /// <exception cref="ArgumentNullException">Если options равен null.</exception>
        public JwtTokenValidator(JwtOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        /// <inheritdoc/>
        public ClaimsPrincipal? ValidateToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_options.SecretKey);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = _options.Issuer,
                ValidateAudience = true,
                ValidAudience = _options.Audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                return tokenHandler.ValidateToken(token, validationParameters, out _);
            }
            catch
            {
                return null;
            }
        }

        /// <inheritdoc/>
        public bool TryValidateToken(string token, out ClaimsPrincipal? principal)
        {
            principal = ValidateToken(token);
            return principal != null;
        }
    }
}
