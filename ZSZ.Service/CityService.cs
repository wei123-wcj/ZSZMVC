using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.DTO;
using ZSZ.IService;


namespace ZSZ.Service
{
    public class CityService : ICityService
    {
        public CityDTO[] GetAll()
        {
            using (MyContext my=new MyContext())
            {
                BaseService<CityEntity> city = new BaseService<CityEntity>(my);
                CityDTO cities = new CityDTO();
                return city.GetAll().ToList().Select(m => ToDto(m)).ToArray();
            }
        }
        public CityDTO ToDto(CityEntity c)
        {
            CityDTO city = new CityDTO
            {
                Id = c.Id,
                CreateDateTime = c.CreateDateTime,
                Name = c.Name,
            };
            return city;
        }
    }
}
