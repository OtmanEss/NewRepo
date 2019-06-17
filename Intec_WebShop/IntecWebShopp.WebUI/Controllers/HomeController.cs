using IntecWebShop.Core.Interfaces;
using IntecWebShop.Models;
using IntecWebShop.ViewModels;
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
        
        public ActionResult Index(string cat=null)
        {
            List<Product> products;
            List<ProductCategory> productCategories = productCategoryContext.Collection().ToList();

            // si pas de category. show all products        
            if (productCategories==null)
            {
                products = context.Collection().ToList();
            }

            //else show product where category is selected
            else
            {
                products = context.Collection().Where(c => c.Category == cat).ToList();
            }

            ProductListViewModel model = new ProductListViewModel();
            model.Products = products;
            model.Categories = productCategories;

            return View(model);
        }

        public ActionResult Details (string id)
        {
            var product = context.FindById(id);
            if (product==null)
            {
                return HttpNotFound();
            }
            return View(product);
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