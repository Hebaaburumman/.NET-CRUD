using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCCRUD.Controllers
{
    public class HomeController : Controller
    {
        productsContext _context = new productsContext();
        public ActionResult Index()
        {
            var listofData = _context.Products.ToList();
            return View(listofData);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Product model)
        {
            _context.Products.Add(model);
            _context.SaveChanges();
            ViewBag.Message = "data inserted successfully";

            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public ActionResult Edit(Product model)
        {
            if (ModelState.IsValid)
            {
                var product = _context.Products.FirstOrDefault(p => p.Id == model.Id);
                if (product != null)
                {
                    product.Name = model.Name;
                    product.Price = model.Price;
                    product.Description = model.Description;

                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return HttpNotFound();
                }
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }




        [HttpGet]
        public ActionResult Delete(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken] 
        public ActionResult DeleteConfirmed(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return HttpNotFound();
        }
    }
    }