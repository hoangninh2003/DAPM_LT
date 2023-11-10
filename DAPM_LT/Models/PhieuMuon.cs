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
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class PhieuMuon
    {
        public class ckngay : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var ngayMuon = (DateTime)value;
                var ngayHienTai = DateTime.Now.Date;

                if (ngayMuon.Date < ngayHienTai)
                {
                    return new ValidationResult(ErrorMessage);
                }

                // Kiểm tra ngày mượn có nhỏ hơn ngày trả không (nếu có)
                var ngayTraProperty = validationContext.ObjectType.GetProperty("Ngaytra");
                if (ngayTraProperty != null)
                {
                    var ngayTra = (DateTime?)ngayTraProperty.GetValue(validationContext.ObjectInstance, null);
                    if (ngayTra.HasValue && ngayMuon.Date >= ngayTra.Value.Date)
                    {
                        return new ValidationResult(ErrorMessage);
                    }
                }

                return ValidationResult.Success;
            }
        }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PhieuMuon()
        {
            this.PhieuMuonChiTiets = new HashSet<PhieuMuonChiTiet>();
        }

        [Key]
        public int Idphieu { get; set; }

        [Display(Name = "Mã sách")]
        [Required(ErrorMessage = "Mã sách không được để trống")]
        public Nullable<int> Idsach { get; set; }

        [Display(Name = "Mã thẻ")]
        [Required(ErrorMessage = "Mã thẻ không được để trống")]
        public Nullable<int> Idthe { get; set; }

        [Display(Name = "Ngày mượn")]
        [Required(ErrorMessage = "Ngày mượn không được để trống")]
        [DataType(DataType.Date)]
        [ckngay(ErrorMessage = "Ngày mượn phải nhỏ hơn ngày hiện tại và ngày trả")]
        public Nullable<System.DateTime> Ngaymuon { get; set; }

        [Display(Name = "Ngày trả")]
        [Required(ErrorMessage = "Ngày trả không được để trống")]
        [DataType(DataType.Date)]
        [ckngay(ErrorMessage = "Ngày trả phải sau ngày mượn")]
        public Nullable<System.DateTime> Ngaytra { get; set; }

        [Display(Name = "Trạng thái mượn")]
        [StringLength(50)]
        [Required(ErrorMessage = "Trạng thái mượn không được để trống")]
        public string Trangthaimuon { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuMuonChiTiet> PhieuMuonChiTiets { get; set; }
        public virtual The The { get; set; }
    }
}
