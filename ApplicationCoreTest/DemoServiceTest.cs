using ApplicationCore.Repositories;
using ApplicationCore.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace ApplicationCoreTest
{
    [TestClass]
    public class DemoServiceTest
    {
        [TestMethod]
        public void GetDemoByKey()
        {
            var mock = new Mock<IDemoRepository>();
            mock.Setup(s => s.GetDemoByKey(new Guid()))
                .Returns(new ApplicationCore.Entities.Demo
                {
                    Id = new Guid(),
                    Name = "张三"
                });

            var demoService = new DemoService(mock.Object);
            var demo = demoService.GetDemoByKey(new Guid());

            Assert.AreEqual(demo.Id, new Guid());
            Assert.AreEqual(demo.Name, "张三");
        }
    }
}
