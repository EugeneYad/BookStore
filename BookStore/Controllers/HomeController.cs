using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;
using log4net;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(HomeController));

        BookContext db = new BookContext();

	    public ActionResult Index()
        {
            //log4net.Config.XmlConfigurator.Configure();
            //logger.Error("Test msg");
            // получаем из бд все объекты Book

            //var storageACcc =
            //    CloudStorageAccount.Parse(
            //        "DefaultEndpointsProtocol=https;AccountName=storage0acc;AccountKey=B5PhADN8FLKMLhm2IicTqI/JoAaR3s3ayqOG+okceAV9Lkm02LHxsoOWF7RNf/bY9dMI1Q9g+yuA0Ls6wIj7Rg==;EndpointSuffix=core.windows.net");

            //var blobClient = new CloudBlobClient(new Uri(@"https://storage0acc.blob.core.windows.net"),
            //    storageACcc.Credentials);
            //CloudBlobContainer container = blobClient.GetContainerReference("testblob");
            //container.CreateIfNotExists();
            //CloudBlockBlob blob = container.GetBlockBlobReference("newTextfile.txt");
            //blob.UploadText("any_content_you_want");
            IEnumerable<Book> books = db.Books;
            return View(books);
        }

        [HttpGet]
        public ActionResult Buy(int id)
        {
            ViewBag.BookId = id;
            ViewBag.Message = "Это вызов частичного представления из обычного";
            SelectList books = new SelectList(db.Books, "Author", "Name");
            return View(books);
        }

        [HttpGet]
        public ActionResult BookView(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Book book = db.Books.Find(id);
            if (book != null)
            {
                return View(book);
            }
            return HttpNotFound();
        }


        [HttpGet]
        public ActionResult EditBook(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Book book = db.Books.Find(id);
            if (book != null)
            {
                return View(book);
            }
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult EditBook(Book book)
        {
            db.Entry(book).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public string Buy(Purchase purchase)
        {
            purchase.Date = DateTime.Now;
            // добавляем информацию о покупке в базу данных
            db.Purchases.Add(purchase);
            // сохраняем в бд все изменения
            db.SaveChanges();
            return "Спасибо," + purchase.Person + ", за покупку!";
        }

        public ActionResult Partial()
        {
            ViewBag.Message = "Это вызов частичного представления из обычного";
            return PartialView();
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Book book)
        {
            db.Entry(book).State = EntityState.Added;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Book b = db.Books.Find(id);
            if (b == null)
            {
                return HttpNotFound();
            }
            return View(b);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Book b = db.Books.Find(id);
            if (b == null)
            {
                return HttpNotFound();
            }
            db.Books.Remove(b);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

		protected override void Dispose(bool disposing)
		{
			if (db != null)
			{
				db.Dispose();
				db = null;
			}
			base.Dispose(disposing);
		}
	}
}