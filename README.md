# WT-8 Razor Pages + Entity Framework Core + PostgreSQL

Интернет-магазин электроники на ASP.NET Core Razor Pages (.NET 8). Данные хранятся в PostgreSQL, доступ через Entity Framework Core (Code First, миграции).

```
Razor Pages → PageModel → AppDbContext → Entity Framework Core → PostgreSQL
```

## Возможности

- список товаров, поиск по названию;
- добавление, редактирование, удаление, просмотр товара;
- структура БД создаётся миграциями (`InitialCreate`, `AddProductDescription`);
- запуск в Docker, публикация на Render с удалённой БД PostgreSQL.

## Запуск локально

1. Создать БД `wt_lab8` в PostgreSQL.
2. В `appsettings.Development.json` указать свой пароль (файл не попадает в Git).
3. `dotnet run`. Миграции применяются автоматически при старте.

## Render

Строка подключения берётся из переменной окружения `DATABASE_URL` (Internal Database URL базы PostgreSQL на Render).

## Репозиторий

https://github.com/lera2005261-ux/wt-lab8-8razor-postgresql-baranova

## Опубликованное приложение

https://wt-lab8-8razor-postgresql-baranova.onrender.com/
