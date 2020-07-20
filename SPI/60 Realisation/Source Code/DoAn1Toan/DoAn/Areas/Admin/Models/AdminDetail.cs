using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAn.Areas.Admin.Models
{
    public class AdminDetail
    {
        public List<Account> DSAdmin()
        {
            List<Account> list = new List<Account>();
            using (CSDLContext db = new CSDLContext())
            {
                list = db.AdminAccount.ToList();
            }
            return list;
        }
        public int DoiMatKhau(int id,string matkhau)
        {
            using (CSDLContext db = new CSDLContext())
            {
                Account admin = db.AdminAccount.Find(id);
                admin.Password = matkhau;
                db.Entry(admin).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return 1;
        }
        public Account Tim(int id)
        {
            using (CSDLContext db = new CSDLContext())
            {
                return db.AdminAccount.Find(id);
            }
        }
    }
}