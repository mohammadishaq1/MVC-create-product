using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using p6.Models;
namespace p6.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult Index(string Search = "")
        {
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
            ViewBag.value = Search;
            List<Product> products = db.Products.Where(temp=>temp.ProductName.Contains(Search)).ToList();
            return View(products);
        }

        public ActionResult Details(long id)
        {
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
            Product p = db.Products.Where(temp => temp.ProductID == id).FirstOrDefault();
            return View(p);
        }
    }
}