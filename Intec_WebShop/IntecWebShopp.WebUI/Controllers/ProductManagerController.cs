using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IntecWebShop.Core.Interfaces;
using IntecWebShop.Core.Models;
using IntecWebShop.Core.ViewModels;
using IntecWebShop.DataAccess.InMemory.Repositories;


namespace IntecWebShopp.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        IRepository<Product> context;
        IRepository<ProductCategory> productCategoryContext;



        public ProductManagerController(IRepository<Product> productContext, IRepository<ProductCategory> categoryContext)
        {
            context = productContext;
            productCategoryContext = categoryContext;

            //productCategoryContext = new InMemoryRepository<ProductCategory>();       // acces a toutes les fonctionnalites de productcategory
            //context = new InMemoryRepository<Product>();                              // acces a toutes les fct de product

        }



        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();

            return View(products);
        }

        [HttpGet]
        public ActionResult Create()
        {
            // utilise le viewmodel(combinaison de deux classes)
            //creer un new objet
            ProductManagerViewModel viewModel = new ProductManagerViewModel();
            viewModel.Product = new Product();
            viewModel.productCategories = productCategoryContext.Collection();
            //Product product = new Product();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(Product product, HttpPostedFileBase file)
        {
            if (ModelState.IsValid == false)        // check si le model est completé correctement 
            {
                return View(product);
            }
            else
            {
                if (file != null)
                {
                    product.Image = product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages/") + product.Image);
                }
                context.Insert(product);
                context.Commit();
            }
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

            if (productToUpdate == null)
            {
                return HttpNotFound();
            }

            ProductManagerViewModel viewModel = new ProductManagerViewModel();
            viewModel.Product = productToUpdate;
            viewModel.productCategories = productCategoryContext.Collection();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    product.Image = product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages/") + product.Image);
                }
                    context.Update(product);
                    //context.Delete(product.Id);     //delete old product
                    //context.Insert(product);        // insert new
                    context.Commit();
            }
            return RedirectToAction("Index");
        }


    }
}