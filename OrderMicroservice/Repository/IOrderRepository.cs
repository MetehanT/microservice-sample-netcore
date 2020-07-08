using OrderMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderMicroservice.Repository
{
	public interface IOrderRepository
	{
		List<Orders> GetOrders();

		List<Orders> GetOrdersByUserId(int UserId); //Bir kullanıcı birden fazla sipariş verebileceği için liste şeklinde tutuyoruz

		//void InsertOrder(Orders order);
		void InsertOrder();

		void Save();
	}
}
