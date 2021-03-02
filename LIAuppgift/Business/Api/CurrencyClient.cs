namespace LIAuppgift.Business.Api
{
    using Newtonsoft.Json;
    using RestSharp;
    using System;
    using LIAuppgift.Models.Pages;
    using LIAuppgift.Models.ViewModels;
    using LIAuppgift.Models.Other;
    using LIAuppgift.Controllers;
    using static LIAuppgift.Models.Other.CurrencyApi;    

    public class CurrencyClient
    {
        public int GetConvertedFromUsd(string price)
        {
            // converts USD to BTC 
            var client = new RestClient($"https://blockchain.info/tobtc?currency=USD&value={price}");

            // gets all the Api data
            // var client = new RestClient("https://blockchain.info/ticker"); 

            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cookie", "__cfduid=d9d9370c643fdf99565def616464fc0e01614591784");
            IRestResponse response = client.Execute(request);

            var bitCurrency = JsonConvert.DeserializeObject<int>(response.Content);

            return bitCurrency;
        }
    }
}