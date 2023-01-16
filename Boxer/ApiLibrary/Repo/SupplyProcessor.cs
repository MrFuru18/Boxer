using ApiLibrary.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrary.Repo
{
    public class SupplyProcessor
    {
        public static async Task<List<Supply>> getAllSupplies(Supply supply)
        {
            string url = "http://localhost:3000/supplies";
            List<Supply> suppliesList = new List<Supply>();
            string serializedSupply = JsonConvert.SerializeObject(supply);

            using (HttpResponseMessage response = await ClientHttp.ApiClient
                .PostAsync(url, new StringContent(serializedSupply, Encoding.UTF8, "application/json")).ConfigureAwait(false))
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

        public static async Task<Supply> getSupply(Supply supply)
        {
            string url = "http://localhost:3000/supply/get/" + supply.id;
            string serializedSupply = JsonConvert.SerializeObject(supply);

            using (HttpResponseMessage response = await ClientHttp.ApiClient
                .PostAsync(url, new StringContent(serializedSupply, Encoding.UTF8, "application/json")).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    string jsonResult = await response.Content.ReadAsStringAsync();
                    supply = JsonConvert.DeserializeObject<Supply>(jsonResult);
                    return supply;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<string> addSupply(Supply supply)
        {
            string url = "http://localhost:3000/supply/add";
            string result;
            string serializedSupply = JsonConvert.SerializeObject(supply);

            using (HttpResponseMessage response = await ClientHttp.ApiClient
                .PostAsync(url, new StringContent(serializedSupply, Encoding.UTF8, "application/json")).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<string> updateSupply(Supply supply)
        {
            string url = "http://localhost:3000/supply/edit/" + supply.id;
            string result;
            string serializedSupply = JsonConvert.SerializeObject(supply);

            using (HttpResponseMessage response = await ClientHttp.ApiClient
                .PutAsync(url, new StringContent(serializedSupply, Encoding.UTF8, "application/json")).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<string> deleteSupply(int id)
        {
            string url = "http://localhost:3000/supply/delete/" + id;
            string result;

            using (HttpResponseMessage response = await ClientHttp.ApiClient.DeleteAsync(url).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
