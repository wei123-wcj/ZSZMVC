using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.Common;
using ZSZ.IService;

namespace ZSZ.Service
{
    public class AdminUserLogin : IAdminUserLogin
    {
        public bool Login(string phone, string pwd)
        {
            using (MyContext my=new MyContext())
            {
                BaseService<AdminUserEntity> user = new BaseService<AdminUserEntity>(my);
                var Loguser = user.GetAll().ToList().SingleOrDefault(m => m.PhoneNum == phone);
                if(Loguser==null)
                {
                    return false;
                }
                else
                {
                    string Salt = Loguser.PasswordHash;
                    string Hash = CommonHelper.CalcMD5(pwd + Salt);
                    return Hash == Loguser.PasswordSalt;
                }
            }
        }
    }
}
