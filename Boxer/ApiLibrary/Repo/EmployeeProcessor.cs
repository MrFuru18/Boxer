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

        public static async Task<Employee> addEmployee(Employee employee)
        {
            string url = "http://localhost:4000/employee/add";
            Employee result = null;
            string serializedEmployee = JsonConvert.SerializeObject(employee);

            using (HttpResponseMessage response = await ClientHttp.ApiClient
                .PostAsync(url, new StringContent(serializedEmployee, Encoding.UTF8, "application/json")).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    string jsonResult = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<Employee>(jsonResult);
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
