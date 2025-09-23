namespace DT.JwtGenerator
{
    /// <summary>
    /// Сервис для управления refresh-токенами: генерация, валидация и отзыв.
    /// </summary>
    public interface IRefreshTokenService
    {
        /// <summary>
        /// Генерирует новый refresh-токен.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя (опционально).</param>
        /// <returns>Сгенерированный refresh-токен в виде строки.</returns>
        Task<string> GenerateRefreshTokenAsync(string? userId = null);

        /// <summary>
        /// Проверяет валидность refresh-токена.
        /// </summary>
        /// <param name="token">Refresh-токен.</param>
        /// <returns>True, если токен действителен и не отозван; иначе false.</returns>
        Task<bool> ValidateRefreshTokenAsync(string token);

        /// <summary>
        /// Отзывает (деактивирует) refresh-токен.
        /// </summary>
        /// <param name="token">Refresh-токен для отзыва.</param>
        /// <returns>True, если отзыв выполнен успешно; иначе false.</returns>
        Task<bool> RevokeRefreshTokenAsync(string token);
    }
}
