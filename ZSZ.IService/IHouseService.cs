using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.DTO;

namespace ZSZ.IService
{
    public interface IHouseService:IServiceSupport
    {
        HouseDTO[] GetAll();
        long Insert();
        bool MarkDeleted(long id);
        HouseDTO GetById(long id);
        void Update();
    }
}
