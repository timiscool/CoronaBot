using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace DiscordBot
{
    public class CoronaMetrics
    {

        public string GetDeathsByCountry(string country)
        {
            _ = "Couldn't deaths found for " + country + "\n";
            var item = GetItems(country);
            string msg = item.deaths.ToString();
            return msg;
        }


        public string GetSummaryByCountry(string country)

        {

            var item = GetItems(country);
            _ = "No summary found for " + country + "\n";


            string summary =
                 country + " has " + item.confirmed.ToString() + " cases, with " + item.recovered.ToString() + " recoverd "
                     + "and " + item.critical.ToString() + " critical, " + "and " + item.deaths.ToString() + " total deaths.";

            return summary;
        }

        private RootObject GetItems (string country)
        {
            var response = GetCoronaContent();
            var contents = ParseContent(response);
            return contents.Find(x => x.country.ToUpper() == country.ToUpper());

        }

        private List<RootObject> ParseContent(IRestResponse response)
        {
            var content = response.Content;
            var parse = (JArray)JsonConvert.DeserializeObject(content);
            var responseObjects = new List<RootObject>();

            foreach (var item in parse.Root)
            {
                responseObjects.Add(JsonConvert.DeserializeObject<RootObject>(item.ToString()));
            }

            return responseObjects;
        }

        private IRestResponse GetCoronaContent()
        {
            var client = new RestClient("https://covid-19-data.p.rapidapi.com/country/all?format=undefined");
            var request = new RestRequest(Method.GET);
            request.AddHeader("", "");
            request.AddHeader("", "");
            IRestResponse response = client.Execute(request);
            return response;
        }

    }



    sealed class RootObject
    {
        public string country { get; set; }
        public int confirmed { get; set; }
        public int recovered { get; set; }
        public int critical { get; set; }
        public int deaths { get; set; }
        public double? latitude { get; set; }
        public double? longitude { get; set; }
    }







}
