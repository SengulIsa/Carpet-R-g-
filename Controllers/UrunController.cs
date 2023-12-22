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
    public class UrunController : Controller
    {
        // GET: Urun
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index(int sayfa=1)
        {
            //var degerler = db.TBLURUNLER.ToList();
            var degerler = db.TBLURUNLER.ToList().ToPagedList(sayfa,4);

            return View(degerler);
        }
        [HttpGet]

        public ActionResult YeniÜrün()
        {
            List<SelectListItem> degerler = (from i in db.TBLCATEGORILER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.CATEGORIAD,
                                                 Value = i.CATEGORIID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
            return View();

        }
        [HttpPost]
        public ActionResult YeniÜrün(TBLURUNLER p1)
        {
            var ktg = db.TBLCATEGORILER.Where(m => m.CATEGORIID == p1.TBLCATEGORILER.CATEGORIID).FirstOrDefault();
            p1.TBLCATEGORILER = ktg;
            db.TBLURUNLER.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult SIL(int id)
        {
            var urun = db.TBLURUNLER.Find(id);
            db.TBLURUNLER.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunGetir(int id)
        {
            var urun = db.TBLURUNLER.Find(id);

            List<SelectListItem> degerler = (from i in db.TBLCATEGORILER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.CATEGORIAD,
                                                 Value = i.CATEGORIID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;

            return View("UrunGetir", urun);
        }
        public ActionResult Guncelle(TBLURUNLER p1)
        {
            var urun = db.TBLURUNLER.Find(p1.URUNID);
            urun.URUNAD = p1.URUNAD;
            //urun.URUNCATEGORY = p1.URUNCATEGORY;
            var ktg = db.TBLCATEGORILER.Where(m => m.CATEGORIID == p1.TBLCATEGORILER.CATEGORIID).FirstOrDefault();
            urun.URUNCATEGORY=ktg.CATEGORIID;
            urun.FIYAT = p1.FIYAT;
            urun.MARKA = p1.MARKA;
            urun.STOK = p1.STOK;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}