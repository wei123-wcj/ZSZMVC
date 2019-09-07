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
                return city.GetAll().ToList().Select(m => ToDto(m)).ToArray();
            }
        }

        public CityDTO GetById(long id)
        {
            using (MyContext my = new MyContext())
            {
                BaseService<CityEntity> city = new BaseService<CityEntity>(my);
                return ToDto(city.GetById(id));
            }
        }

        public long Insert(string name)
        {
            using (MyContext my = new MyContext())
            {
                CityEntity city = new CityEntity
                {
                    Name = name,
                };
                my.Cities.Add(city);
                my.SaveChanges();
                return city.Id;
            }
        }

        public bool MarkDeleted(long id)
        {
            using (MyContext my = new MyContext())
            {
                BaseService<CityEntity> city = new BaseService<CityEntity>(my);
                return city.MarkDeleted(id);
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
