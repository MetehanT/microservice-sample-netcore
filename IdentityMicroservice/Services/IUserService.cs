using IdentityMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityMicroservice.Services
{
	public interface IUserService
	{
		(string username, string token)? Authenticate(string username, string password);
	}
}
