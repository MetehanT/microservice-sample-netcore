using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using UI.Web.Models;
using UI.Web.Services;

namespace UI.Web.Controllers
{
    public class ProductController : Controller
    {
        IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            var response = _productService.GetProducts().Result;

            return View(response);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductCreate product)
        {
            _productService.Add(product);
            return View();
        }

        public IActionResult Edit(int id)
        {
            var response = _productService.GetProductById(id).Result;

            return View(response);
        }
        [HttpPost]
        public IActionResult Edit(ProductListViewModel productEdit)
        {
            _productService.Save(productEdit);
            return View();
        }
    }
}