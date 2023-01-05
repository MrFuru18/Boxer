using ApiLibrary.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrary.Repo
{
    class TaskProcessor
    {
        public static async Task<List<Tasks>> getAllTasks()
        {
            string url = "http://localhost:3000/tasks/all";
            List<Tasks> tasksList = new List<Tasks>();
            
            using (HttpResponseMessage response = await ClientHttp.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    string jsonResult = await response.Content.ReadAsStringAsync();
                    tasksList = JsonConvert.DeserializeObject<List<Tasks>>(jsonResult);
                    return tasksList;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
