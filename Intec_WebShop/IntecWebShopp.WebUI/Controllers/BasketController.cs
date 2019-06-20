using IntecWebShop.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntecWebShopp.WebUI.Controllers
{

    public class BasketController : Controller
    {
        IBasketService BasketService;

        public BasketController(IBasketService basketService)
        {
            this.BasketService = basketService;
        }

        // GET: Services
        public ActionResult Index()
        {
            var model = BasketService.GetBasketItem(HttpContext);
            return View(model);
        }

        public ActionResult AddToBasket(string Id)
        {
            BasketService.AddToBasket(HttpContext, Id);
            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromBasket(string Id)
        {
            BasketService.RemoveFromBasket(HttpContext, Id);
            return RedirectToAction("Index");
        }

        public PartialViewResult BasketSummary()
        {
            var basketsummary = BasketService.GetBasketSummary(HttpContext);
            return PartialView(basketsummary);
        }
    }
}