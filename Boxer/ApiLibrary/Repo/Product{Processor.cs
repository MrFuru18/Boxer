using ApiLibrary.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrary.Repo
{
    class Product_Processor
    {
        public static async Task<List<Product>> getAllProducts()
        {
            string url = "http://localhost:3000/products";
            List<Product> productsList = new List<Product>();

            using (HttpResponseMessage response = await ClientHttp.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    string jsonResult = await response.Content.ReadAsStringAsync();
                    productsList = JsonConvert.DeserializeObject<List<Product>>(jsonResult);
                    return productsList;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
