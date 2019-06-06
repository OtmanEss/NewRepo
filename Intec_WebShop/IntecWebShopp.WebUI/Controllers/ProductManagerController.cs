using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IntecWebShop.DataAccess.InMemory.Repositories;
using IntecWebShop.Models;



namespace IntecWebShopp.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        public ProductManagerController()
        {
            context = new ProductRepository();
        }

        ProductRepository context;

        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();

            return View(products);
        }

        [HttpGet]
        public ActionResult Create()
        {
            Product product = new Product();
            return View(product);
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid == false)        // check si le model est complete correctement 
            {
                return View(product);
            }
            context.Insert(product);
            context.Commit();
            return RedirectToAction("Index");

        }
    }
}