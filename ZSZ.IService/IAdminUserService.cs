using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.DTO;

namespace ZSZ.IService
{
   public interface IAdminUserService:IServiceSupport
    {
        AdminUserDTO[] GetAll();
        AdminUserDTO GetById(long id);
        CityDTO[] GetCities();
        long GetPhoneUpdate(string PhoneNum);
        bool GetPhoneAdd(string PhoneNum);
        bool MarkDeleted(long id);
        void Insert(string name,string PhoneNum,string Pwd,string Email,long? CityId,long[] RoleId);
        void Update(long id,string name, string PhoneNum, string Pwd, string Email, long? CityId, long[] RoleId);
    }
}
