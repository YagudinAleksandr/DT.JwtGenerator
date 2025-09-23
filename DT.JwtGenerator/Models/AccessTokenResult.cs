namespace DT.JwtGenerator
{
    /// <summary>
    /// Результат генерации пары токенов: access и refresh.
    /// </summary>
    public class AccessTokenResult
    {
        /// <summary>
        /// Access-токен (JWT).
        /// </summary>
        public string AccessToken { get; set; } = string.Empty;

        /// <summary>
        /// Refresh-токен.
        /// </summary>
        public string RefreshToken { get; set; } = string.Empty;

        /// <summary>
        /// Время истечения срока действия access-токена.
        /// </summary>
        public DateTime AccessTokenExpiration { get; set; }

        /// <summary>
        /// Время истечения срока действия refresh-токена.
        /// </summary>
        public DateTime RefreshTokenExpiration { get; set; }
    }
}
