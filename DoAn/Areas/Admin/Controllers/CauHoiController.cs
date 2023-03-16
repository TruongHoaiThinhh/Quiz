using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoAn.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.OleDb;
using System.IO;

namespace DoAn.Areas.Admin.Controllers
{
    public class CauHoiController : Controller
    {
        private TracNghiem1Entities db = new TracNghiem1Entities();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        OleDbConnection Econ;

        // GET: Admin/CauHoi
        public ActionResult Index()
        {
            var cauHois = db.CauHois.Include(c => c.Bai);
            return View(cauHois.ToList());
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            string filename = Guid.NewGuid() + Path.GetExtension(file.FileName);
            string filepath = "/excelfolder/" + filename;
            file.SaveAs(Path.Combine(Server.MapPath("/excelfolder"), filename));
            InsertExceldata(filepath, filename);

            var cauHois = db.CauHois.Include(c => c.Bai);
            return View(cauHois.ToList());

        }
        private void ExcelConn(string filepath)
        {
            string constr = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0 Xml;HDR=YES;""", filepath);
            Econ = new OleDbConnection(constr);
        }
        private void InsertExceldata(string fileepath, string filename)
        {
            string fullpath = Server.MapPath("/excelfolder/") + filename;
            ExcelConn(fullpath);
            string query = string.Format("Select * from [{0}]", "Sheet1$");
            OleDbCommand Ecom = new OleDbCommand(query, Econ);
            Econ.Open();
            DataSet ds = new DataSet();
            OleDbDataAdapter oda = new OleDbDataAdapter(query, Econ);
            Econ.Close();
            oda.Fill(ds);
            DataTable dt = ds.Tables[0];
            SqlBulkCopy objbulk = new SqlBulkCopy(con);
            objbulk.DestinationTableName = "CauHoi";
            objbulk.ColumnMappings.Add("CauHoiId", "CauHoiId");
            objbulk.ColumnMappings.Add("CauHoi", "CauHoi");
            objbulk.ColumnMappings.Add("BaiId", "BaiId");


            con.Open();
            objbulk.WriteToServer(dt);
            con.Close();
        }


        // GET: Admin/CauHoi/Create
        [ValidateInput(false)]
        public ActionResult Them()
        {
            ViewBag.BaiId = new SelectList(db.Bais, "BaiId", "TenBai");
            return View();
        }

        // POST: Admin/CauHoi/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Them([Bind(Include = "CauHoiId,CauHoi1,BaiId")] CauHoi cauHoi)
        {
            if (ModelState.IsValid)
            {
                db.CauHois.Add(cauHoi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BaiId = new SelectList(db.Bais, "BaiId", "TenBai", cauHoi.BaiId);
            return View(cauHoi);
        }

        // GET: Admin/CauHoi/Edit/5
        [ValidateInput(false)]
        public ActionResult Sua(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CauHoi cauHoi = db.CauHois.Find(id);
            if (cauHoi == null)
            {
                return HttpNotFound();
            }
            ViewBag.BaiId = new SelectList(db.Bais, "BaiId", "TenBai", cauHoi.BaiId);
            return View(cauHoi);
        }

        // POST: Admin/CauHoi/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Sua([Bind(Include = "CauHoiId,CauHoi1,BaiId")] CauHoi cauHoi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cauHoi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BaiId = new SelectList(db.Bais, "BaiId", "TenBai", cauHoi.BaiId);
            return View(cauHoi);
        }

        // GET: Admin/CauHoi/Delete/5
        public ActionResult Xoa(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CauHoi cauHoi = db.CauHois.Find(id);
            if (cauHoi == null)
            {
                return HttpNotFound();
            }
            return View(cauHoi);
        }

        // POST: Admin/CauHoi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CauHoi cauHoi = db.CauHois.Find(id);
            db.CauHois.Remove(cauHoi);
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
