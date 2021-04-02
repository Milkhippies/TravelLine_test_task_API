using System;
using RestSharp;
using RestSharp.Serialization.Json;
using TestAPI.Connector.OneCall;
using TestAPI.Connector.Weather;

namespace TestAPI
{
    public abstract class Utils
    {
        
        private const string weatherUrl = "http://api.openweathermap.org/data/2.5/weather";
        private const string OneCallUrl = "http://api.openweathermap.org/data/2.5/onecall";
        
        public static WeatherResponce GetWeather(string city, string metrics, string keyAPI)
        {
            var client = new RestClient(weatherUrl);
            var request = new RestRequest("?" + city + metrics + keyAPI, Method.GET);
            var queryResult = client.Execute(request);
            WeatherResponce receivedJSON = new JsonDeserializer().Deserialize<WeatherResponce>(queryResult);
            return receivedJSON;
        }
        
        
        public static OneCallResponce GetOneCall(string lat, string lon, string exclude,string metrics, string keyAPI)
        {
            var client = new RestClient(OneCallUrl);
            var request = new RestRequest("?" + lat + lon + exclude + metrics + keyAPI, Method.GET);
            var queryResult = client.Execute(request);
            OneCallResponce receivedJSONOneCall = new JsonDeserializer().Deserialize<OneCallResponce>(queryResult);
            return receivedJSONOneCall;
        }
        
        
        public static DateTime UnixTimeStampToDateTime( double unixTimeStamp )
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970,1,1,0,0,0,0,System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds( unixTimeStamp ).ToLocalTime();
            return dtDateTime;
        }


        public static int setRandomDay()
        {
            var rnd = new Random();
            int addDay = rnd.Next(0,8);
            
            DateTime currentDay = DateTime.Now;
            System.Console.WriteLine("Сегодня " + currentDay.Day + "-е число");
            currentDay = currentDay.AddDays(addDay);
            System.Console.WriteLine("Посмотрим на " + currentDay.Day + "-е число");
            
            return addDay;
        }
    }
}