using Newtonsoft.Json;
using OrderMicroservice.DBContexts;
using OrderMicroservice.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMicroservice.Repository
{
	public class OrderRepository : IOrderRepository
	{

		private OrderContext _orderContext;

		public OrderRepository(OrderContext orderContext)
		{
			_orderContext = orderContext;
		}

		public List<Orders> GetOrdersByUserId(int UserId)
		{
			List<Orders> orders = _orderContext.Orders.Where(o => o.UserId == UserId).ToList();
			return orders;
		}

		public List<Orders> GetOrders()
		{
			return _orderContext.Orders.ToList();
		}

		public void InsertOrder()
		{
			
			var factory = new ConnectionFactory() { HostName = "172.27.240.1", UserName = "admin", Password = "password" };
			using (IConnection connection = factory.CreateConnection())
			using (IModel channel = connection.CreateModel())
			{
				//Received methoduyla gelen dataları yakalayıp işlem yapacağımız için EventingBasicConsumer classından nesne alıyoruz.
				var consumer = new EventingBasicConsumer(channel);
				//Yeni data geldiğinde bu event otomatik tetikleniyor.
				consumer.Received += (model, ea) =>
				{
					var body = ea.Body.ToArray();//Kuyruktaki içerik bilgisi.
					var message = Encoding.UTF8.GetString(body);//Gelen bodyi stringe çeviriyoruz.
					Orders order = JsonConvert.DeserializeObject<Orders>(message); //Mesajdan dönen veriyi classa çeviriyoruz.
					_orderContext.Add(order);//Contextimize gönderiyoruz
											 //Normalde Action resulttan gelen order ile direk gönderiyorduk. Şimdi rabbitmq dan alıyoruz.
				};
				channel.BasicConsume(queue: "Order", //Consume edilecek kuyruk ismi
					autoAck: true, //Kuyruk ismi doğrulansın mı
					consumer: consumer); //Hangi consumer kullanılacak.

			}
			//_orderContext.Add(order);
			Save();
		}

		public void Save()
		{
			_orderContext.SaveChanges();
		}

	}
}
