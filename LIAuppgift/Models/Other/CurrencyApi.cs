namespace LIAuppgift.Models.Other
{
    using Newtonsoft.Json;

    public class CurrencyApi
    {
        public class USD
        {
            [JsonProperty("15m")]
            public double _15m { get; set; }
            public double last { get; set; }
            public double buy { get; set; }
            public double sell { get; set; }
            public string symbol { get; set; }
        }

        public class EUR
        {
            [JsonProperty("15m")]
            public double _15m { get; set; }
            public double last { get; set; }
            public double buy { get; set; }
            public double sell { get; set; }
            public string symbol { get; set; }
        }

        public class Root
        {
            public USD USD { get; set; }
            public EUR EUR { get; set; }
        }
    }
}