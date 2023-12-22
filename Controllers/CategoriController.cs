using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace MvcStok.Controllers
{
    public class CategoriController : Controller
    {
        // GET: Categori
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index(int sayfa=1)
        {
            //var degerler = db.TBLCATEGORILER.ToList();
            var degerler = db.TBLCATEGORILER.ToList().ToPagedList(sayfa, 4);
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniKategori()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniKategori(TBLCATEGORILER p1)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniKAtegori");
            }
            db.TBLCATEGORILER.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult SIL(int id)
        {
            var kategori = db.TBLCATEGORILER.Find(id);
            db.TBLCATEGORILER.Remove(kategori);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KategoriGetir(int id)
        {
            var ktgr = db.TBLCATEGORILER.Find(id);
            return View("KategoriGetir", ktgr);
        }
        public ActionResult Guncelle(TBLCATEGORILER p1)
        {
            var ktg = db.TBLCATEGORILER.Find(p1.CATEGORIID);
            ktg.CATEGORIAD = p1.CATEGORIAD;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}