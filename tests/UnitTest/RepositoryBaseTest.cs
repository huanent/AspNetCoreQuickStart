using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace ApplicationCoreTest
{
    public class RepositoryBaseTest
    {
        AppDbContext _dbContext;

        public RepositoryBaseTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseInMemoryDatabase("TestDb");
            _dbContext = new AppDbContext(optionsBuilder.Options);
        }

        [Fact]
        public void FindTest()
        {
            var demoSet = _dbContext.Set<Demo>();
            var id = new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd");

            demoSet.Add(new Demo
            {
                Id = id,
                Name = "a"
            });
            _dbContext.SaveChanges();

            Assert.Equal(1, demoSet.Count());

            var entity = demoSet.Find(id);

            Assert.NotNull(entity);
            Assert.Equal(entity.Id, id);
            Assert.Equal(entity.Name, "a");
        }



    }
}
