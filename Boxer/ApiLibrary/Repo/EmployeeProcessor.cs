using ApiLibrary.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrary.Repo
{
    public class EmployeeProcessor
    {
        public static async Task<List<Employee>> getAllEmployees()
        {
            string url = "http://localhost:4000/employees";
            List<Employee> employeesList = new List<Employee>();

            using (HttpResponseMessage response = await ClientHttp.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    string jsonResult = await response.Content.ReadAsStringAsync();
                    employeesList = JsonConvert.DeserializeObject<List<Employee>>(jsonResult);
                    return employeesList;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
