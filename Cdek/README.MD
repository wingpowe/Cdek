# Сервис расчета стоимости доставки СДЕК

## Подключение к базе данных
* необходимо установить Postgresql
* необходимо настроить параметры подключения к базе данных: Model\ApplicationContext.cs строка 26
* dotnet ef database update (visual studio: Update-Database)

## Сборка проекта
dotnet run

## Адрес swager
https://localhost:7004/swagger/index.html ```



## формат запроса
```
[
  {
    "id": 0,
    "weight": 5.9, // в кг
    "height": 0.1, // в м
    "width": 0.2, // в м
    "length": 0.3, // в м
    "fromLocation": "17e498bd-5f9e-4221-8998-5fa24a35ed2e", //фиас код
    "toLocation": "a9958004-d348-442e-ae0d-b09aca9fdf25"
  }
] 
```



