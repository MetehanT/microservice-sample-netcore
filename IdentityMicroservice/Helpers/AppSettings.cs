using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityMicroservice.Helpers
{
	public class AppSettings
	{
		//Appsettings.json dosyasındaki secret keyi maplemek için bu classı oluşturduk
		public string SecretKey { get; set; }
	}
}
