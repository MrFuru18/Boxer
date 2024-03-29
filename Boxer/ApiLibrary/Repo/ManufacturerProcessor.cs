﻿using ApiLibrary.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrary.Repo
{
    public class ManufacturerProcessor
    {
        public static async Task<List<Manufacturer>> getAllManufacturers(Manufacturer manufacturer)
        {
            string url = "http://localhost:3000/manufaturers";
            List<Manufacturer> manufacturersList = new List<Manufacturer>();
            string serializedManufacturer = JsonConvert.SerializeObject(manufacturer);

            using (HttpResponseMessage response = await ClientHttp.ApiClient
                .PostAsync(url, new StringContent(serializedManufacturer, Encoding.UTF8, "application/json")).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    string jsonResult = await response.Content.ReadAsStringAsync();
                    manufacturersList = JsonConvert.DeserializeObject<List<Manufacturer>>(jsonResult);
                    return manufacturersList;
                }
                else
                {
                    return null;
                }
            }
        }

        public static async Task<Manufacturer> getManufacturer(Manufacturer manufacturer)
        {
            string url = "http://localhost:3000/manufacturer/get/" + manufacturer.id;
            string serializedManufacturer = JsonConvert.SerializeObject(manufacturer);
            List<Manufacturer> manufacturersList = new List<Manufacturer>();

            using (HttpResponseMessage response = await ClientHttp.ApiClient
                .PostAsync(url, new StringContent(serializedManufacturer, Encoding.UTF8, "application/json")).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    string jsonResult = await response.Content.ReadAsStringAsync();
                    manufacturersList = JsonConvert.DeserializeObject<List<Manufacturer>>(jsonResult);
                    if (manufacturersList.Count > 0)
                        return manufacturersList[0];
                    return null;
                }
                else
                {
                    return null;
                }
            }
        }

        public static async Task<string> addManufacturer(Manufacturer manufacturer)
        {
            string url = "http://localhost:3000/manufacturer/add";
            string result;
            string serializedManufacturer = JsonConvert.SerializeObject(manufacturer);

            using (HttpResponseMessage response = await ClientHttp.ApiClient
                .PostAsync(url, new StringContent(serializedManufacturer, Encoding.UTF8, "application/json")).ConfigureAwait(false))
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

        public static async Task<string> updateManufacturer(Manufacturer manufacturer)
        {
            string url = "http://localhost:3000/manufacturer/edit/" + manufacturer.id;
            string result;
            string serializedManufacturer = JsonConvert.SerializeObject(manufacturer);

            using (HttpResponseMessage response = await ClientHttp.ApiClient
                .PutAsync(url, new StringContent(serializedManufacturer, Encoding.UTF8, "application/json")).ConfigureAwait(false))
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

        public static async Task<string> deleteManufacturer(Manufacturer manufacturer)
        {
            string url = "http://localhost:3000/manufacturer/delete/" + manufacturer.id;
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
