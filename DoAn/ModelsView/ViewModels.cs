using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAn.ModelsView
{
    public class ViewModels
    {
        public class UserVM
        {
            public int UserID { get; set; }
            public string FullName { get; set; }
            public string ProfilImage { get; set; }
        }

        public class BaiVM
        {
            public int BaiId { get; set; }
            public string TenBai { get; set; }
            public List<SelectListItem> ListOfBai { get; set; }

        }

        public class CauHoiVM
        {
            public int CauHoiId { get; set; }
            public string CauHoi { get; set; }
            public string CauHoiType { get; set; }
            public string KetQua { get; set; }
            public ICollection<DapAnVM> DapAn { get; set; }
        }

        public class DapAnVM
        {
            public int DapAnId { get; set; }
            public string DapAn { get; set; }
        }

        public class KetQuaVM
        {
            public int CauHoiId { get; set; }
            public string CauHoi { get; set; }
            public string DapAnQ { get; set; }
            public bool isCorrect { get; set; }


        }
    }
}