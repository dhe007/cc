using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using CurrencyRateCalculator.Models;
using System.Collections.ObjectModel;
using Newtonsoft.Json.Linq;
using CurrencyRateCalculator.Service;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.IO;

namespace CurrencyRateCalculator.Services
{
    public class RestService : IRestService
	{
		HttpClient client;
        public string EnquireResult { get; private set; }

        
        /// <summary>
        /// Setup the connection to the server.
        /// </summary>

        public RestService ()
		{
			var authData = string.Format ("{0}:{1}", Constants.Username, Constants.Password);
			var authHeaderValue = Convert.ToBase64String (Encoding.UTF8.GetBytes (authData));

			client = new HttpClient ();
			client.MaxResponseContentBufferSize = 25600000;
			client.BaseAddress = new Uri (Constants.RestUrl);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue ("Basic", authHeaderValue);          
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        
        }

        private static bool IsValidJson(string strInput)
        {
            strInput = strInput.Trim();
            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
                (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
            {
                try
                {
                    var obj = JToken.Parse(strInput);
                    return true;
                }
                catch (JsonReaderException jex)
                {                                
                    Debug.WriteLine(@"Json invalid ERROR {0}", jex.Message);
                    return false;
                }
                catch (Exception ex) //some other exception
                {
                    Debug.WriteLine(@"Json invalid ERROR {0}", ex.Message);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// Get rate information from api and save it into local database
        /// </summary>

        public async Task<RateInfo> ITryGetRateInfos()
        {
            var rateInfos = new RateInfo();
            var localRateInfo = new LocalRateInfo();
            string s = Constants.latest + "access_key=" + Constants.Key + "&base=" + Constants.baseCurrency + "&symbols=" + Constants.symbolCurrency;

            try
            {
                var response = await client.GetAsync(s);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    rateInfos = JsonConvert.DeserializeObject<RateInfo>(content);
                    localRateInfo.success = rateInfos.success;
                    localRateInfo.Base = rateInfos.Base;

                    localRateInfo.date = rateInfos.date;
                    localRateInfo.symbol = Constants.symbolCurrency;
                    localRateInfo.rate = rateInfos.rates.USD;

                    localRateInfo.timestamp = rateInfos.timestamp;
                   

                    TodoItemDatabase database = await TodoItemDatabase.Instance;
                    await database.DeleteAllItemAsync();
                    await database.SaveItemAsync(localRateInfo);

                }

                else rateInfos = null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"Did not get rateInfos list ERROR {0}", ex.Message);
            }

            return rateInfos;
        }

        /// <summary>
        /// Get rate information from local database
        /// </summary>
        public async Task<RateInfo> ITryGetRateInfosFromLocaDatabase()
        {
            var rateInfos = new RateInfo();
            var localRateInfo = new LocalRateInfo();
            var rates = new rates();

            try
            {

                TodoItemDatabase database = await TodoItemDatabase.Instance;
                localRateInfo = await database.GetItemAsync();


                rateInfos.success = localRateInfo.success;
                rateInfos.Base = localRateInfo.Base;

                rateInfos.date = localRateInfo.date;

                rateInfos.timestamp = localRateInfo.timestamp;
                rates.USD = localRateInfo.rate;
                rateInfos.rates = rates;



            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"Did not get rateInfos list ERROR {0}", ex.Message);
            }

            return rateInfos;
        }







    }
}
