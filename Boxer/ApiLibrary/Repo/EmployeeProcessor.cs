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

            using (HttpResponseMessage response = await ClientHttp.ApiClient.GetAsync(url).ConfigureAwait(false))
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

        public static async Task<Employee> getEmployee(string uid)
        {
            string url = "http://localhost:4000/employee/" + uid;
            Employee employee = new Employee();

            using (HttpResponseMessage response = await ClientHttp.ApiClient.GetAsync(url).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    string jsonResult = await response.Content.ReadAsStringAsync();
                    employee = JsonConvert.DeserializeObject<Employee>(jsonResult);
                    return employee;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<string> addEmployee(Employee employee)
        {
            string url = "http://localhost:4000/employee/add";
            string result;
            string serializedEmployee = JsonConvert.SerializeObject(employee);

            using (HttpResponseMessage response = await ClientHttp.ApiClient
                .PostAsync(url, new StringContent(serializedEmployee, Encoding.UTF8, "application/json")).ConfigureAwait(false))
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

        public static async Task<string> updateEmployee(Employee employee)
        {
            string url = "http://localhost:4000/employee/edit/" + employee.uid;
            string result;
            string serializedEmployee = JsonConvert.SerializeObject(employee);

            using (HttpResponseMessage response = await ClientHttp.ApiClient
                .PutAsync(url, new StringContent(serializedEmployee, Encoding.UTF8, "application/json")).ConfigureAwait(false))
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

        public static async Task<string> deleteEmployee(string uid)
        {
            string url = "http://localhost:4000/employee/delete/" + uid;
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
