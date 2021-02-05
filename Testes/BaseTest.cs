using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.IO;
using TagSDK.Factories;
using System.Text.Json;
using TagSDK.Models.Enums;
using TagSDK.Models.Authentication;
using System;

namespace Testes
{
    [TestClass]
    public class BaseTest
    {
        public static IConfigurationRoot Configuration { get; set; }
        public static TagServiceCollection fac { get; set; }

        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        public void Init()
        {
            setToken();
        }

        protected void Print<T>(T value)
        {
            string obj = JsonConvert.SerializeObject(value);
            testContextInstance.WriteLine(PrettyJson(obj));
        }

        public void setToken()
        {

            var builder = new ConfigurationBuilder()
            .SetBasePath($"{Directory.GetDirectoryRoot("/")}/Configuration")
            .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            Dictionary<string, object> creditor = Configuration.GetSection("creditor").Get<Dictionary<string, object>>();
            Dictionary<string, object> acquirer = Configuration.GetSection("acquirer").Get<Dictionary<string, object>>();

            TokenRequest acquirerCredentials = new TokenRequest();
            acquirerCredentials.Audience = (string)acquirer["audience"];
            acquirerCredentials.ClientId = (string)acquirer["client_id"];
            acquirerCredentials.ClientSecret = (string)acquirer["client_secret"];

            TokenRequest creditorCredentials = new TokenRequest();
            creditorCredentials.Audience = (string)creditor["audience"];
            creditorCredentials.ClientId = (string)creditor["client_id"];
            creditorCredentials.ClientSecret = (string)creditor["client_secret"];

            fac = TagSdk.GetServices(options =>
            {
                options.BaseUrl = "https://integration.nonprod.taginfraestrutura.com.br";
                options.SetCredential(acquirerCredentials, Profile.ACQUIRER);
                options.SetCredential(creditorCredentials, Profile.CREDITOR);
            });


        }

        private string PrettyJson(string unPrettyJson)
        {
            var options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };

            var jsonElement = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(unPrettyJson);

            return System.Text.Json.JsonSerializer.Serialize(jsonElement, options);
        }

        public string GeraCNPJ()
        {
            int n = 10;
            Random randObj = new Random();
            int n1 = randObj.Next(n);
            int n2 = randObj.Next(n);
            int n3 = randObj.Next(n);
            int n4 = randObj.Next(n);
            int n5 = randObj.Next(n);
            int n6 = randObj.Next(n);
            int n7 = randObj.Next(n);
            int n8 = randObj.Next(n);
            int n9 = 0;
            int n10 = 0;
            int n11 = 0;
            int n12 = 1;
            int d1 = n12 * 2 + n11 * 3 + n10 * 4 + n9 * 5 + n8 * 6 + n7 * 7 + n6 * 8 + n5 * 9 + n4 * 2 + n3 * 3 + n2 * 4 + n1 * 5;

            d1 = 11 - (d1 % 11);

            if (d1 >= 10)
                d1 = 0;

            int d2 = d1 * 2 + n12 * 3 + n11 * 4 + n10 * 5 + n9 * 6 + n8 * 7 + n7 * 8 + n6 * 9 + n5 * 2 + n4 * 3 + n3 * 4 + n2 * 5 + n1 * 6;

            d2 = 11 - (d2 % 11);

            if (d2 >= 10)
                d2 = 0;

            string returnedCNPJ;
            
            returnedCNPJ = "" + n1 + n2 + n3 + n4 + n5 + n6 + n7 + n8 + n9 + n10 + n11 + n12 + d1 + d2;

            return returnedCNPJ;
        }
    }
}
