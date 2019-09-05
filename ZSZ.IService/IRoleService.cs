using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.DTO;

namespace ZSZ.IService
{
   public interface IRoleService:IServiceSupport
    {
        RoleDTO[] GetAll();
        long GetTotalCount();
        RoleDTO GetById(long id);
        RoleDTO[] GetAdminRole(long id);
        bool MarkDeleted(long id);
        long Insert(string name);
        void RolePer(long Rid, long[] DesId);
        void Update(long Id, string name, long[] DesId);
    }
}
