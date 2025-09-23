namespace DT.JwtGenerator
{
    /// <inheritdoc cref="IRefreshTokenService"/>
    internal class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokenStore _store;
        private readonly JwtOptions _options;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="RefreshTokenService"/>.
        /// </summary>
        /// <param name="store">Хранилище refresh-токенов.</param>
        /// <param name="options">Настройки JWT.</param>
        /// <exception cref="ArgumentNullException">Если store или options равны null.</exception>
        public RefreshTokenService(IRefreshTokenStore store, JwtOptions options)
        {
            _store = store ?? throw new ArgumentNullException(nameof(store));
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        /// <inheritdoc/>
        public async Task<string> GenerateRefreshTokenAsync(string? userId = null)
        {
            var token = new RefreshToken
            {
                UserId = userId,
                Expires = DateTime.UtcNow.Add(_options.RefreshTokenLifetime)
            };

            await _store.AddAsync(token);
            return token.Token;
        }

        /// <inheritdoc/>
        public async Task<bool> ValidateRefreshTokenAsync(string token)
        {
            if (string.IsNullOrWhiteSpace(token)) return false;

            var storedToken = await _store.GetByTokenAsync(token);
            if (storedToken == null) return false;
            if (storedToken.IsRevoked) return false;
            if (storedToken.Expires < DateTime.UtcNow) return false;

            return true;
        }

        /// <inheritdoc/>
        public async Task<bool> RevokeRefreshTokenAsync(string token)
        {
            return await _store.RevokeAsync(token);
        }
    }
}
