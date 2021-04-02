using System;
using NUnit.Framework;
using TestAPI.Connector.OneCall;
using TestAPI.Connector.Weather;

namespace TestAPI
{
    public class TestAPI
    {
        string cityID = "id=466806&";
        string wrongCityID = "id=4668061111&";
        string notCityID = "id=FFF315&";

        string exclude = "exclude=minutely,hourly,alerts&";
        string lat = "lat=56.6388&";
        string lon = "lon=47.8908&";
        
        string metrics = "units=metric&";
        
        string validAPI = "appid=638c24b3e73bf7020f254dd951504118";
        string invalidAPI = "appid=638c24b3e73bf7020f254dd951504";

        private WeatherResponce weatherResponce;
        private OneCallResponce oneCallResponce;

        [Test]
        public void test_get_random_day_temperature()
        {
            oneCallResponce = Utils.GetOneCall(lat, lon, exclude, metrics, validAPI);
            int dayDelta = Utils.setRandomDay();
            Console.WriteLine(DateTime.Now.AddDays(dayDelta).Day + "-го числа будет " + oneCallResponce.daily[dayDelta].temp.day + "°");
            Assert.Pass();

            // DateTime currentDay = DateTime.Now;
            // for (int i = 0; i < 8; i++) { Console.WriteLine(currentDay.AddDays(i).Day + "-го числа будет " +
            //                                                        oneCallResponce.daily[i].temp.day + "°"); }
            
        }
        
        
        [Test]
        public void test_get_status_200_cod_ok()
        {
            weatherResponce = Utils.GetWeather(cityID, metrics, validAPI);
            Console.WriteLine("Получен код: " + weatherResponce.cod);
            Assert.That(weatherResponce.cod == 200, "Ожидался код 200");
        }
        
       
        [Test]
        public void test_get_status_400_not_city_id()
        {
            weatherResponce = Utils.GetWeather(notCityID, metrics, validAPI);
            Console.WriteLine("Получено сообщение: " + weatherResponce.message);
            Console.WriteLine("Получен код: " + weatherResponce.cod);
            Assert.That(weatherResponce.message.Contains("is not a city ID"), "Ожидалась,что для невалидного ИДа будет соответствующее сообщение");
            Assert.That(weatherResponce.cod == 400, "Ошибка должна иметь код 400");
        }

        
        [Test]
        public void test_get_status_401_invalid_api()
        {
            weatherResponce = Utils.GetWeather(cityID, metrics, invalidAPI);
            Console.WriteLine("Получено сообщение: " + weatherResponce.message);
            Console.WriteLine("Получен код: " + weatherResponce.cod);
            Assert.That(weatherResponce.message.Length >= 0, "Нет текста в сообщении об ошибке");
            Assert.That(weatherResponce.cod == 401, "Ошибка должна иметь код 401");
        }

        
        [Test]
        public void test_get_status_404_message_wrong_city()
        {
            weatherResponce = Utils.GetWeather(wrongCityID, metrics, validAPI);
            Console.WriteLine("Получено сообщение: " + weatherResponce.message);
            Console.WriteLine("Получен код: " + weatherResponce.cod);
            Assert.That(weatherResponce.message == "city not found", "Город не должен был быть найден");
            Assert.That(weatherResponce.cod == 404, "Ошибка должна иметь код 404");
        }
        
        
        [Test]
        public void test_does_received_JSON_have_correct_date()
        {
            weatherResponce = Utils.GetWeather(cityID, metrics, validAPI);
            var dateJSON = Utils.UnixTimeStampToDateTime(weatherResponce.dt);
            Console.WriteLine("Получена дата: " + dateJSON);
            Assert.That(dateJSON.Day == DateTime.Now.Day, "Не верный день в полученном JSON");
        }
        
        
        [Test]
        public void test_get_city_name_Yoshkar_Ola()
        {
            weatherResponce = Utils.GetWeather(cityID, metrics, validAPI);
            Console.WriteLine("Получено название пункта: " + weatherResponce.name);
            Assert.That(weatherResponce.name == "Yoshkar-Ola", "По ИДу нашлась не Йошкар-Ола");
        }
        
        
        [Test]
        public void test_get_temperature_for_Yoshkar_Ola()
        {
            weatherResponce = Utils.GetWeather(cityID, metrics, validAPI);
            Console.WriteLine("Полученая температура " + weatherResponce.main.temp + "°C");
            Assert.Less(weatherResponce.main.temp, 70, "Не может быть больше 70");
            Assert.Greater(weatherResponce.main.temp, -50, "Не может быть меньше -50C");
        }
    }
}