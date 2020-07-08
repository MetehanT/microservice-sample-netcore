using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Web.Models
{
	public class OrderCreate
	{
		public int UserId { get; set; }
		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Adress { get; set; }
		[Column(TypeName = "decimal(18,2)")]
		public decimal Price { get; set; }

	}
}
