using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreFront.Models;

namespace StoreFront.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        //We are going to generate this view w/ List of CIViewModel Objects [no data context class]
        public ActionResult Index()
        {
            //pull session based cart into a local variable that we can pass to this view.
            var shoppingCart = (Dictionary<int, CartItemViewModel>)Session["cart"];

            if (shoppingCart == null || shoppingCart.Count == 0)
            {
                //User either hasn't put anything in, or they removed all items from the cart, or session expired
                //set cart to an empty object ( can still send to the view... we would get errors if this object wasn't initialized
                shoppingCart = new Dictionary<int, CartItemViewModel>();

                //Create a message to pass via the ViewBag about the cart being empty
                ViewBag.Message = "There are no items in your cart.";
            }
            else
            {
                ViewBag.Message = null;//explicitly clears out that variable in case we have items in the cart.
            }
            return View(shoppingCart);
        }//end Index

        public ActionResult RemoveFromCart(int id)
        {
            //get the session variable and create a local variable to house that value in
            Dictionary<int, CartItemViewModel> shoppingCart = (Dictionary<int, CartItemViewModel>)Session["cart"];

            //remove the item form the local varialbe
            shoppingCart.Remove(id);

            //Update the session variable
            Session["cart"] = shoppingCart;

            return RedirectToAction("Index");
        }//end RemoveFromCart

        public ActionResult UpdateCart(int productID, int qty)
        {
            //get the session variable and create a local variable to house its value
            Dictionary<int, CartItemViewModel> shoppingCart = (Dictionary<int, CartItemViewModel>)Session["cart"];

            //target the correct cartItem using bookID for key - then change its qty
            shoppingCart[productID].Qty = qty;

            //return the local cart to session and send the user back to the index
            Session["cart"] = shoppingCart;

            return RedirectToAction("Index");
        }
    }
}