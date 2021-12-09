using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StoreFront.DATA.EF;
using System.ComponentModel.DataAnnotations;

namespace StoreFront.Models
{
    public class CartItemViewModel
    {
        //fields

        //props
        [Range(1, int.MaxValue)]//ensures the values are greater than 1 when an item is added to the cart
        public int Qty { get; set; }

        public Product Product { get; set; }
        //ctors
        public CartItemViewModel(int qty, Product product)
        {
            //Property = param;
            Qty = qty;
            Product = product;
        }
        //methods
    }
}