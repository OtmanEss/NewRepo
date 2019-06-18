using IntecWebShop.Core.Interfaces;
using IntecWebShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace IntecWebShop.Services.ServiceModels
{
    public class BasketService
    {
        IRepository<Product> productContext;
        IRepository<Basket> basketContext;

        public const string BasketSessionName = "eCommerceBasket ";

        public BasketService(IRepository<Product> productcontext, IRepository<Basket> basketcontext)
        {
            this.productContext = productcontext;
            this.basketContext = basketcontext;
        }

        //httpcontext utile pour toute les requetes entre client et service
        private Basket GetBasket(HttpContextBase httpContext, bool createIfNull)
        {
            HttpCookie cookie = httpContext.Request.Cookies.Get(BasketSessionName);
            Basket basket = new Basket();
            if (cookie!=null)       // cookie existe deja avec des infos 
            {
                string basketId = cookie.Value;
                if (string.IsNullOrEmpty(basketId)==false)      // si basketid is Not null or empty cad qu'il existe deja 
                {
                    basket = basketContext.FindById(basketId);      // si existe on le recherche
                }
                else
                {
                    if (createIfNull)
                    {
                        basket = CreateNewBasket(httpContext);      // on cree 
                    }
                }
            }
            else
            {
                basket = CreateNewBasket(httpContext);
            }

            return basket;
        }

        private Basket CreateNewBasket(HttpContextBase httpContext)
        {
            Basket basket = new Basket();
            basketContext.Insert(basket);
            basketContext.Commit();

            HttpCookie cookie = new HttpCookie(BasketSessionName);
            cookie.Value = basket.Id;
            cookie.Expires = DateTime.Now.AddDays(2);

            return basket;

        }

        public void AddToBasket(HttpContextBase httpContext , string prodId)
        {
            Basket basket = GetBasket(httpContext, true);
            BasketItem item = basket.BasketItems.FirstOrDefault(b => b.ProductId == prodId);
            if (item==null)
            {
                item = new BasketItem()
                {
                    BasketId = basket.Id,
                    ProductId = prodId,
                    Quantity = 1

                };
                basket.BasketItems.Add(item);
            }
            else
            {
                item.Quantity = item.Quantity + 1;
            }

            basketContext.Commit();
        }

        public void RemoveFromBasket(HttpContextBase httpContext, string prodId)
        {
            Basket basket = GetBasket(httpContext, true);
            BasketItem item = basket.BasketItems.FirstOrDefault(i => i.Id == prodId);

            if (item!=null)
            {
                basket.BasketItems.Remove(item);
                basketContext.Commit();
            }
        }
    }
}
