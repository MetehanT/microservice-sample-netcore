using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UI.Web.Models;
using UI.Web.Services;

namespace UI.Web.Controllers
{
    public class OrderController : Controller
    {

        IOrderService _orderService;
        IProductService _productService;

        public OrderController(IOrderService orderService, IProductService productService)
        {
            _orderService = orderService;
            _productService = productService;
        }

        public IActionResult ListAdmin()
        {
            var response = _orderService.GetOrders().Result;
            return View(response);
        }

        public IActionResult ListUser()
        {
            var response = _orderService.GetOrdersByUserId(Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value)).Result;
            return View(response);
        }

        public IActionResult Buy(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Auth");
            }

            var product = _productService.GetProductById(id).Result;

            OrderCreate orderCreate = new OrderCreate();
            orderCreate.ProductId = id;
            orderCreate.ProductName = product.Name;
            orderCreate.Price = product.Price;
            return View(orderCreate);
        }

        [HttpPost]
        public IActionResult Buy(OrderCreate order)
        {
            order.UserId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            _orderService.Add(order);
            return RedirectToAction("Index", "Product");
        }
    }
}