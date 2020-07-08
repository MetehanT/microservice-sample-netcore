using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.Web.Models;

namespace UI.Web.Services
{
	public interface IProductService
	{
		Task<IEnumerable<ProductListViewModel>> GetProducts();

		Task Add(ProductCreate productCreate);

		Task Save(ProductListViewModel productListViewModel);

		Task<ProductListViewModel> GetProductById(int productId);
	}
}
