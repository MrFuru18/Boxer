using ApiLibrary.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrary.Repo
{
    public class TaskProcessor
    {
        public static async Task<List<Tasks>> getAllTasks(Tasks task)
        {
            string url = "http://localhost:3000/tasks/all";
            List<Tasks> tasksList = new List<Tasks>();
            string serializedTask = JsonConvert.SerializeObject(task);

            using (HttpResponseMessage response = await ClientHttp.ApiClient
                .PostAsync(url, new StringContent(serializedTask, Encoding.UTF8, "application/json")).ConfigureAwait(false))
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

        public static async Task<Tasks> getTasks(Tasks task)
        {
            string url = "http://localhost:3000/task/get/" + task.id;
            string serializedTask = JsonConvert.SerializeObject(task);

            using (HttpResponseMessage response = await ClientHttp.ApiClient
                .PostAsync(url, new StringContent(serializedTask, Encoding.UTF8, "application/json")).ConfigureAwait(false))
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

        public static async Task<string> deleteTasks(Tasks task)
        {
            string url = "http://localhost:3000/task/delete/" + task.id;
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

        public static async Task<List<TaskState>> getTaskStates(TaskState taskState)
        {
            string url = "http://localhost:3000/task_states/get/" + taskState.task_id;
            List<TaskState> taskStatesList = new List<TaskState>();
            string serializedTaskState = JsonConvert.SerializeObject(taskState);

            using (HttpResponseMessage response = await ClientHttp.ApiClient
                .PostAsync(url, new StringContent(serializedTaskState, Encoding.UTF8, "application/json")).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    string jsonResult = await response.Content.ReadAsStringAsync();
                    taskStatesList = JsonConvert.DeserializeObject<List<TaskState>>(jsonResult);
                    return taskStatesList;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

    }
}
