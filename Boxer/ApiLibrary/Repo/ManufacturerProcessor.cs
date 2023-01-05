using ApiLibrary.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrary.Repo
{
    class ManufacturerProcessor
    {
        public static async Task<List<Manufacturer>> getAllManufacturers()
        {
            string url = "http://localhost:3000/manufacturers";
            List<Manufacturer> manufacturersList = new List<Manufacturer>();

            using (HttpResponseMessage response = await ClientHttp.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    string jsonResult = await response.Content.ReadAsStringAsync();
                    manufacturersList = JsonConvert.DeserializeObject<List<Manufacturer>>(jsonResult);
                    return manufacturersList;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
