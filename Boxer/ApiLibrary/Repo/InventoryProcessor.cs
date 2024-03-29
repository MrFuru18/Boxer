﻿using ApiLibrary.Model;
using ApiLibrary.Model.ToCreate;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrary.Repo
{
    public class InventoryProcessor
    {
        public static async Task<List<Inventory>> getAllInventory(Inventory inventory)
        {
            string url = "http://localhost:3000/inventory";
            List<Inventory> inventoryList = new List<Inventory>();
            string serializedInventory = JsonConvert.SerializeObject(inventory);

            using (HttpResponseMessage response = await ClientHttp.ApiClient
                .PostAsync(url, new StringContent(serializedInventory, Encoding.UTF8, "application/json")).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    string jsonResult = await response.Content.ReadAsStringAsync();
                    inventoryList = JsonConvert.DeserializeObject<List<Inventory>>(jsonResult);
                    return inventoryList;
                }
                else
                {
                    return null;
                }
            }
        }

        public static async Task<Inventory> getInventory(Inventory inventory)
        {
            string url = "http://localhost:3000/inventory/get/" + inventory.id;

            using (HttpResponseMessage response = await ClientHttp.ApiClient.GetAsync(url).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    string jsonResult = await response.Content.ReadAsStringAsync();
                    inventory = JsonConvert.DeserializeObject<Inventory>(jsonResult);
                    return inventory;
                }
                else
                {
                    return null;
                }
            }
        }

        public static async Task<List<Inventory>> getInventoryWhereProduct(Inventory inventory)
        {
            string url = "http://localhost:3000/inventory/product/get/" + inventory.product_id;
            List<Inventory> inventoryList = new List<Inventory>();
            string serializedInventory = JsonConvert.SerializeObject(inventory);

            using (HttpResponseMessage response = await ClientHttp.ApiClient
                .PostAsync(url, new StringContent(serializedInventory, Encoding.UTF8, "application/json")).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    string jsonResult = await response.Content.ReadAsStringAsync();
                    inventoryList = JsonConvert.DeserializeObject<List<Inventory>>(jsonResult);
                    return inventoryList;
                }
                else
                {
                    return null;
                }
            }
        }

        public static async Task<string> addInventory(ToCreateInventory inventory)
        {
            string url = "http://localhost:3000/inventory/add";
            string result;
            string serializedInventory = JsonConvert.SerializeObject(inventory);

            using (HttpResponseMessage response = await ClientHttp.ApiClient
                .PostAsync(url, new StringContent(serializedInventory, Encoding.UTF8, "application/json")).ConfigureAwait(false))
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

        public static async Task<string> updateInventory(ToCreateInventory inventory)
        {
            string url = "http://localhost:3000/inventory/edit/" + inventory.id;
            string result;
            string serializedInventory = JsonConvert.SerializeObject(inventory);

            using (HttpResponseMessage response = await ClientHttp.ApiClient
                .PutAsync(url, new StringContent(serializedInventory, Encoding.UTF8, "application/json")).ConfigureAwait(false))
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

        public static async Task<string> deleteInventory(Inventory inventory)
        {
            string url = "http://localhost:3000/inventory/delete/" + inventory.id;
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
