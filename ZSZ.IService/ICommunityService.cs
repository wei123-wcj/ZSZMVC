using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.DTO;

namespace ZSZ.IService
{
   public interface ICommunityService:IServiceSupport
    {
        CommunityDTO[] GetAll();
        long Insert();
        bool MarkDeleted(long id);
        CommunityDTO GetById(long id);
        void Update();
    }
}
