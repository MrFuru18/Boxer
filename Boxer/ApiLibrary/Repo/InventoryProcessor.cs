using ApiLibrary.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrary.Repo
{
    class InventoryProcessor
    {
        public static async Task<List<Inventory>> getAllInventory()
        {
            string url = "http://localhost:3000/inventory";
            List<Inventory> inventoryList = new List<Inventory>();

            using (HttpResponseMessage response = await ClientHttp.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    string jsonResult = await response.Content.ReadAsStringAsync();
                    inventoryList = JsonConvert.DeserializeObject<List<Inventory>>(jsonResult);
                    return inventoryList;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
