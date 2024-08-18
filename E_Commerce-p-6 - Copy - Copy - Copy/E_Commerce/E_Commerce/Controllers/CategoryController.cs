using E_Commerce.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Commerce.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        private ecommerceEntities db = new ecommerceEntities();

        public ActionResult Products()
        {
            //// Get the categories and products to display on the Category page
            //var categories = db.Categories.ToList();
            //var products = db.Products.ToList();

            //// Pass both categories and products to the View using a ViewModel
            //var model = new Category
            //{
            //    Categories = categories,
            //    Products = products
            //};
            var proucts = db.Products.ToList();
            return View(proucts);
        }

        public ActionResult Cart()

        {
            //if (Session["UserID"] == null)
            //{
            //    return RedirectToAction("login", "Users");
            //}
            //int userID = (int)Session["UserID"];

            var cartItems = db.Carts.Include(c => c.Product).ToList();
            var products = cartItems.Select(c => c.Product).ToList();
            return View(products);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult singleProductDetails(int? id)
        {
            var cart = db.Products.Where(x=>x.id == id).FirstOrDefault();
            //var cartItems = db.Carts.ToList();
            return View(cart);
        }



        public ActionResult AddToCart(int productId, int Quantity)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("login", "Users");
            }
            int userID = (int)Session["UserId"];
            var product = db.Products.Find(productId);
            if (product != null)
            {
                var cartItem = db.Carts.FirstOrDefault(p => p.Users_id == userID && p.product_id == productId);
                if (cartItem == null)
                {
                    var cartItemm = new Cart
                    {
                        product_id = productId,
                        quantity = Quantity,
                        Users_id = userID

                    };
                    db.Carts.Add(cartItemm); 
                }
                else
                {
                    cartItem.quantity += Quantity;
                }
                db.SaveChanges();
            }

            return RedirectToAction("Cart");
        }

    }
}