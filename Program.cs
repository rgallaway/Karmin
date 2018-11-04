using Karmin.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Karmin
{
    public class Program
    {
        private static HttpClient mEntityClient;
        public static HttpClient EntityClient 
        {
            get 
            {
                if (mEntityClient == null)
                {
                    mEntityClient = new HttpClient();
                    mEntityClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Constants.EntityApiKey);
                }
                return mEntityClient;
            }
        }

        private static HttpClient mQemotionClient;
        public static HttpClient QemotionClient 
        {
            get
            {
                if (mQemotionClient == null)
                {
                    mQemotionClient = new HttpClient();
                    mQemotionClient.DefaultRequestHeaders.Add("Authorization", "Token token=\"" + Constants.QemotionToken + "\"");
                    mQemotionClient.DefaultRequestHeaders.Add("X-Mashape-Key", Constants.MashapeKey);
                }
                return mQemotionClient;
            }
        }

        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
