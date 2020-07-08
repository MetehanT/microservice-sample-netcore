using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Web.Models.Order
{
	public class OrderList
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public string ProductName { get; set; }
		public string FirstName { get; set; }
		public decimal Price { get; set; }
		public string LastName { get; set; }
		public string Adress { get; set; }

	}
}
