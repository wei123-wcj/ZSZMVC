using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.DTO;
using ZSZ.IService;

namespace ZSZ.Service
{
    public class RoleService : IRoleService
    {
        public RoleDTO[] GetAll()
        {
            using (MyContext my = new MyContext())
            {
                BaseService<RoleEntity> bs = new BaseService<RoleEntity>(my);
                return bs.GetAll().AsNoTracking().ToList().Select(m => ToDto(m)).ToArray();
            }
        }

        public RoleDTO GetById(long id)
        {
            using (MyContext my = new MyContext())
            {
                BaseService<RoleEntity> bs = new BaseService<RoleEntity>(my);
                return ToDto(bs.GetAll().Include(x => x.Permissions).FirstOrDefault(m=>m.Id==id));
            }
        }
        public long GetTotalCount()
        {
            using (MyContext my = new MyContext())
            {
                BaseService<RoleEntity> bs = new BaseService<RoleEntity>(my);
                return bs.GetTotalCount();
            }
        }
        //添加角色
        public long Insert(string name)
        {
            using (MyContext my = new MyContext())
            {
                RoleEntity p = new RoleEntity
                { 
                    Name = name,
                };
                my.Roles.Add(p);
                my.SaveChanges();
                return p.Id;
            }
        }
        //添加权限
        public void RolePer(long Rid, long[] DesId)
        {
            using (MyContext my=new MyContext())
            {
                BaseService<RoleEntity> r = new BaseService<RoleEntity>(my);
                //查找角色
                var Role = r.GetById(Rid);
                if(Role==null)
                {
                    throw new Exception("不存在" + Rid + "的角色");
                }
                else
                {
                    BaseService<PermissionEntity> p = new BaseService<PermissionEntity>(my);
                    //显示查找 long[] DesId集合在权限表的id
                    List<PermissionEntity> permission = p.GetAll().ToList().Where(m => DesId.Contains(m.Id)).ToList();
                    foreach (var item in permission)
                    {
                        //把查找的id全部添加到当前角色
                        Role.Permissions.Add(item);
                    }
                }
                my.SaveChanges();
            }
        }
        public bool MarkDeleted(long id)
        {
            using (MyContext my = new MyContext())
            {
                BaseService<RoleEntity> bs = new BaseService<RoleEntity>(my);
                return bs.MarkDeleted(id);
            }
        }
        public RoleDTO ToDto(RoleEntity role)
        {
            PermissionService permissionService = new PermissionService(); 
            RoleDTO roleDTO = new RoleDTO
            {
                Id = role.Id,
                Name = role.Name,
                Permission = permissionService.GetAll(),
                CreateDateTime = role.CreateDateTime,
            };
            return roleDTO;
        }
        public void Update(long Id, string name, long[] DesId)
        {
            using (MyContext my = new MyContext())
            {
                BaseService<RoleEntity> RoleService = new BaseService<RoleEntity>(my);
                var RoleModel = RoleService.GetById(Id);//先查出来
                BaseService<PermissionEntity> per = new BaseService<PermissionEntity>(my);
                if (RoleModel == null)
                {
                    throw new Exception("id不存在");
                }
                else
                {
                    RoleModel.Name = name;
                    RoleModel.Permissions.Clear();
                    BaseService<PermissionEntity> p = new BaseService<PermissionEntity>(my);
                    //显示查找 long[] DesId集合在权限表的id
                    List<PermissionEntity> permission = p.GetAll().ToList().Where(m => DesId.Contains(m.Id)).ToList();
                    foreach (var item in permission)
                    {
                        //把查找的id全部添加到当前角色
                        RoleModel.Permissions.Add(item);
                    }
                    my.SaveChanges();//在更新
                }
            }
        }

        public RoleDTO[] GetAdminRole(long id)
        {
            using (MyContext my=new MyContext())
            {
                BaseService<AdminUserEntity> user = new BaseService<AdminUserEntity>(my);
                return user.GetById(id).Roles.Select(m => ToDto(m)).ToArray();
            }
        }
    }
}
