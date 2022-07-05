using Ajax.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace Ajax.Controllers
{
    public class HomeController : Controller
    {

        private DBEntities db = new DBEntities();
        public ActionResult Index()
        {
            //dkjsfishgfhu
            return View();
        }
        public JsonResult GetData()
        {
            try
            {
                var dsTen = (from ten in db.Tens
                             select new
                             {
                                 Id = ten.ID,
                                 Ho = ten.Ho,
                                 Ten = ten.Ten1
                             }).ToList();
                return Json(new { dsTen = dsTen }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { ex.Message });
            }
        }
        [HttpPost]
        public JsonResult AddData(string ho, string ten)
        {
            try
            {
                var dsten = new Ten();
                dsten.Ho = ho;
                dsten.Ten1 = ten;
                db.Tens.Add(dsten);
                db.SaveChanges();
                return Json(JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult Detail(int Id)
        {
            try
            {
                var dsten = db.Tens.SingleOrDefault(x => x.ID == Id);
                return Json(new { ds = dsten }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ex.Message }, JsonRequestBehavior.AllowGet);

            }
        }
        [HttpPost]
        public JsonResult Edit(int Id, string ho, string ten)
        {
            try
            {
                // tim dua vao id
                var dsten = db.Tens.SingleOrDefault(x => x.ID == Id);
                //gan lai cac thuoc tinh
                dsten.Ho = ho;
                dsten.Ten1 = ten;
                //luu
                db.SaveChanges();
                return Json(JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {

                return Json(new { ex.Message }, JsonRequestBehavior.AllowGet);

            }
        }
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            try
            {
                var dsten = db.Tens.SingleOrDefault(x => x.ID == Id);
                db.Tens.Remove(dsten);

                db.SaveChanges();
                return Json(JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {

                return Json(new { ex.Message }, JsonRequestBehavior.AllowGet);

            }
        }
        // tim kiem 
        [HttpGet]
        public JsonResult GetDataSearch(string search)
        {
            try
            {
                var dsTen = (from ten in db.Tens
                             select new
                             {
                                 Id = ten.ID,
                                 Ho = ten.Ho,
                                 Ten = ten.Ten1
                             }).ToList();
                var s = dsTen.Where(x => x.Ho.ToLower().Contains(search)).ToList();
                return Json(new { dsTen = s }, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {

                return Json(new { ex.Message });
            }
        }
    }
}