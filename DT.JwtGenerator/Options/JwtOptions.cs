namespace DT.JwtGenerator
{
    /// <summary>
    /// Настройки для генерации и валидации JWT-токенов.
    /// </summary>
    public class JwtOptions
    {
        /// <summary>
        /// Секретный ключ для подписи токенов (должен быть не менее 32 символов для HS256).
        /// </summary>
        public string SecretKey { get; set; } = string.Empty;

        /// <summary>
        /// Издатель токенов (issuer).
        /// </summary>
        public string Issuer { get; set; } = string.Empty;

        /// <summary>
        /// Получатель токенов (audience).
        /// </summary>
        public string Audience { get; set; } = string.Empty;

        /// <summary>
        /// Время жизни access-токена.
        /// </summary>
        public TimeSpan AccessTokenLifetime { get; set; } = TimeSpan.FromMinutes(15);

        /// <summary>
        /// Время жизни refresh-токена.
        /// </summary>
        public TimeSpan RefreshTokenLifetime { get; set; } = TimeSpan.FromDays(7);
    }
}
