using ApiLibrary.Model;
using ApiLibrary.Model.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrary.Repo
{
    public class ProductProcessor
    {
        public static async Task<List<Product>> getAllProducts(Product product)
        {
            string url = "http://localhost:3000/products";
            List<Product> productsList = new List<Product>();
            string serializedProduct = JsonConvert.SerializeObject(product);

            using (HttpResponseMessage response = await ClientHttp.ApiClient
                .PostAsync(url, new StringContent(serializedProduct, Encoding.UTF8, "application/json")).ConfigureAwait(false))
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

        public static async Task<List<ProductDetailed>> getAllProductsDetailed(Product product)
        {
            string url = "http://localhost:3000/products";
            List<ProductDetailed> productsList = new List<ProductDetailed>();
            string serializedProduct = JsonConvert.SerializeObject(product);

            using (HttpResponseMessage response = await ClientHttp.ApiClient
                .PostAsync(url, new StringContent(serializedProduct, Encoding.UTF8, "application/json")).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    string jsonResult = await response.Content.ReadAsStringAsync();
                    productsList = JsonConvert.DeserializeObject<List<ProductDetailed>>(jsonResult);
                    return productsList;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<Product> getProduct(Product product)
        {
            string url = "http://localhost:3000/product/get/" + product.id;
            string serializedProduct = JsonConvert.SerializeObject(product);

            using (HttpResponseMessage response = await ClientHttp.ApiClient
                .PostAsync(url, new StringContent(serializedProduct, Encoding.UTF8, "application/json")).ConfigureAwait(false))
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

        public static async Task<string> deleteProduct(Product product)
        {
            string url = "http://localhost:3000/product/delete/" + product.id;
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
        public static async Task<Category> getCategory(Category category)
        {
            string url = "http://localhost:3000/category/get/" + category.id;
            List<Category> categoriesList = new List<Category>();
            string serializedCategory = JsonConvert.SerializeObject(category);

            using (HttpResponseMessage response = await ClientHttp.ApiClient
                .PostAsync(url, new StringContent(serializedCategory, Encoding.UTF8, "application/json")).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    string jsonResult = await response.Content.ReadAsStringAsync();
                    categoriesList = JsonConvert.DeserializeObject<List<Category>>(jsonResult);
                    if (categoriesList.Count > 0)
                        category = categoriesList[0];
                    return category;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }


        public static async Task<List<Category>> getCategories(Category category)
        {
            string url = "http://localhost:3000/categories";
            List<Category> categoriesList = new List<Category>();
            string serializedCategory = JsonConvert.SerializeObject(category);

            using (HttpResponseMessage response = await ClientHttp.ApiClient
                .PostAsync(url, new StringContent(serializedCategory, Encoding.UTF8, "application/json")).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    string jsonResult = await response.Content.ReadAsStringAsync();
                    categoriesList = JsonConvert.DeserializeObject<List<Category>>(jsonResult);
                    return categoriesList;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
