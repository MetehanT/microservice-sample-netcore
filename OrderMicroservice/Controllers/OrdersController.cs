using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderMicroservice.Models;
using OrderMicroservice.Repository;

namespace OrderMicroservice.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrdersController : ControllerBase
	{

		private IOrderRepository _orderRepository;

		public OrdersController(IOrderRepository orderRepository)
		{
			_orderRepository = orderRepository;
		}
		[HttpGet]
		public IActionResult Get()
		{
			var orders = _orderRepository.GetOrders();
			return new OkObjectResult(orders);
		}

		[HttpGet("{id}")]
		public IActionResult GetOrdersByUserId(int id)
		{
			var orders = _orderRepository.GetOrdersByUserId(id);
			return new OkObjectResult(orders);
		}

		[HttpPost]
		public IActionResult Post()
		{
			_orderRepository.InsertOrder(); //Normalde repository desenei ile database veri ekleme işlemini aşağıdaki fonk gibi yapıyorduk
			return Ok();                    //rabbitmq işlemlerini izlemek için böyle bir yol seçtik
		}

		/*[HttpPost]
		public IActionResult Post([FromBody] Orders order)
		{
			using (var scope = new TransactionScope())
			{
				_orderRepository.InsertOrder(order);
				scope.Complete();
				return Ok();
			}
		}*/
	}
}