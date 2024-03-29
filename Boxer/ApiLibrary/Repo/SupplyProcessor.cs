﻿using ApiLibrary.Model;
using ApiLibrary.Model.ToCreate;
using ApiLibrary.Model.Views;
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
                    return null;
                }
            }
        }

        public static async Task<List<Supply>> getSuppliesNotConnected(Supply supply)
        {
            string url = "http://localhost:3000/supplies/not_connected";
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
                    return null;
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
                    return null;
                }
            }
        }

        public static async Task<string> addSupply(ToCreateSupply supply)
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
                    return null;
                }
            }
        }

        public static async Task<string> updateSupply(ToCreateSupply supply)
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
                    return null;
                }
            }
        }

        public static async Task<string> deleteSupply(Supply supply)
        {
            string url = "http://localhost:3000/supply/delete/" + supply.id;
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
                    return null;
                }
            }
        }


        public static async Task<List<SupplyItem>> getSupplyItems(SupplyItem supplyItem)
        {
            string url = "http://localhost:3000/supply_items/get_from_supply/" + supplyItem.supply_id;
            List<SupplyItem> supplyItemsList = new List<SupplyItem>();
            string serializedSupplyItem = JsonConvert.SerializeObject(supplyItem);

            using (HttpResponseMessage response = await ClientHttp.ApiClient
                .PostAsync(url, new StringContent(serializedSupplyItem, Encoding.UTF8, "application/json")).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    string jsonResult = await response.Content.ReadAsStringAsync();
                    supplyItemsList = JsonConvert.DeserializeObject<List<SupplyItem>>(jsonResult);
                    return supplyItemsList;
                }
                else
                {
                    return null;
                }
            }
        }

        public static async Task<List<SupplyItemDetailed>> getSupplyItemsDetailed(SupplyItem supplyItem)
        {
            string url = "http://localhost:3000/supply_items/get_from_supply/" + supplyItem.supply_id;
            List<SupplyItemDetailed> supplyItemsList = new List<SupplyItemDetailed>();
            string serializedSupplyItem = JsonConvert.SerializeObject(supplyItem);

            using (HttpResponseMessage response = await ClientHttp.ApiClient
                .PostAsync(url, new StringContent(serializedSupplyItem, Encoding.UTF8, "application/json")).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    string jsonResult = await response.Content.ReadAsStringAsync();
                    supplyItemsList = JsonConvert.DeserializeObject<List<SupplyItemDetailed>>(jsonResult);
                    return supplyItemsList;
                }
                else
                {
                    return null;
                }
            }
        }

        public static async Task<string> addSupplyItem(SupplyItem supplyItem)
        {
            string url = "http://localhost:3000/supply_item/add";
            string result;
            string serializedSupplyItem = JsonConvert.SerializeObject(supplyItem);

            using (HttpResponseMessage response = await ClientHttp.ApiClient
                .PostAsync(url, new StringContent(serializedSupplyItem, Encoding.UTF8, "application/json")).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();
                    return result;
                }
                else
                {
                    return null;
                }
            }
        }

        public static async Task<string> deleteSupplyItem(SupplyItem supplyItem)
        {
            string url = "http://localhost:3000/supply_item/delete/" + supplyItem.id;
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
                    return null;
                }
            }
        }
    }
}
