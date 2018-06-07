using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace FeatureflagDemo
{
    public class Program
    {
        private const string urlAppData = "https://lrplv54dni.execute-api.us-east-1.amazonaws.com/FFTEST/flagdata/appdata ";
        private const string urlAppDataParameters = "?appName=Customer Authority";

        private const string urlFlagData = "https://lrplv54dni.execute-api.us-east-1.amazonaws.com/FFTEST/flagdata";
        private static string urlFlagDataParameters = "?featureName=02 test_feature";

        static void Main(string[] args)
        {
          
            var appFeatureFlags = GetAppFeatureFlags().Result;
            foreach (var item in appFeatureFlags.Items)
            {
                DisplayFeatureFlag(item);
            }

            Console.WriteLine();
            Console.WriteLine($"Enter a Feature Flag to search:");
            urlFlagDataParameters = $"?featureName={Console.ReadLine()}";

            var featureFlagResponse = GetFeatureFlag(urlFlagDataParameters).Result;
            Console.WriteLine();
            DisplayFeatureFlag(featureFlagResponse.Item);

            Console.ReadLine();
        }

        private static async Task<AppFeatureFlagData> GetAppFeatureFlags()
        {
            using (var client = new HttpClient())
            {
                AppFeatureFlagData featureFlags = null;

                client.BaseAddress = new Uri(urlAppData);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(urlAppDataParameters);
                if (response.IsSuccessStatusCode)
                {
                    featureFlags = await response.Content.ReadAsAsync<AppFeatureFlagData>();
                    return featureFlags;
                }
                else
                {
                    Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                    return featureFlags;
                }
            }
        }

        private static async Task<FeatureFlagResult> GetFeatureFlag(string featureFlagSearchParm)
        {
            using (var client = new HttpClient())
            {
                FeatureFlagResult featureFlags = null;

                client.BaseAddress = new Uri(urlFlagData);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(featureFlagSearchParm);
                if (response.IsSuccessStatusCode)
                {
                    featureFlags = await response.Content.ReadAsAsync<FeatureFlagResult>();
                    return featureFlags;
                }
                else
                {
                    Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                    return featureFlags;
                }
            }
        }

        static void DisplayFeatureFlag(Item item)
        {
            Console.WriteLine($"Application Name    --> {item.FeatureSettings.application}");
            Console.WriteLine($"Feature Name        --> {item.FeatureSettings.name}");
            Console.WriteLine($"DEV Flag            --> {item.FeatureSettings.dev}");
            Console.WriteLine($"QA Flag             --> {item.FeatureSettings.qa}");
            Console.WriteLine($"PROD Flag           --> {item.FeatureSettings.prod}");

            Console.WriteLine();
        }
    }
}
