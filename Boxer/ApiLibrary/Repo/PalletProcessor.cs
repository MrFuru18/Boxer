using ApiLibrary.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrary.Repo
{
    class PalletProcessor
    {
        public static async Task<List<Pallet>> getAllPallets()
        {
            string url = "http://localhost:3000/pallets";
            List<Pallet> palletsList = new List<Pallet>();

            using (HttpResponseMessage response = await ClientHttp.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    string jsonResult = await response.Content.ReadAsStringAsync();
                    palletsList = JsonConvert.DeserializeObject<List<Pallet>>(jsonResult);
                    return palletsList;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
