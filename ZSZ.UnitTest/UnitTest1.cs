using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZSZ.Service;

namespace ZSZ.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            log4net.Config.XmlConfigurator.Configure();
            using (MyContext zc=new MyContext())
            {
                CityEntity city = new CityEntity
                {
                    Name = "北京",
                };
                zc.Cities.Add(city);
                zc.SaveChanges();
            }
        }
    }
}
