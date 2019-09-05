using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.Common;
using ZSZ.DTO;
using ZSZ.IService;

namespace ZSZ.Service
{
   public class AdminUserService : IAdminUserService
    {
        public AdminUserDTO[] GetAll()
        {
            using (MyContext my=new MyContext())
            {
                BaseService<AdminUserEntity> bs = new BaseService<AdminUserEntity>(my);
                return bs.GetAll().ToList().Select(m => ToDto(m)).ToArray();
            }
        }
        public AdminUserDTO ToDto(AdminUserEntity am)
        {
            AdminUserDTO userDTO = new AdminUserDTO
            {
                Id = am.Id,
                Name = am.Name,
                CreateDateTime = am.CreateDateTime,
                PhoneNum = am.PhoneNum,
                LoginErrorTimes = am.LoginErrorTimes,
                Email = am.Email,
                CityId=am.CityId,
                CityName = am.City.Name
            };
            return userDTO;
        }
        public AdminUserDTO GetById(long id)
        {
            using (MyContext my = new MyContext())
            {
                BaseService<AdminUserEntity> bs = new BaseService<AdminUserEntity>(my);
                return ToDto(bs.GetById(id));
            }
        }

        public bool MarkDeleted(long id)
        {
            using (MyContext my = new MyContext())
            {
                BaseService<AdminUserEntity> bs = new BaseService<AdminUserEntity>(my);
                return bs.MarkDeleted(id);
            }
        }

        public void Insert(string name, string PhoneNum, string Pwd, string Email, long? CityId, long[] RoleId)
        {
            using (MyContext my = new MyContext())
            {
                BaseService<AdminUserEntity> Admins = new BaseService<AdminUserEntity>(my);
                AdminUserEntity admin = new AdminUserEntity
                {
                    Name = name,
                    PhoneNum = PhoneNum,
                    PasswordHash = Pwd.GetHashCode().ToString(),
                    PasswordSalt = CommonHelper.CalcMD5(Pwd),
                    Email = Email,
                    CityId = CityId,
                    LoginErrorTimes = 0,
                    LastLoginErrorDateTime = DateTime.Now
                };
                my.AdminUsers.Add(admin);
                my.SaveChanges();
                var data= Admins.GetById(admin.Id);
                if(data==null)
                {
                    throw new Exception("不存在Id为" + admin.Id + "的用户");
                }
                else
                {
                    BaseService<RoleEntity> Role = new BaseService<RoleEntity>(my);
                    List<RoleEntity> roles = Role.GetAll().ToList().Where(m => RoleId.Contains(m.Id)).ToList();
                    foreach (var item in roles)
                    {
                        data.Roles.Add(item);
                    }
                }
                my.SaveChanges();
            }
        }

        public void Update(long id,string name, string PhoneNum, string Pwd, string Email, long? CityId, long[] RoleId)
        {
            using (MyContext my=new MyContext())
            {
                BaseService<AdminUserEntity> user = new BaseService<AdminUserEntity>(my);
                var data = user.GetById(id);
                if (data == null)
                {
                    throw new Exception("不存在Id为" +id + "的用户");
                }
                else
                {
                    data.Name = name;
                    data.PhoneNum = PhoneNum;
                    if (Pwd == null)
                    {

                    }
                    else
                    {
                        data.PasswordHash = Pwd.GetHashCode().ToString();
                        data.PasswordSalt = CommonHelper.CalcMD5(Pwd);
                    }
                    data.Email = Email;
                    data.CityId = CityId;
                    data.Roles.Clear();
                    BaseService<RoleEntity> Role = new BaseService<RoleEntity>(my);
                    List<RoleEntity> roles = Role.GetAll().ToList().Where(m => RoleId.Contains(m.Id)).ToList();
                    foreach (var item in roles)
                    {
                        data.Roles.Add(item);
                    }
                }
                my.SaveChanges();
            }
        }
    }
}
