namespace LIAuppgift.Business.Api
{
    using RestSharp;
    using System;   

    public class CurrencyClient
    {
        public double GetConvertedFromUsd(string price)
        {
            // gets converted currency USD > BTC 
            var client = new RestClient($"https://blockchain.info/tobtc?currency=USD&value={price}");            

            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cookie", "__cfduid=d9d9370c643fdf99565def616464fc0e01614591784");
            IRestResponse response = client.Execute(request);

            var bitCurrency = response.Content;

            // If the API fails, sets the convertedPrice to 404
            if (Double.TryParse(bitCurrency, out double convertedPrice))
            {
                return convertedPrice;
            }
            else 
            {
                convertedPrice = 404;
            }

            return convertedPrice;
        }
    }
}