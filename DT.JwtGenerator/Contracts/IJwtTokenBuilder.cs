using System.Security.Claims;

namespace DT.JwtGenerator
{
    /// <summary>
    /// Предоставляет fluent-интерфейс для пошаговой генерации JWT-токена.
    /// </summary>
    public interface IJwtTokenBuilder
    {
        /// <summary>
        /// Добавляет пользовательский claim к токену.
        /// </summary>
        /// <param name="type">Тип claim (например, "sub", "role", "email").</param>
        /// <param name="value">Значение claim.</param>
        /// <returns>Текущий экземпляр билдера для цепочки вызовов.</returns>
        IJwtTokenBuilder AddClaim(string type, object value);

        /// <summary>
        /// Добавляет коллекцию claims к токену.
        /// </summary>
        /// <param name="claims">Коллекция claims.</param>
        /// <returns>Текущий экземпляр билдера для цепочки вызовов.</returns>
        IJwtTokenBuilder AddClaims(IEnumerable<Claim> claims);

        /// <summary>
        /// Устанавливает издателя токена (issuer).
        /// </summary>
        /// <param name="issuer">Издатель токена.</param>
        /// <returns>Текущий экземпляр билдера для цепочки вызовов.</returns>
        IJwtTokenBuilder WithIssuer(string issuer);

        /// <summary>
        /// Устанавливает получателя токена (audience).
        /// </summary>
        /// <param name="audience">Получатель токена.</param>
        /// <returns>Текущий экземпляр билдера для цепочки вызовов.</returns>
        IJwtTokenBuilder WithAudience(string audience);

        /// <summary>
        /// Устанавливает время жизни токена относительно текущего момента.
        /// </summary>
        /// <param name="expiration">Время жизни токена.</param>
        /// <returns>Текущий экземпляр билдера для цепочки вызовов.</returns>
        IJwtTokenBuilder WithExpiration(TimeSpan expiration);

        /// <summary>
        /// Устанавливает точную дату и время истечения срока действия токена.
        /// </summary>
        /// <param name="expires">Дата и время истечения.</param>
        /// <returns>Текущий экземпляр билдера для цепочки вызовов.</returns>
        IJwtTokenBuilder WithExpiration(DateTime expires);

        /// <summary>
        /// Устанавливает время, до которого токен не должен быть принят (not before).
        /// </summary>
        /// <param name="notBeforeFromNow">Время от текущего момента.</param>
        /// <returns>Текущий экземпляр билдера для цепочки вызовов.</returns>
        IJwtTokenBuilder WithNotBefore(TimeSpan notBeforeFromNow);

        /// <summary>
        /// Устанавливает время выдачи токена.
        /// </summary>
        /// <param name="issuedAt">Время выдачи.</param>
        /// <returns>Текущий экземпляр билдера для цепочки вызовов.</returns>
        IJwtTokenBuilder WithIssuedAt(DateTime issuedAt);

        /// <summary>
        /// Устанавливает секретный ключ и алгоритм подписи для токена.
        /// </summary>
        /// <param name="secretKey">Секретный ключ (минимум 32 символа для HS256).</param>
        /// <param name="algorithm">Алгоритм подписи (по умолчанию HS256).</param>
        /// <returns>Текущий экземпляр билдера для цепочки вызовов.</returns>
        IJwtTokenBuilder WithSigningKey(string secretKey, string algorithm = "HS256");

        /// <summary>
        /// Генерирует и возвращает строковое представление JWT-токена.
        /// </summary>
        /// <returns>Сформированный JWT-токен в виде строки.</returns>
        /// <exception cref="InvalidOperationException">Если не задан ключ подписи.</exception>
        string Build();
    }
}
