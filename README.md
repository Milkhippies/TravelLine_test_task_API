Данный проект является решением третьего задания - API автотесты для OpenWeatherMap.org

- API автотесты для проверки WeatherAPI описываются в файле TestAPI. Проверки осуществляются сравнением статусов и сообщений получаемых в ответ на наш запрос с разной конфигурацией.
- Проверка температуры в Йошкар-Оле на любую дату реализована TestAPI, в тесте test_get_random_day_temperature с использованием OneCallAPI

Используя бесплатный тарифный план мы можем получить температуру лишь на 7 дней вперед. При помощи рандома получаем число на которое хотим вывести температуру и непосредственно выводим в консоли.
