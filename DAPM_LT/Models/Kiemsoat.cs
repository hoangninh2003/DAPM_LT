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
    
    public partial class Kiemsoat
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Kiemsoat()
        {
            this.PhieuMuaChiTiets = new HashSet<PhieuMuaChiTiet>();
            this.PhieuMuonChiTiets = new HashSet<PhieuMuonChiTiet>();
            this.TrangThaiSachMuons = new HashSet<TrangThaiSachMuon>();
        }
    
        public int Idkiemsoat { get; set; }
        public string Imgtrangthai { get; set; }
        public string Trangthaisach { get; set; }
        public string Muontra { get; set; }
        public Nullable<int> Solanmuon { get; set; }
        public Nullable<int> Idsach { get; set; }
    
        public virtual Sach Sach { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuMuaChiTiet> PhieuMuaChiTiets { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuMuonChiTiet> PhieuMuonChiTiets { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TrangThaiSachMuon> TrangThaiSachMuons { get; set; }
    }
}
