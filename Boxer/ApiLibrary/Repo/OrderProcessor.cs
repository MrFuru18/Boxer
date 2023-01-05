using ApiLibrary.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrary.Repo
{
    class OrderProcessor
    {
        public static async Task<List<Order>> getAllOrders()
        {
            string url = "http://localhost:3000/orders";
            List<Order> ordersList = new List<Order>();

            using (HttpResponseMessage response = await ClientHttp.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    string jsonResult = await response.Content.ReadAsStringAsync();
                    ordersList = JsonConvert.DeserializeObject<List<Order>>(jsonResult);
                    return ordersList;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
