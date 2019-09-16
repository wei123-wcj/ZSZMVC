using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.DTO;

namespace ZSZ.IService
{
   public interface IRegionService:IServiceSupport
    {
        RegionDTO[] GetAll();
        long Insert(string name);
        bool MarkDeleted(long id);
        RegionDTO GetById(long id);
        void Update(long id,string name);
    }
}
