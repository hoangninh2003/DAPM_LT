﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAPM_LT.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class dapmEntities : DbContext
    {
        public dapmEntities()
            : base("name=dapmEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<DatSach> DatSaches { get; set; }
        public virtual DbSet<Kiemsoat> Kiemsoats { get; set; }
        public virtual DbSet<Loai> Loais { get; set; }
        public virtual DbSet<PhanQuyen> PhanQuyens { get; set; }
        public virtual DbSet<PhieuMua> PhieuMuas { get; set; }
        public virtual DbSet<PhieuMuaChiTiet> PhieuMuaChiTiets { get; set; }
        public virtual DbSet<PhieuMuon> PhieuMuons { get; set; }
        public virtual DbSet<PhieuMuonChiTiet> PhieuMuonChiTiets { get; set; }
        public virtual DbSet<Sach> Saches { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }
        public virtual DbSet<The> Thes { get; set; }
        public virtual DbSet<TrangThaiSachMuon> TrangThaiSachMuons { get; set; }
    }
}
