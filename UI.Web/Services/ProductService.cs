using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using UI.Web.Models;

namespace UI.Web.Services
{
	public class ProductService : IProductService
	{
		private HttpClient _httpClient;

		public ProductService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}
		//------------- Önemli -----------------//
		//Normalde web ui da http client ile istek atarken localhostu ve ocelot portunu kullanırız örn.: http://localhost:5000/api/product
		//docker compose ile çalıştırırken istek atmamız gereken adres docker makinede çalışan ip si olmalı örn: http://172.20.0.1:5000/api/products/
		// ** hyper-v virtual ethernet adapter #2 <<<< kendim için not
		public async Task Add(ProductCreate productCreate)
		{
			var jsonProduct = JsonConvert.SerializeObject(productCreate);
			StringContent content = new StringContent(jsonProduct, Encoding.UTF8, "application/json");
			await _httpClient.PostAsync("http://172.27.240.1:5000/api/products", content);
		}

		public async Task<ProductListViewModel> GetProductById(int productId)
		{
			using (var response = await _httpClient.GetAsync("http://172.27.240.1:5000/api/products/" + productId))
			{
				var jsonResponse = await response.Content.ReadAsStringAsync();

				var product = JsonConvert.DeserializeObject<ProductListViewModel>(jsonResponse);
				return product;
			}
		}

		public async Task<IEnumerable<ProductListViewModel>> GetProducts()
		{
			using (var response = await _httpClient.GetAsync("http://172.27.240.1:5000/api/products")) {
				var jsonResponse = await response.Content.ReadAsStringAsync();
				var products = JsonConvert.DeserializeObject<List<ProductListViewModel>>(jsonResponse);
				return products;
			}
		}

		public async Task Save(ProductListViewModel productListViewModel)
		{
			var jsonProduct = JsonConvert.SerializeObject(productListViewModel);
			StringContent content = new StringContent(jsonProduct, Encoding.UTF8, "application/json");
			await _httpClient.PutAsync("http://172.27.240.1:5000/api/products", content);
		}
	}
}
