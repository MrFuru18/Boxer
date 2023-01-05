using ApiLibrary.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrary.Repo
{
    class SupplyProcessor
    {
        public static async Task<List<Supply>> getAllSupplies()
        {
            string url = "http://localhost:3000/supplies";
            List<Supply> suppliesList = new List<Supply>();

            using (HttpResponseMessage response = await ClientHttp.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    string jsonResult = await response.Content.ReadAsStringAsync();
                    suppliesList = JsonConvert.DeserializeObject<List<Supply>>(jsonResult);
                    return suppliesList;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
