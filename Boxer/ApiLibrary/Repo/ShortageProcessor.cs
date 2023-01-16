using ApiLibrary.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrary.Repo
{
    public class ShortageProcessor
    {
        public static async Task<List<Shortage>> getAllShortages(Shortage shortage)
        {
            string url = "http://localhost:3000/shortages";
            List<Shortage> shortagesList = new List<Shortage>();
            string serializedShortage = JsonConvert.SerializeObject(shortage);

            using (HttpResponseMessage response = await ClientHttp.ApiClient
                .PostAsync(url, new StringContent(serializedShortage, Encoding.UTF8, "application/json")).ConfigureAwait(false))
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

        public static async Task<Shortage> getShortage(Shortage shortage)
        {
            string url = "http://localhost:3000/shortage/get/" + shortage.id;
            string serializedShortage = JsonConvert.SerializeObject(shortage);

            using (HttpResponseMessage response = await ClientHttp.ApiClient
                .PostAsync(url, new StringContent(serializedShortage, Encoding.UTF8, "application/json")).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    string jsonResult = await response.Content.ReadAsStringAsync();
                    shortage = JsonConvert.DeserializeObject<Shortage>(jsonResult);
                    return shortage;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<string> addShortage(Shortage shortage)
        {
            string url = "http://localhost:3000/shortage/add";
            string result;
            string serializedShortage = JsonConvert.SerializeObject(shortage);

            using (HttpResponseMessage response = await ClientHttp.ApiClient
                .PostAsync(url, new StringContent(serializedShortage, Encoding.UTF8, "application/json")).ConfigureAwait(false))
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

        public static async Task<string> updateShortage(Shortage shortage)
        {
            string url = "http://localhost:3000/shortage/edit/" + shortage.id;
            string result;
            string serializedShortage = JsonConvert.SerializeObject(shortage);

            using (HttpResponseMessage response = await ClientHttp.ApiClient
                .PutAsync(url, new StringContent(serializedShortage, Encoding.UTF8, "application/json")).ConfigureAwait(false))
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

        public static async Task<string> deleteShortage(int id)
        {
            string url = "http://localhost:3000/shortage/delete/" + id;
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
