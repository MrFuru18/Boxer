using ApiLibrary.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrary.Repo
{
    public class LocationProcessor
    {
        public static async Task<List<Location>> getAllLocations(Location location)
        {
            string url = "http://localhost:3000/locations";
            List<Location> locationsList = new List<Location>();
            string serializedLocation = JsonConvert.SerializeObject(location);

            using (HttpResponseMessage response = await ClientHttp.ApiClient
                .PostAsync(url, new StringContent(serializedLocation, Encoding.UTF8, "application/json")).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    string jsonResult = await response.Content.ReadAsStringAsync();
                    locationsList = JsonConvert.DeserializeObject<List<Location>>(jsonResult);
                    return locationsList;
                }
                else
                {
                    return null;
                }
            }
        }

        public static async Task<List<Location>> getAvailableLocations(Location location)
        {
            string url = "http://localhost:3000/locations/available";
            List<Location> locationsList = new List<Location>();
            string serializedLocation = JsonConvert.SerializeObject(location);

            using (HttpResponseMessage response = await ClientHttp.ApiClient
                .PostAsync(url, new StringContent(serializedLocation, Encoding.UTF8, "application/json")).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    string jsonResult = await response.Content.ReadAsStringAsync();
                    locationsList = JsonConvert.DeserializeObject<List<Location>>(jsonResult);
                    return locationsList;
                }
                else
                {
                    return null;
                }
            }
        }

        public static async Task<Location> getLocation(Location location)
        {
            string url = "http://localhost:3000/location/get/" + location.id;
            string serializedLocation = JsonConvert.SerializeObject(location);

            using (HttpResponseMessage response = await ClientHttp.ApiClient
                .PostAsync(url, new StringContent(serializedLocation, Encoding.UTF8, "application/json")).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    string jsonResult = await response.Content.ReadAsStringAsync();
                    location = JsonConvert.DeserializeObject<Location>(jsonResult);
                    return location;
                }
                else
                {
                    return null;
                }
            }
        }

        public static async Task<string> addLocation(Location location)
        {
            string url = "http://localhost:3000/location/add";
            string result;
            string serializedLocation = JsonConvert.SerializeObject(location);

            using (HttpResponseMessage response = await ClientHttp.ApiClient
                .PostAsync(url, new StringContent(serializedLocation, Encoding.UTF8, "application/json")).ConfigureAwait(false))
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

        public static async Task<string> updateLocation(Location location)
        {
            string url = "http://localhost:3000/location/edit/" + location.id;
            string result;
            string serializedLocation = JsonConvert.SerializeObject(location);

            using (HttpResponseMessage response = await ClientHttp.ApiClient
                .PutAsync(url, new StringContent(serializedLocation, Encoding.UTF8, "application/json")).ConfigureAwait(false))
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

        public static async Task<string> deleteLocation(Location location)
        {
            string url = "http://localhost:3000/location/delete/" + location.id;
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
