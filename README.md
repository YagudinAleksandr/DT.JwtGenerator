# DigitTwin.JwtGenerator

[![NuGet Version](https://img.shields.io/nuget/v/DT.JwtGenerator.svg?logo=nuget)](https://www.nuget.org/packages/DT.JwtGenerator)
[![NuGet Downloads](https://img.shields.io/nuget/dt/DT.JwtGenerator.svg)](https://www.nuget.org/packages/DT.JwtGenerator)
[![.NET](https://img.shields.io/badge/.NET-9%2B%20%7C%207%20%7C%208-blue?logo=.net)](https://dotnet.microsoft.com/)
[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)

## Описание
Fluent JWT Builder для .NET с поддержкой DDD и DI.

```bash
dotnet add package DigitTwin.JwtGenerator
```

### Настройка
Добавить в `appsettings.json`

```json
{
  "Jwt": {
    "SecretKey": "your_32_chars_min_secret_key_here_!",
    "Issuer": "https://yourapi.com",
    "Audience": "https://yourclient.com",
    "AccessTokenLifetime": "00:15:00",
    "RefreshTokenLifetime": "7.00:00:00"
  }
}
```

Зарегистрируйте в `Program.cs`

```csharp
using DigitTwin.JwtGenerator;

builder.Services.AddJwtServices(builder.Configuration);
```

### Использование
Генерация токена

```csharp
var token = _jwtTokenBuilder
    .AddClaim("sub", "123")
    .AddClaim("email", "user@example.com")
    .WithExpiration(TimeSpan.FromHours(1))
    .Build();
```

Вадидация токена

```cshsrp
if (_jwtTokenValidator.TryValidateToken(jwt, out var principal))
{
    var userId = principal.GetUserId();
    var email = principal.GetEmail();
}
```

Refresh-токены
```csharp
var refreshToken = await _refreshTokenService.GenerateRefreshTokenAsync("123");
var isValid = await _refreshTokenService.ValidateRefreshTokenAsync(refreshToken);
await _refreshTokenService.RevokeRefreshTokenAsync(refreshToken);
```

### Дополнительно
Реализуйте `IRefreshTokenStore` в инфраструктуре:

```csharp
public class EfCoreRefreshTokenStore : IRefreshTokenStore
{
    // Реализуйте методы: GetByTokenAsync, AddAsync, RevokeAsync, RemoveExpiredAsync
}
```

И зарегистрируйте:

```csharp
builder.Services.AddScoped<IRefreshTokenStore, EfCoreRefreshTokenStore>();
```