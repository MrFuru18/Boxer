using ApiLibrary.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrary.Repo
{
    public class CustomerProcessor
    {
        public static async Task<List<Customer>> getAllCustomers(Customer customer)
        {
            string url = "http://localhost:3000/customers";
            List<Customer> customersList = new List<Customer>();
            string serializedCustomer = JsonConvert.SerializeObject(customer);

            using (HttpResponseMessage response = await ClientHttp.ApiClient
                .PostAsync(url, new StringContent(serializedCustomer, Encoding.UTF8, "application/json")).ConfigureAwait(false))
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
        
        public static async Task<Customer> getCustomer(Customer customer)
        {
            string url = "http://localhost:3000/customer/get/" + customer.id;
            string serializedCustomer = JsonConvert.SerializeObject(customer);

            using (HttpResponseMessage response = await ClientHttp.ApiClient
                .PostAsync(url, new StringContent(serializedCustomer, Encoding.UTF8, "application/json")).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    string jsonResult = await response.Content.ReadAsStringAsync();
                    customer = JsonConvert.DeserializeObject<Customer>(jsonResult);
                    return customer;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<string> addCustomer(Customer customer)
        {
            string url = "http://localhost:3000/customer/add";
            string result;
            string serializedCustomer = JsonConvert.SerializeObject(customer);

            using (HttpResponseMessage response = await ClientHttp.ApiClient
                .PostAsync(url, new StringContent(serializedCustomer, Encoding.UTF8, "application/json")).ConfigureAwait(false))
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

        public static async Task<string> updateCustomer(Customer customer)
        {
            string url = "http://localhost:3000/customer/edit/" + customer.id;
            string result;
            string serializedCustomer = JsonConvert.SerializeObject(customer);

            using (HttpResponseMessage response = await ClientHttp.ApiClient
                .PutAsync(url, new StringContent(serializedCustomer, Encoding.UTF8, "application/json")).ConfigureAwait(false))
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

        public static async Task<string> deleteCustomer(int id)
        {
            string url = "http://localhost:3000/customer/delete/" + id;
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


        public static async Task<List<CustomerAddress>> getCustomerAdresses(CustomerAddress customerAddress)
        {
            string url = "http://localhost:3000/customer_addresses/get_all_of_customer/" + customerAddress.customer_id;
            List<CustomerAddress> customerAddressesList = new List<CustomerAddress>();
            string serializedCustomerAddress = JsonConvert.SerializeObject(customerAddress);

            using (HttpResponseMessage response = await ClientHttp.ApiClient
                .PostAsync(url, new StringContent(serializedCustomerAddress, Encoding.UTF8, "application/json")).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    string jsonResult = await response.Content.ReadAsStringAsync();
                    customerAddressesList = JsonConvert.DeserializeObject<List<CustomerAddress>>(jsonResult);
                    return customerAddressesList;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
