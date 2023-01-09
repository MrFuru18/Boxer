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

            using (HttpResponseMessage response = await ClientHttp.ApiClient.GetAsync(url).ConfigureAwait(false))
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

        public static async Task<Product> getProduct(int id)
        {
            string url = "http://localhost:3000/product/" + id;
            Product product = new Product();

            using (HttpResponseMessage response = await ClientHttp.ApiClient.GetAsync(url).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    string jsonResult = await response.Content.ReadAsStringAsync();
                    product = JsonConvert.DeserializeObject<Product>(jsonResult);
                    return product;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<string> addProduct(Product product)
        {
            string url = "http://localhost:3000/product/add";
            string result;
            string serializedProduct = JsonConvert.SerializeObject(product);

            using (HttpResponseMessage response = await ClientHttp.ApiClient
                .PostAsync(url, new StringContent(serializedProduct, Encoding.UTF8, "application/json")).ConfigureAwait(false))
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

        public static async Task<string> updateProduct(Product product)
        {
            string url = "http://localhost:3000/product/edit/" + product.id;
            string result;
            string serializedProduct = JsonConvert.SerializeObject(product);

            using (HttpResponseMessage response = await ClientHttp.ApiClient
                .PutAsync(url, new StringContent(serializedProduct, Encoding.UTF8, "application/json")).ConfigureAwait(false))
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

        public static async Task<string> deleteProduct(int id)
        {
            string url = "http://localhost:3000/product/delete/" + id;
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
