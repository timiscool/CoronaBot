using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Globalization;


namespace DiscordBot
{
    public class Program
    {
        static void Main(string[] args)
        {
            var bot = new CoronaBot();
            bot.RunAsync().GetAwaiter().GetResult();
        }

    }
}
