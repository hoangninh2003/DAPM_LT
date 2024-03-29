using DAPM_LT.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DAPM_LT.Facade
{
    public class QuanLyLoaiFacade
    {
        private dapmEntities db = new dapmEntities();

        public List<Loai> LayTatCaLoai()
        {
            return db.Loais.ToList();
        }

        public Loai TimLoaiTheoId(int id)
        {
            return db.Loais.Find(id);
        }

        public void ThemLoai(Loai loai)
        {
            db.Loais.Add(loai);
            db.SaveChanges();
        }

        public void CapNhatLoai(Loai loai)
        {
            db.Entry(loai).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void XoaLoai(int id)
        {
            Loai loai = db.Loais.Find(id);
            db.Loais.Remove(loai);
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}