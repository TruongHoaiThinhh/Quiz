using DoAn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static DoAn.ModelsView.ViewModels;

namespace DoAn.Controllers
{
    public class TracNghiemController : Controller
    {
        // GET: TracNghiem

        public TracNghiem1Entities dbContext = new TracNghiem1Entities();

        public ActionResult DanhSachBai(int id)
        {
            var bai = from s in dbContext.Bais where s.LoaiBaiId == id select s;
            return View(bai);

        }

        public ActionResult DanhMuc(int id)
        {
            var danhmuc = from s in dbContext.LoaiBais where s.DanhMucId == id select s;
            return View(danhmuc);
        }

        [HttpGet]
        public ActionResult ChonBai(int id)
        {
            BaiVM quiz = new DoAn.ModelsView.ViewModels.BaiVM();
            quiz.ListOfBai = dbContext.Bais.Where(q => q.BaiId == id).Select(q => new SelectListItem
            {
                Text = q.TenBai,
                Value = id.ToString()

            }).ToList();

            return View(quiz);
        }

        [HttpPost]
        public ActionResult ChonBai(BaiVM bai)
        {
            BaiVM chonbai = dbContext.Bais.Where(q => q.BaiId == bai.BaiId).Select(q => new BaiVM
            {
                BaiId = q.BaiId,
                TenBai = q.TenBai,


            }).FirstOrDefault();

            if (chonbai != null)
            {
                Session["ChonBai"] = chonbai;

                return RedirectToAction("LamBai");
            }

            return View();
        }

        [HttpGet]
        public ActionResult LamBai()
        {
            BaiVM quizSelected = Session["ChonBai"] as BaiVM;
            IQueryable<CauHoiVM> questions = null;

            if (quizSelected != null)
            {
                questions = dbContext.CauHois.Where(q => q.Bai.BaiId == quizSelected.BaiId)
                   .Select(q => new CauHoiVM
                   {
                       CauHoiId = q.CauHoiId,
                       CauHoi = q.CauHoi1,
                       DapAn = q.DapAns.Select(c => new DapAnVM
                       {
                           DapAnId = c.DapAnId,
                           DapAn = c.DapAn1
                       }).ToList()

                   }).AsQueryable();


            }

            return View(questions);
        }

        [HttpPost]
        public ActionResult LamBai(List<KetQuaVM> ketquacautracnghiem)
        {
            List<KetQuaVM> ketquacuoicung = new List<DoAn.ModelsView.ViewModels.KetQuaVM>();

            foreach (KetQuaVM cautraloi in ketquacautracnghiem)
            {
                KetQuaVM ketqua = dbContext.KetQuas.Where(a => a.CauHoiId == cautraloi.CauHoiId).Select(a => new KetQuaVM
                {
                    CauHoiId = a.CauHoiId.Value,
                    DapAnQ = a.KetQua1,
                    isCorrect = (cautraloi.DapAnQ.ToLower().Equals(a.KetQua1.ToLower()))

                }).FirstOrDefault();

                ketquacuoicung.Add(ketqua);
            }

            return Json(new { ketqua = ketquacuoicung }, JsonRequestBehavior.AllowGet);
        }

    }

}