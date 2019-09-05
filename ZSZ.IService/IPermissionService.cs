using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.DTO;

namespace ZSZ.IService
{
   public interface IPermissionService:IServiceSupport
    {
        PermissionDTO[] GetAll();
        long GetTotalCount();
        PermissionDTO GetById(long id);
        PermissionDTO[] GetPermissions(long id);
        bool MarkDeleted(long id);
        long Insert(string name,string des);
        void Update(long id, string name, string des);
    }
}
