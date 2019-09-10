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
                    //取出该数据的密码盐
                    string Salt = Loguser.PasswordHash;
                    //把用户输入的密码和密码盐加密
                    string Hash = CommonHelper.CalcMD5(pwd + Salt);
                    //判断数据库中的密码和用户加密后的密码是否相同
                    return Hash == Loguser.PasswordSalt;
                }
            }
        }
    }
}
