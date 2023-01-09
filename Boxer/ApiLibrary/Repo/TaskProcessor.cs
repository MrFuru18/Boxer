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
            
            using (HttpResponseMessage response = await ClientHttp.ApiClient.GetAsync(url).ConfigureAwait(false))
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

        public static async Task<Tasks> getTasks(int id)
        {
            string url = "http://localhost:3000/task/" + id;
            Tasks task = new Tasks();

            using (HttpResponseMessage response = await ClientHttp.ApiClient.GetAsync(url).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    string jsonResult = await response.Content.ReadAsStringAsync();
                    task = JsonConvert.DeserializeObject<Tasks>(jsonResult);
                    return task;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<string> addTasks(Tasks task)
        {
            string url = "http://localhost:3000/task/add";
            string result;
            string serializedTasks = JsonConvert.SerializeObject(task);

            using (HttpResponseMessage response = await ClientHttp.ApiClient
                .PostAsync(url, new StringContent(serializedTasks, Encoding.UTF8, "application/json")).ConfigureAwait(false))
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

        public static async Task<string> updateTasks(Tasks task)
        {
            string url = "http://localhost:3000/task/edit/" + task.id;
            string result;
            string serializedTasks = JsonConvert.SerializeObject(task);

            using (HttpResponseMessage response = await ClientHttp.ApiClient
                .PutAsync(url, new StringContent(serializedTasks, Encoding.UTF8, "application/json")).ConfigureAwait(false))
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

        public static async Task<string> deleteTasks(int id)
        {
            string url = "http://localhost:3000/task/delete/" + id;
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
