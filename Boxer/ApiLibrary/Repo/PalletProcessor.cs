﻿using ApiLibrary.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrary.Repo
{
    public class PalletProcessor
    {
        public static async Task<List<Pallet>> getAllPallets(Pallet pallet)
        {
            string url = "http://localhost:3000/pallets";
            List<Pallet> palletsList = new List<Pallet>();
            string serializedPallet = JsonConvert.SerializeObject(pallet);

            using (HttpResponseMessage response = await ClientHttp.ApiClient
                .PostAsync(url, new StringContent(serializedPallet, Encoding.UTF8, "application/json")).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    string jsonResult = await response.Content.ReadAsStringAsync();
                    palletsList = JsonConvert.DeserializeObject<List<Pallet>>(jsonResult);
                    return palletsList;
                }
                else
                {
                    return null;
                }
            }
        }

        public static async Task<Pallet> getPallet(Pallet pallet)
        {
            string url = "http://localhost:3000/pallet/get/" + pallet.id;
            string serializedPallet = JsonConvert.SerializeObject(pallet);

            using (HttpResponseMessage response = await ClientHttp.ApiClient
                .PostAsync(url, new StringContent(serializedPallet, Encoding.UTF8, "application/json")).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    string jsonResult = await response.Content.ReadAsStringAsync();
                    pallet = JsonConvert.DeserializeObject<Pallet>(jsonResult);
                    return pallet;
                }
                else
                {
                    return null;
                }
            }
        }

        public static async Task<string> addPallet(Pallet pallet)
        {
            string url = "http://localhost:3000/pallet/add";
            string result;
            string serializedPallet = JsonConvert.SerializeObject(pallet);

            using (HttpResponseMessage response = await ClientHttp.ApiClient
                .PostAsync(url, new StringContent(serializedPallet, Encoding.UTF8, "application/json")).ConfigureAwait(false))
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

        public static async Task<string> updatePallet(Pallet pallet)
        {
            string url = "http://localhost:3000/pallet/edit/" + pallet.id;
            string result;
            string serializedPallet = JsonConvert.SerializeObject(pallet);

            using (HttpResponseMessage response = await ClientHttp.ApiClient
                .PutAsync(url, new StringContent(serializedPallet, Encoding.UTF8, "application/json")).ConfigureAwait(false))
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

        public static async Task<string> deletePallet(Pallet pallet)
        {
            string url = "http://localhost:3000/pallet/delete/" + pallet.id;
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
