using IntecWebShop.Core.Interfaces;
using IntecWebShop.DataAccess.InMemory.Repositories;
using IntecWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntecWebShopp.WebUI.Controllers
{
    public class CategoryManagerController : Controller
    {
        IRepository<ProductCategory> context;

        public CategoryManagerController(IRepository<ProductCategory> categoryContext)
        {
            //context = new InMemoryRepository<ProductCategory>();
            context = categoryContext;
        }    

        public ActionResult Index()
        {
            List<ProductCategory> categories = context.Collection().ToList();
            return View(categories);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ProductCategory category = new ProductCategory();
            return View(category);
        }

        [HttpPost]
        public ActionResult Create(ProductCategory cat)     // ATTENTION objet cat doit avoir un nom different de la prop category 
        {
            if (ModelState.IsValid == false)        // check si le model est completé correctement 
            {
                return View(cat);
            }
            context.Insert(cat);
            context.Commit();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            // trouver le produit 
            var productToDelete = context.FindById(id);

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
            var productToUpdate = context.FindById(id);

            return View(productToUpdate);
        }

        [HttpPost]
        public ActionResult Edit(ProductCategory cat)
        {
            context.Update(cat);
            //context.Delete(product.Id);     //delete old product
            //context.Insert(product);        // insert new
            context.Commit();
            return RedirectToAction("Index");


        }
    }
}