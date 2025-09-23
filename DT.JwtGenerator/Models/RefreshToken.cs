namespace DT.JwtGenerator
{
    /// <summary>
    /// Модель refresh-токена.
    /// </summary>
    public class RefreshToken
    {
        /// <summary>
        /// Уникальное значение токена.
        /// </summary>
        public string Token { get; set; } = Guid.NewGuid().ToString("N");

        /// <summary>
        /// Время создания токена.
        /// </summary>
        public DateTime Created { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Время истечения срока действия токена.
        /// </summary>
        public DateTime Expires { get; set; }

        /// <summary>
        /// Признак отзыва токена.
        /// </summary>
        public bool IsRevoked { get; set; }

        /// <summary>
        /// Идентификатор пользователя, которому принадлежит токен.
        /// </summary>
        public string? UserId { get; set; }
    }
}
