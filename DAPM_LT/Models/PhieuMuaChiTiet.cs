//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class PhieuMuaChiTiet
    {
        public int IdPhieuMuaChiTiet { get; set; }
        public Nullable<int> IdPhieuMua { get; set; }
        public Nullable<int> IdSach { get; set; }
        public Nullable<double> Tongtien { get; set; }
        public Nullable<int> SoLuong { get; set; }
    
        public virtual PhieuMua PhieuMua { get; set; }
        public virtual Sach Sach { get; set; }
    }
}
