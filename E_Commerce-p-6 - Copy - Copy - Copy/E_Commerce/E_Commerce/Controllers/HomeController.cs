using E_Commerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Commerce.Controllers
{
    public class HomeController : Controller
    {
        private ecommerceEntities db = new ecommerceEntities();

        public ActionResult Home()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
           
            return View ();
        }

        public ActionResult RemoveFromCart(int id) { 
        var cartItem = db.Carts.FirstOrDefault(c => c.product_id == id);
            if (cartItem != null) { 
            db.Carts.Remove(cartItem);
            db.SaveChanges();
            
            
            }

        
        return RedirectToAction("Cart", "Category");  
        
        
        }
    }
}