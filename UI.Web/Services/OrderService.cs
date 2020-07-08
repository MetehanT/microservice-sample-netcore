using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UI.Web.Helpers;
using UI.Web.Models;
using UI.Web.Models.Order;

namespace UI.Web.Services
{
	public class OrderService : IOrderService
	{

		private HttpClient _httpClient;

		public OrderService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public void Add(OrderCreate order)
		{
			//var jsonOrder = JsonConvert.SerializeObject(order);
			//StringContent content = new StringContent(jsonOrder, Encoding.UTF8, "application/json");
			//await _httpClient.PostAsync("http://172.20.0.1:5000/api/orders", content);

			EventBus.OrderGenerate(order);
		}

		public async Task<List<OrderList>> GetOrders()
		{
			StringContent c = new StringContent("");
			await _httpClient.PostAsync("http://172.27.240.1:5000/api/orders", c);

			using (var response = await _httpClient.GetAsync("http://172.27.240.1:5000/api/orders"))
			{
				var jsonOrders = await response.Content.ReadAsStringAsync();

				var orders = JsonConvert.DeserializeObject<List<OrderList>>(jsonOrders);
				return orders;
			}
		}

		public async Task<List<OrderList>> GetOrdersByUserId(int userId)
		{
			using (var response = await _httpClient.GetAsync("http://172.27.240.1:5000/api/orders/" + userId))
			{
				var jsonOrders = await response.Content.ReadAsStringAsync();

				var orders = JsonConvert.DeserializeObject<List<OrderList>>(jsonOrders);
				return orders;
			}
		}

	}
}
