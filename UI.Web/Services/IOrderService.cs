using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.Web.Models;
using UI.Web.Models.Order;

namespace UI.Web.Services
{
	public interface IOrderService
	{
		Task<List<OrderList>> GetOrders();

		Task<List<OrderList>> GetOrdersByUserId(int userId);

		void Add(OrderCreate order);
	}
}
