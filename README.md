# UsersTasksBackend

Backend API для управления пользователями и их задачами (тестовое задание)

---

## 🛠️ Технологии

### Языки

* C#

### Инструменты и технологии

* .NET 10
* Docker (для развертывания в Render)
* ASP.NET Core Web API
* Entity Framework Core 10

---

## 📋 Требования

Перед запуском убедитесь, что установлено следующее:

* **.NET SDK**: версия 10.0 или выше
  [https://dotnet.microsoft.com/download/dotnet](https://dotnet.microsoft.com/download/dotnet)
* **Git**
  [https://git-scm.com/downloads](https://git-scm.com/downloads)
* **IDE для C# (опционально)**
  Visual Studio, Rider или VS Code с расширением C#

---

## 📦 Установка и запуск

### 1. Клонирование репозитория

```bash
git clone https://github.com/coolrinet/UsersTasksBackend.git
cd UsersTasksBackend
```

---

### 2. Локальный запуск

```bash
# Восстановление .NET зависимостей
dotnet restore

# Сборка проекта
dotnet build

# Запуск приложения
dotnet run
```

---

## ⚙️ Конфигурация

Настройка выполняется через `appsettings.json` или переменные окружения.

### Переменные окружения

* `ASPNETCORE_ENVIRONMENT` — окружение (Development / Production)
* `ConnectionStrings:DefaultConnection` — строка подключения к БД

---

### Пример `appsettings.json`

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=UsersTasksDB;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

---
