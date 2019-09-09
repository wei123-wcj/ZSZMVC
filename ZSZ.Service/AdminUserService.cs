using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ZSZ.Common;
using ZSZ.DTO;
using ZSZ.IService;

namespace ZSZ.Service
{
    public class AdminUserService : IAdminUserService
    {
        /// <summary>
        /// 获取全部数据
        /// </summary>
        /// <returns></returns>
        public AdminUserDTO[] GetAll()
        {
            using (MyContext my = new MyContext())
            {
                BaseService<AdminUserEntity> bs = new BaseService<AdminUserEntity>(my);
                return bs.GetAll().AsNoTracking().Include(m => m.City).ToList().Select(m => ToDto(m)).ToArray();
            }
        }
        /// <summary>
        /// 获取城市
        /// </summary>
        /// <returns></returns>
        public CityDTO[] GetCities()
        {
            CityService city = new CityService();
            CityDTO cityEntity = new CityDTO
            {
                Id = 0,
                Name = "总部"
            };
            return city.GetAll().Prepend(cityEntity).ToArray();
        }
        /// <summary>
        /// AdminUserEntity转换AdminUserDTO
        /// </summary>
        /// <param name="am"></param>
        /// <returns></returns>
        public AdminUserDTO ToDto(AdminUserEntity am)
        {
            if (am.CityId == null)
            {
                am.CityId = 0;
                CityEntity adminUser = new CityEntity
                {
                    Id = 0,
                    Name = "总部"
                };
                am.City = adminUser;
            }
            AdminUserDTO userDTO = new AdminUserDTO
            {
                Id = am.Id,
                Name = am.Name,
                CreateDateTime = am.CreateDateTime,
                PhoneNum = am.PhoneNum,
                LoginErrorTimes = am.LoginErrorTimes,
                Email = am.Email,
                CityId = am.CityId,
                CityName = am.City.Name
            };
            return userDTO;
        }
        /// <summary>
        /// 根据id获取数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AdminUserDTO GetById(long id)
        {
            using (MyContext my = new MyContext())
            {
                BaseService<AdminUserEntity> bs = new BaseService<AdminUserEntity>(my);
                return ToDto(bs.GetById(id));
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool MarkDeleted(long id)
        {
            using (MyContext my = new MyContext())
            {
                BaseService<AdminUserEntity> bs = new BaseService<AdminUserEntity>(my);
                return bs.MarkDeleted(id);
            }
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="name"></param>
        /// <param name="PhoneNum"></param>
        /// <param name="Pwd"></param>
        /// <param name="Email"></param>
        /// <param name="CityId"></param>
        /// <param name="RoleId"></param>
        public void Insert(string name, string PhoneNum, string Pwd, string Email, long? CityId, long[] RoleId)
        {
            using (MyContext my = new MyContext())
            {
                if (CityId == 0)
                {
                    CityId = null;
                }
                BaseService<AdminUserEntity> Admins = new BaseService<AdminUserEntity>(my);
                string alit = CommonHelper.CreateVerifyCode(6);
                AdminUserEntity admin = new AdminUserEntity
                {
                    Name = name,
                    PhoneNum = PhoneNum,
                    PasswordHash = alit,
                    PasswordSalt = CommonHelper.CalcMD5(Pwd + alit),
                    Email = Email,
                    CityId = CityId,
                    LoginErrorTimes = 0,
                };
                my.AdminUsers.Add(admin);
                my.SaveChanges();
                var data = Admins.GetById(admin.Id);
                if (data == null)
                {
                    throw new Exception("不存在Id为" + admin.Id + "的用户");
                }
                else
                {//添加角色
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
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="PhoneNum"></param>
        /// <param name="Pwd"></param>
        /// <param name="Email"></param>
        /// <param name="CityId"></param>
        /// <param name="RoleId"></param>
        public void Update(long id, string name, string PhoneNum, string Pwd, string Email, long? CityId, long[] RoleId)
        {
            using (MyContext my = new MyContext())
            {
                BaseService<AdminUserEntity> user = new BaseService<AdminUserEntity>(my);
                var data = user.GetById(id);
                if (data == null)
                {
                    throw new Exception("不存在Id为" + id + "的用户");
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
                        string salt = CommonHelper.CreateVerifyCode(6);
                        data.PasswordHash = salt;
                        data.PasswordSalt = CommonHelper.CalcMD5(Pwd + salt);
                    }
                    data.Email = Email;
                    if (CityId == 0)
                    {
                        data.CityId = null;
                    }
                    else
                    {
                        data.CityId = CityId;
                    }
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
        /// <summary>
        /// 查找该手机号是否被注册
        /// </summary>
        /// <param name="PhoneNum"></param>
        /// <returns></returns>
        public bool GetPhoneAdd(string PhoneNum)
        {
            using (MyContext my = new MyContext())
            {
                BaseService<AdminUserEntity> user = new BaseService<AdminUserEntity>(my);
                return user.GetAll().ToList().Select(m => m.PhoneNum).Contains(PhoneNum);
            }
        }
        public long GetPhoneUpdate(string PhoneNum)
        {
            using (MyContext my = new MyContext())
            {
                BaseService<AdminUserEntity> user = new BaseService<AdminUserEntity>(my);
                var a = user.GetAll().AsNoTracking().Include(m=>m.City).ToArray().Select(m => ToDto(m)).FirstOrDefault(m => m.PhoneNum == PhoneNum);
                return a.Id;
            }
        }
    }
}