//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DoAn.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class KetQua
    {
        public int KetQuaId { get; set; }
        public string KetQua1 { get; set; }
        public Nullable<int> CauHoiId { get; set; }
    
        public virtual CauHoi CauHoi { get; set; }
    }
}