using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ZSZ.DTO;
using ZSZ.IService;

namespace ZSZ.Service
{
    public class PermissionService : IPermissionService
    {

        public PermissionDTO[] GetAll()
        {
            using (MyContext my = new MyContext())
            {
                BaseService<PermissionEntity> bs = new BaseService<PermissionEntity>(my);
                return bs.GetAll().AsNoTracking().ToList().Select(m => GetDTO(m)).ToArray();
            }
        }

        public PermissionDTO GetById(long id)
        {
            using (MyContext my = new MyContext())
            {
                BaseService<PermissionEntity> bs = new BaseService<PermissionEntity>(my);
                return GetDTO(bs.GetById(id));
            }
           
        }

        public long GetTotalCount()
        {
            using (MyContext my = new MyContext())
            {
                BaseService<PermissionEntity> bs = new BaseService<PermissionEntity>(my);
                return bs.GetTotalCount();
            }
        }

        public bool MarkDeleted(long id)
        {
             using (MyContext my = new MyContext())
            {
                BaseService<PermissionEntity> bs = new BaseService<PermissionEntity>(my);
                return bs.MarkDeleted(id);
            }
        }
        //添加
        public long Insert(string name,string des)
        {
            using (MyContext my = new MyContext())
            {
                PermissionEntity p = new PermissionEntity();
                p.Description = des;
                p.Name = name;
                my.Permissions.Add(p);
                int b = my.SaveChanges();
                return p.Id;
            }
        }
        //PermissionEntity转换PermissionDTO
        public PermissionDTO GetDTO(PermissionEntity p)
        {
            PermissionDTO d = new PermissionDTO
            {
                Name = p.Name,
                Description = p.Description,
                Id = p.Id,
                CreateDateTime = p.CreateDateTime
            };
            return d;
        }
        public void Update(long id,string name,string des)
        {
            using (MyContext my = new MyContext())
            {
                BaseService<PermissionEntity> permissionService = new BaseService<PermissionEntity>(my);
                var perModel = permissionService.GetById(id);//先查出来
                if (perModel == null)
                {
                    throw new Exception("id不存在");
                }
                else
                {
                    perModel.Name = name;
                    perModel.Description = des;
                    my.SaveChanges();//在更新
                }
            }
        }
        public PermissionDTO[] GetPermissions(long id)
        {
            using (MyContext my = new MyContext())
            {
                BaseService<RoleEntity> bs = new BaseService<RoleEntity>(my);
                return bs.GetById(id).Permissions.Select(m=>GetDTO(m)).ToArray();
            }
        }
    }
}
