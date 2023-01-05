using ApiLibrary.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrary.Repo
{
    class CustomerProcessor
    {
        public static async Task<List<Customer>> getAllCustomers()
        {
            string url = "http://localhost:3000/customers";
            List<Customer> customersList = new List<Customer>();

            using (HttpResponseMessage response = await ClientHttp.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    string jsonResult = await response.Content.ReadAsStringAsync();
                    customersList = JsonConvert.DeserializeObject<List<Customer>>(jsonResult);
                    return customersList;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
