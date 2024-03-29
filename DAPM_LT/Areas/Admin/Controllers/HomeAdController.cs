using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAPM_LT.Models;
using PagedList.Mvc;
using PagedList;

namespace DAPM_LT.Areas.Admin.Controllers
{
    public class HomeAdController : Controller
    {
        private dapmEntities db = new dapmEntities();
        // GET: Admin/HomeAd
        public ActionResult Index()
        {
            var currentUser = Session["use"] as DAPM_LT.Models.TaiKhoan;
            //Kiểm tra nếu tên quyền Administrator mới được truy cập trang admin
            if (currentUser != null && (currentUser.PhanQuyen.TenQuyen == "Adminstrator" || currentUser.PhanQuyen.TenQuyen == "Nhanvien"))
            {
                return View();
            }
            return RedirectPermanent("~/Home/Index");


        }

        public ActionResult Sachpartial(string _name, string _selectedValue, int? _id, int? page)
        {
            int pageSize = 10;

            IQueryable<Kiemsoat> query = db.Kiemsoats.Include(k => k.Sach);


            if (!string.IsNullOrEmpty(_name))
            {
               
                query = query.Where(s => s.Sach.Tieude.Contains(_name));
            }
            if (_id != null)
            {
               
                query = query.Where(i => i.Idkiemsoat == _id);
            }

           



            query = query.OrderByDescending(s => s.Idkiemsoat); // Sắp xếp giảm dần theo Idkiémoat

            int totalItems = query.Count(); // Lấy tổng số mục

            int pageNumber = (page ?? 1);

            // Sử dụng Skip và Take sau khi đã sắp xếp
            var kiemsoatList = query.Skip((pageNumber - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToList();

            // Chuyển đổi danh sách sang kiểu PagedList
            IPagedList<Kiemsoat> pagedList = new StaticPagedList<Kiemsoat>(kiemsoatList, pageNumber, pageSize, totalItems);
            return View(pagedList);
        }



        public ActionResult TKpartial()
        {
            var taiKhoans = db.TaiKhoans.Include(t => t.PhanQuyen);
            return View(taiKhoans.ToList());
        }
    }
}