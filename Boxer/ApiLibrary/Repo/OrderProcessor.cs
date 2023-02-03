using ApiLibrary.Model;
using ApiLibrary.Model.ToCreate;
using ApiLibrary.Model.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrary.Repo
{
    public class OrderProcessor
    {
        public static async Task<List<Order>> getAllOrders(Order order)
        {
            string url = "http://localhost:3000/orders";
            List<Order> ordersList = new List<Order>();
            string serializedOrder = JsonConvert.SerializeObject(order);

            using (HttpResponseMessage response = await ClientHttp.ApiClient
                .PostAsync(url, new StringContent(serializedOrder, Encoding.UTF8, "application/json")).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    string jsonResult = await response.Content.ReadAsStringAsync();
                    ordersList = JsonConvert.DeserializeObject<List<Order>>(jsonResult);
                    return ordersList;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<List<Order>> getOrdersNotConnected(Order order)
        {
            string url = "http://localhost:3000/orders/not_connected";
            List<Order> ordersList = new List<Order>();
            string serializedOrder = JsonConvert.SerializeObject(order);

            using (HttpResponseMessage response = await ClientHttp.ApiClient
                .PostAsync(url, new StringContent(serializedOrder, Encoding.UTF8, "application/json")).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    string jsonResult = await response.Content.ReadAsStringAsync();
                    ordersList = JsonConvert.DeserializeObject<List<Order>>(jsonResult);
                    return ordersList;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<Order> getOrder(Order order)
        {
            string url = "http://localhost:3000/order/get/" + order.id;
            string serializedOrder = JsonConvert.SerializeObject(order);

            using (HttpResponseMessage response = await ClientHttp.ApiClient
                .PostAsync(url, new StringContent(serializedOrder, Encoding.UTF8, "application/json")).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    string jsonResult = await response.Content.ReadAsStringAsync();
                    order = JsonConvert.DeserializeObject<Order>(jsonResult);
                    return order;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<string> addOrder(ToCreateOrder order)
        {
            string url = "http://localhost:3000/order/add";
            string result;
            string serializedOrder = JsonConvert.SerializeObject(order);

            using (HttpResponseMessage response = await ClientHttp.ApiClient
                .PostAsync(url, new StringContent(serializedOrder, Encoding.UTF8, "application/json")).ConfigureAwait(false))
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

        public static async Task<string> updateOrder(ToCreateOrder order)
        {
            string url = "http://localhost:3000/order/edit/" + order.id;
            string result;
            string serializedOrder = JsonConvert.SerializeObject(order);

            using (HttpResponseMessage response = await ClientHttp.ApiClient
                .PutAsync(url, new StringContent(serializedOrder, Encoding.UTF8, "application/json")).ConfigureAwait(false))
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

        public static async Task<string> deleteOrder(Order order)
        {
            string url = "http://localhost:3000/order/delete/" + order.id;
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



        public static async Task<List<OrderItem>> getOrderItems(OrderItem orderItem)
        {
            string url = "http://localhost:3000/order_items/get_from_order/" + orderItem.order_id;
            List<OrderItem> orderItemsList = new List<OrderItem>();
            string serializedOrderItem = JsonConvert.SerializeObject(orderItem);

            using (HttpResponseMessage response = await ClientHttp.ApiClient
                .PostAsync(url, new StringContent(serializedOrderItem, Encoding.UTF8, "application/json")).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    string jsonResult = await response.Content.ReadAsStringAsync();
                    orderItemsList = JsonConvert.DeserializeObject<List<OrderItem>>(jsonResult);
                    return orderItemsList;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
        public static async Task<List<OrderItemDetailed>> getOrderItemsDetailed(OrderItem orderItem)
        {
            string url = "http://localhost:3000/order_items/get_from_order/" + orderItem.order_id;
            List<OrderItemDetailed> orderItemsList = new List<OrderItemDetailed>();
            string serializedOrderItem = JsonConvert.SerializeObject(orderItem);

            using (HttpResponseMessage response = await ClientHttp.ApiClient
                .PostAsync(url, new StringContent(serializedOrderItem, Encoding.UTF8, "application/json")).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    string jsonResult = await response.Content.ReadAsStringAsync();
                    orderItemsList = JsonConvert.DeserializeObject<List<OrderItemDetailed>>(jsonResult);
                    return orderItemsList;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<string> addOrderItem(OrderItem orderItem)
        {
            string url = "http://localhost:3000/order_item/add";
            string result;
            string serializedOrderItem = JsonConvert.SerializeObject(orderItem);

            using (HttpResponseMessage response = await ClientHttp.ApiClient
                .PostAsync(url, new StringContent(serializedOrderItem, Encoding.UTF8, "application/json")).ConfigureAwait(false))
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

        public static async Task<string> editOrderItem(OrderItem orderItem)
        {
            string url = "http://localhost:3000/order_item/edit/" + orderItem.id;
            string result;
            string serializedOrderItem = JsonConvert.SerializeObject(orderItem);

            using (HttpResponseMessage response = await ClientHttp.ApiClient
                .PutAsync(url, new StringContent(serializedOrderItem, Encoding.UTF8, "application/json")).ConfigureAwait(false))
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


        public static async Task<string> deleteOrderItem(OrderItem orderItem)
        {
            string url = "http://localhost:3000/order_item/delete/" + orderItem.id;
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
