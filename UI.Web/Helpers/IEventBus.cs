using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.Web.Models;

namespace UI.Web.Helpers
{
	public interface IEventBus
	{
		void OrderGenerate(OrderCreate order);
	}
}
