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
            if (ModelState.IsValid == false)        // check si le model est completé correctement 
            {
                return View(product);
            }
            context.Insert(product);
            context.Commit();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            // trouver le produit 
            var productToDelete = context.FindProduct(id);
           
                // affiche le product to delete
                return View(productToDelete);           
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string id)
        {
            var productToDelete = context.Delete(id);
            // si product to delete don't exist
            if (productToDelete == false)
            {
                return HttpNotFound();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {           
            // find the product
            var productToUpdate = context.FindProduct(id);

            return View(productToUpdate);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            context.Update(product);
            //context.Delete(product.Id);     //delete old product
            //context.Insert(product);        // insert new
            context.Commit();
            return RedirectToAction("Index");

            
        }
       
       
    }
}