using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DT.JwtGenerator
{
    /// <summary>
    /// Методы расширения для регистрации JWT-сервисов в DI-контейнере.
    /// </summary>
    public static class JwtGeneratorExtensions
    {
        /// <summary>
        /// Регистрирует JWT-сервисы в контейнере зависимостей.
        /// Обратите внимание: интерфейс <see cref="IRefreshTokenStore"/> НЕ регистрируется здесь.
        /// Его реализацию необходимо зарегистрировать в инфраструктурном слое приложения.
        /// </summary>
        /// <param name="services">Коллекция сервисов.</param>
        /// <param name="configuration">Конфигурация приложения.</param>
        /// <returns>Обновлённая коллекция сервисов.</returns>
        /// <exception cref="ArgumentNullException">Если services или configuration равны null.</exception>
        public static IServiceCollection AddJwtServices(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            services.Configure<JwtOptions>(configuration.GetSection("Jwt"));

            services.AddScoped<IJwtTokenBuilder, JwtTokenBuilder>();
            services.AddScoped<IJwtTokenValidator, JwtTokenValidator>();
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();

            return services;
        }
    }
}
