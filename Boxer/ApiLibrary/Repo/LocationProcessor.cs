using ApiLibrary.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrary.Repo
{
    class LocationProcessor
    {
        public static async Task<List<Location>> getAllLocations()
        {
            string url = "http://localhost:3000/locations";
            List<Location> locationsList = new List<Location>();

            using (HttpResponseMessage response = await ClientHttp.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    string jsonResult = await response.Content.ReadAsStringAsync();
                    locationsList = JsonConvert.DeserializeObject<List<Location>>(jsonResult);
                    return locationsList;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
