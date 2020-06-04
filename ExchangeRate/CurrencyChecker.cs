using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate
{
    public class CurrencyChecker
    {
        public string Currency { get; set; }
        public string Rate { get; set; }
        public string Number { get; set; }
        public DateTime RateDate { get; set; }
        static HttpClient client = new HttpClient();
        public HttpResponseMessage response = new HttpResponseMessage();
        public async Task<string> GetData(string CurrencyIsoCode)
        {
            try
            {
                Currency = CurrencyIsoCode.ToLower().Trim();
                client.BaseAddress = new Uri("http://api.nbp.pl/api/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                response = await client.GetAsync($"http://api.nbp.pl/api/exchangerates/rates/a/{Currency}/");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();

            }
            catch(Exception ex)
            {
                return null;
               // return null;
            }
            
        }
        public string ReturnData(Task<string> task)
        {
            try
            {
                task.Wait();
                var dt2 = JObject.Parse(task.Result)["rates"];
                foreach (var str in dt2)
                {
                    RateDate = (DateTime)str["effectiveDate"];
                    Number = (string)str["no"];
                    Rate = (string)str["mid"];

                }
                return Rate;
            }
            catch(Exception ex)
            {
                return "Currency not found";
            }
        }
    }
}
