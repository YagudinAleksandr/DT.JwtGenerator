using System.Security.Claims;

namespace DT.JwtGenerator
{
    /// <summary>
    /// Предоставляет методы для валидации JWT-токенов.
    /// </summary>
    public interface IJwtTokenValidator
    {
        /// <summary>
        /// Проверяет валидность токена и возвращает его утверждения (claims).
        /// </summary>
        /// <param name="token">JWT-токен в виде строки.</param>
        /// <returns>Объект <see cref="ClaimsPrincipal"/> при успешной валидации, иначе null.</returns>
        ClaimsPrincipal? ValidateToken(string token);

        /// <summary>
        /// Безопасно проверяет валидность токена и возвращает результат через out-параметр.
        /// </summary>
        /// <param name="token">JWT-токен в виде строки.</param>
        /// <param name="principal">Результат валидации — утверждения токена.</param>
        /// <returns>True, если токен валиден; иначе false.</returns>
        bool TryValidateToken(string token, out ClaimsPrincipal? principal);
    }
}
