using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.DTO;
using ZSZ.IService;

namespace ZSZ.Service
{
    public class SettingService : ISettingService
    {
        /// <summary>
        /// 获取全部配置信息
        /// </summary>
        /// <returns></returns>
        public SettingDTO[] GetAll()
        {
            using (MyContext my=new MyContext())
            {
                BaseService<SettingEntity> set = new BaseService<SettingEntity>(my);
                return set.GetAll().ToList().Select(m => DTOSett(m)).ToArray();
            }
        }
        /// <summary>
        /// 根据id获取某条信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SettingDTO GetById(long id)
        {
            using (MyContext my = new MyContext())
            {
                BaseService<SettingEntity> set = new BaseService<SettingEntity>(my);
                return DTOSett(set.GetById(id));
            }
        }
        /// <summary>
        /// 添加配置信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public long Insert(string name, string value)
        {
            using (MyContext my = new MyContext())
            {
                SettingEntity setting = new SettingEntity
                {
                    Name = name,
                    Value = value,
                };
                my.Settings.Add(setting);
                my.SaveChanges();
                return setting.Id;
            }
        }
        /// <summary>
        /// 删除配置信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool MarkDeleted(long id)
        {
            using (MyContext my = new MyContext())
            {
                BaseService<SettingEntity> set = new BaseService<SettingEntity>(my);
                return set.MarkDeleted(id);
            }
        }
        /// <summary>
        /// 修改配置信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public void Update(long id,string name, string value)
        {
            using (MyContext my = new MyContext())
            {
                BaseService<SettingEntity> set = new BaseService<SettingEntity>(my);
                var data = set.GetById(id);
                data.Name = name;
                data.Value = value;
                my.SaveChanges();
            }
        }
        /// <summary>
        /// SettingEntity转换SettingDTO
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public SettingDTO DTOSett(SettingEntity setting)
        {
            SettingDTO set = new SettingDTO
            {
                Id = setting.Id,
                Name = setting.Name,
                CreateDateTime = setting.CreateDateTime,
                Value = setting.Value
            };
            return set;
        }
    }
}
