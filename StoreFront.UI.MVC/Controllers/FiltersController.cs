using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreFront.DATA.EF;//Added for db connection
using PagedList;//Added for Paging functionality

namespace StoreFront.Controllers
{
    public class FiltersController : Controller
    {
        //Create a connection instance to the BookStorePlus db
        private StoreFrontEntities db = new StoreFrontEntities();

        // GET: Filters
        public ActionResult Index()
        {
            return View();
        }//end Index

        public ActionResult BrandQS(string searchFilter)
        {
            #region Optional QueryString Search
            if (String.IsNullOrEmpty(searchFilter))
            {
                var brand = db.Brands;
                return View(brand.ToList());
            }
            else
            {
                //If the optional search is Used, then we will filter results from the database by those criteria
                string searchUpCase = searchFilter.ToUpper();

                //Linq method syntax
                List<Brand> searchResults = db.Brands.Where(a => a.BrandName.ToUpper().Contains(searchUpCase)).ToList();
                return View(searchResults);
            }
            #endregion
        }//end ActionResult

        public ActionResult ProductsMVCPaging(string searchString, int page = 1)
        {
            //This sets the number of items per 'page'
            int pageSize = 5;//This sets the number of items(Books) per page to 5

            var products = db.Products.OrderBy(b => b.ProductName).ToList();

            #region Search functionality
            if (!String.IsNullOrEmpty(searchString))
            {
                products = (
                    from b in products
                    where b.ProductName.ToLower().Contains(searchString.ToLower())
                    select b).ToList();
            }

            ViewBag.SearchString = searchString;
            #endregion

            return View(products.ToPagedList(page, pageSize));
        }//end ActionResult

    }//end class
}//end namespace