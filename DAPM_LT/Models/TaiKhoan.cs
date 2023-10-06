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
    
    public partial class TaiKhoan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TaiKhoan()
        {
            this.DatSaches = new HashSet<DatSach>();
            this.PhieuMuas = new HashSet<PhieuMua>();
            this.Thes = new HashSet<The>();
        }
    
        public int Idtaikhoan { get; set; }
        public string Holot { get; set; }
        public string Ten { get; set; }
        public string Email { get; set; }
        public string Dienthoai { get; set; }
        public string Matkhau { get; set; }
        public string ImgU { get; set; }
        public Nullable<int> IDQuyen { get; set; }
        public string Diachi { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DatSach> DatSaches { get; set; }
        public virtual PhanQuyen PhanQuyen { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuMua> PhieuMuas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<The> Thes { get; set; }
    }
}
