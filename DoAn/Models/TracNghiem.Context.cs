﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TracNghiem1Entities : DbContext
    {
        public TracNghiem1Entities()
            : base("name=TracNghiem1Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Bai> Bais { get; set; }
        public virtual DbSet<CauHoi> CauHois { get; set; }
        public virtual DbSet<DanhMuc> DanhMucs { get; set; }
        public virtual DbSet<DapAn> DapAns { get; set; }
        public virtual DbSet<KetQua> KetQuas { get; set; }
        public virtual DbSet<LoaiBai> LoaiBais { get; set; }
    }
}
