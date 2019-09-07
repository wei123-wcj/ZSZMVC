using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.DTO;

namespace ZSZ.IService
{
   public interface ISettingService:IServiceSupport
    {
        SettingDTO[] GetAll();
        long Insert(string name, string value);
        bool MarkDeleted(long id);
        SettingDTO GetById(long id);
        void Update(long id,string name, string value);
    }
}
