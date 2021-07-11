using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using luyentap1.Models.Entities;
using System.Data.Entity;
using System.Net;
using PagedList;
namespace luyentap1.Controllers
{
    public class BaiThiController : Controller
    {

        NguyenVuLong_dbContext db = new NguyenVuLong_dbContext();
        // GET: BaiThi
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ActionCau3(int? idPro, int? page)
        {
            int pageSize = 3;
            var listHangHoa = db.HangHoas.Where(s => s.Gia >= 100).ToList();
           
            
    
            if (page > 0)
            {
                page = page + 0;
            }
            else
            {
                page = 1;
            }
            int start = (int)(page - 1) * pageSize;
            ViewBag.pageCurrent = page;
            int totalPage = listHangHoa.Count();
            float totalNumsize = (totalPage / (float)pageSize);
            int numSize = (int)Math.Ceiling(totalNumsize);
            ViewBag.numSize = numSize;
            var listHangHoa2 = listHangHoa.OrderBy(x => x.MaHang).Skip(start).Take(pageSize);
            return PartialView("~/Views/Shared/NguyenVuLong_Main.cshtml", listHangHoa2);
        }

        public ActionResult RenderNav()
        {
            List<LoaiHang> listloai = db.LoaiHangs.ToList();
            return PartialView("NguyenVuLong_Header",listloai);
        }
        public ActionResult RenderProduct()
        {
            List<HangHoa> listhang = db.HangHoas.Where(h => h.Gia >= 100).ToList();
            return PartialView("NguyenVuLong_Main", listhang);
        }
        public ActionResult RenderProductByCatID(int CatID)
        {
            ViewBag.numSize = 10;
            ViewBag.pageCurrent = 1;
            List<HangHoa> listhang = db.HangHoas.Where(h => h.MaLoai == CatID).ToList();
            return PartialView("NguyenVuLong_Main", listhang);
        }
        public ActionResult RenderProductByName(string searchString)
        {
            ViewBag.numSize = 10;
            ViewBag.pageCurrent = 1;
            List<HangHoa> listhang = db.HangHoas.Where(h => h.TenHang.ToLower().Contains(searchString.ToLower())).ToList();
            return PartialView("NguyenVuLong_Main", listhang);
        }
        // GET: HangHoas/Create
        public ActionResult Create()
        {
            ViewBag.MaLoai = new SelectList(db.LoaiHangs, "MaLoai", "TenLoai");
            return View();
        }

        // POST: HangHoas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHang,MaLoai,TenHang,Gia,Anh")] HangHoa hangHoa)
        {
            if (ModelState.IsValid)
            {
                db.HangHoas.Add(hangHoa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLoai = new SelectList(db.LoaiHangs, "MaLoai", "TenLoai", hangHoa.MaLoai);
            return View(hangHoa);
        }
        // GET: HangHoas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HangHoa hangHoa = db.HangHoas.Find(id);
            if (hangHoa == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLoai = new SelectList(db.LoaiHangs, "MaLoai", "TenLoai", hangHoa.MaLoai);
            return View(hangHoa);
        }

        // POST: HangHoas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaHang,MaLoai,TenHang,Gia,Anh")] HangHoa hangHoa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hangHoa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLoai = new SelectList(db.LoaiHangs, "MaLoai", "TenLoai", hangHoa.MaLoai);
            return View(hangHoa);
        }
        // GET: HangHoas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HangHoa hangHoa = db.HangHoas.Find(id);
            if (hangHoa == null)
            {
                return HttpNotFound();
            }
            return View(hangHoa);
        }

        // POST: HangHoas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HangHoa hangHoa = db.HangHoas.Find(id);
            db.HangHoas.Remove(hangHoa);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}