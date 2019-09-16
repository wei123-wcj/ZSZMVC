using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.DTO;
using ZSZ.IService;
using System.Data.Entity;

namespace ZSZ.Service
{
    public class HouseService : IHouseService
    {
        public IRegionService Region { get; set; }
        public HouseDTO[] GetAll()
        {
            using (MyContext my=new MyContext())
            {
                BaseService<HouseEntity> h = new BaseService<HouseEntity>(my);
                return h.GetAll().AsNoTracking().ToList().Select(m => DTO(m)).ToArray();
            }
        }
        public HouseDTO DTO(HouseEntity h)
        {

            HouseDTO d = new HouseDTO
            {
                Id = h.Id,
                RegionName = Region.GetById(h.Community.RegionId).Name,
                CommunityName = h.Community.Name,
                Address = h.Address,
                MonthRent = h.MonthRent,
                Area = h.Area,
                RoomType = h.RoomType.Name,
                DecorateStatusType = h.DecorateStatus.Name
            };
            return d;
            
           
        }
        public HouseDTO GetById(long id)
        {
            throw new NotImplementedException();
        }

        public long Insert()
        {
            throw new NotImplementedException();
        }

        public bool MarkDeleted(long id)
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
