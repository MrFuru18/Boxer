using ApiLibrary.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrary.Repo
{
    class ShortageProcessor
    {
        public static async Task<List<Shortage>> getAllShortages()
        {
            string url = "http://localhost:3000/shortages";
            List<Shortage> shortagesList = new List<Shortage>();

            using (HttpResponseMessage response = await ClientHttp.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    string jsonResult = await response.Content.ReadAsStringAsync();
                    shortagesList = JsonConvert.DeserializeObject<List<Shortage>>(jsonResult);
                    return shortagesList;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
