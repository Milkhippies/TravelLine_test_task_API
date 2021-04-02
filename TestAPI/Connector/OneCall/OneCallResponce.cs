using System.Collections.Generic;

namespace TestAPI.Connector.OneCall
{
    public class OneCallResponce
    {
        public double lat { get; set; }
        public double lon { get; set; }
        public string timezone { get; set; }
        public int timezone_offset { get; set; }
        public Current current { get; set; }
        public List<Daily> daily { get; set; }
    }
}