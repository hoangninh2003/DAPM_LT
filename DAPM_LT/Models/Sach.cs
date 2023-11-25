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
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;

    public partial class Sach
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sach()
        {
            this.Kiemsoats = new HashSet<Kiemsoat>();
            this.TrangThaiSachMuons = new HashSet<TrangThaiSachMuon>();
        }
        [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
        public sealed class cknam : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (value != null)
                {
                    int year = (int)value;

                    if (year > DateTime.Now.Year)
                    {
                        return new ValidationResult("Năm xuất bản không được lớn hơn năm hiện tại");
                    }
                }

                return ValidationResult.Success;
            }
        }
        [Display(Name = "Mã sách")]
        [Key]
        public int Idsach { get; set; }

        [Display(Name = "Tên sách")]
        [StringLength(int.MaxValue, MinimumLength = 1, ErrorMessage = "Tên sách phải ít nhất 1 ký tự")]
        [Required(ErrorMessage = "Tên sách không được để trống")]
        public string Tieude { get; set; }
        [Display(Name = "T?i ?nh lên")]
        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
        public byte[] ImgSach { get; set; }
        [Display(Name = "Tác giả")]
        public string Tacgia { get; set; }

        [Display(Name = "Giá mua")]
        [Required(ErrorMessage = "Giá mua không được để trống")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá mua không hợp lệ")]
        public Nullable<decimal> GiaMua { get; set; }

        [Display(Name = "Năm xuất bản")]
        [Required(ErrorMessage = "Năm xuất bản không được để trống")]
        [Range(1000, int.MaxValue, ErrorMessage = "Năm xuất bản không hợp lệ")]
        [cknam(ErrorMessage = "Năm xuất bản không được lớn hơn năm hiện tại")]
        public Nullable<int> Namxuatban { get; set; }

        [Display(Name = "Mô tả")]
        [Required(ErrorMessage = "Mô tả không được để trống")]
        public string Mota { get; set; }

        [Display(Name = "Mã loại")]
        [Required(ErrorMessage = "Mã loại không được để trống")]
        public Nullable<int> Idloai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kiemsoat> Kiemsoats { get; set; }
        public virtual Loai Loai { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TrangThaiSachMuon> TrangThaiSachMuons { get; set; }
    }
}
