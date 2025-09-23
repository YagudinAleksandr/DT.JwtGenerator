namespace DT.JwtGenerator
{
    /// <summary>
    /// Интерфейс хранилища refresh-токенов. Реализуется в инфраструктурном слое приложения.
    /// </summary>
    public interface IRefreshTokenStore
    {
        /// <summary>
        /// Получает refresh-токен по его значению.
        /// </summary>
        /// <param name="token">Значение refresh-токена.</param>
        /// <returns>Объект refresh-токена или null, если не найден.</returns>
        Task<RefreshToken?> GetByTokenAsync(string token);

        /// <summary>
        /// Сохраняет новый refresh-токен.
        /// </summary>
        /// <param name="token">Refresh-токен для сохранения.</param>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        Task AddAsync(RefreshToken token);

        /// <summary>
        /// Отзывает (деактивирует) refresh-токен.
        /// </summary>
        /// <param name="token">Значение refresh-токена для отзыва.</param>
        /// <returns>True, если отзыв выполнен успешно; иначе false.</returns>
        Task<bool> RevokeAsync(string token);

        /// <summary>
        /// Удаляет все просроченные refresh-токены из хранилища.
        /// </summary>
        /// <returns>Задача, представляющая асинхронную операцию очистки.</returns>
        Task RemoveExpiredAsync();
    }
}
