using IntecWebShop.Core.Interfaces;
using IntecWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntecWebShopp.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IRepository<Product> context;
        IRepository<ProductCategory> productCategoryContext;

        public HomeController(IRepository<Product> productContext, IRepository<ProductCategory> categoryContext)
        {
            context = productContext;
            productCategoryContext = categoryContext;      
        }
        
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}