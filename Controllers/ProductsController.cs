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
        public ActionResult Index(string Search = "", string SortColumn="ProductName", string IconClass="fa-sort-asc")
        {
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
            ViewBag.value = Search;
            List<Product> products = db.Products.Where(temp => temp.ProductName.Contains(Search)).ToList();
            ViewBag.SortColumn = SortColumn;
            ViewBag.IconClass = IconClass;
            if (ViewBag.SortColumn == "ProductID")
            {
                if (ViewBag.IconClass == "fa-sort-asc")
                    products = products.OrderBy(temp => temp.ProductID).ToList();
                else
                 products = products.OrderByDescending(temp => temp.ProductID).ToList();
            }
            
            return View(products);
        }

        public ActionResult Details(long id)
        {
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
            Product p = db.Products.Where(temp => temp.ProductID == id).FirstOrDefault();
            return View(p);
        }

        public ActionResult Create()
        {
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
            ViewBag.category = db.Categories.ToList();
            ViewBag.brand = db.Brands.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product p)
        {
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
            db.Products.Add(p);
            db.SaveChanges();
            return RedirectToAction("index");
        }

        public ActionResult Edit(long id)
        {
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
            Product currentProduct = db.Products.Where(temp => temp.ProductID == id).FirstOrDefault();
            ViewBag.brand = db.Brands.ToList();
            ViewBag.category = db.Categories.ToList();
            return View(currentProduct);
        }

        [HttpPost]
        public ActionResult Edit( Product p)
        {
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
            Product currentProduct = db.Products.Where(temp => temp.ProductID == p.ProductID).FirstOrDefault();
            currentProduct.ProductName = p.ProductName;
            currentProduct.Price = p.Price;
            currentProduct.DateOfPurchase = p.DateOfPurchase;
            currentProduct.CategoryID = p.CategoryID;
            currentProduct.BrandID = p.BrandID;
            currentProduct.AvailabilityStatus = p.AvailabilityStatus;
            currentProduct.Active = p.Active;
            db.SaveChanges();

            return RedirectToAction("index");
        }

        public ActionResult Delete(long id)
        {
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
            Product currentProduct = db.Products.Where(temp => temp.ProductID == id).FirstOrDefault();
            return View(currentProduct);
        }

        [HttpPost]
        public ActionResult Delete(long id, Product p)
        {
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
            Product currentProduct = db.Products.Where(temp => temp.ProductID == id).FirstOrDefault();
            db.Products.Remove(currentProduct);
            db.SaveChanges();
            return RedirectToAction("index");
        }
    }
}