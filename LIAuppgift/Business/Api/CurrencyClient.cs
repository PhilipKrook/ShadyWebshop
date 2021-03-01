namespace LIAuppgift.Business.Api
{
    using Newtonsoft.Json;
    using RestSharp;
    using System;
    using static LIAuppgift.Models.Other.CurrencyApi;

    public class CurrencyClient
    {
        public Root CurrencyApiClient()
        {
            var client = new RestClient("https://blockchain.info/ticker");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cookie", "__cfduid=d9d9370c643fdf99565def616464fc0e01614591784");
            IRestResponse response = client.Execute(request);
            // Console.WriteLine(response.Content);

            var BitCurrency = JsonConvert.DeserializeObject<Root>(response.Content);
            // Console.WriteLine("EUR = " + Currency.EUR.last + " " + Currency.EUR.symbol);
            // Console.WriteLine("USD = " + Currency.USD.last + " " + Currency.USD.symbol);

            return BitCurrency;
        }
    }
}