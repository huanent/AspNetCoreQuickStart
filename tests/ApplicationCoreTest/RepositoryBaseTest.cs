using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationCoreTest
{
    [TestClass]
    public class RepositoryBaseTest
    {
        [TestMethod]
        public void FindTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseInMemoryDatabase("TestDb");
            var context = new AppDbContext(optionsBuilder.Options);
            var demoSet = context.Set<Demo>();
            var id = new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd");

            demoSet.Add(new Demo
            {
                Id = id,
                Name = "a"
            });
            context.SaveChanges();

            var context2 = new AppDbContext(optionsBuilder.Options);
            var demoSet2 = context.Set<Demo>();
            var entity = demoSet2.Find(id);

            Assert.IsNotNull(entity);
            Assert.AreEqual(entity.Id, id);
            Assert.AreEqual(entity.Name, "a");
        }

    }
}
