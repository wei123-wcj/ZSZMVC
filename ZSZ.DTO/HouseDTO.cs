using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZSZ.DTO
{
   public class HouseDTO:BaseDTO
    {
        public string RegionName { get; set; }
        public string CommunityName { get; set; }
        public string Address { get; set; }
        public int MonthRent { get; set; }
        public decimal Area { get; set; }
        public string RoomType { get; set; }
        public string DecorateStatusType { get; set; }
    }
}
