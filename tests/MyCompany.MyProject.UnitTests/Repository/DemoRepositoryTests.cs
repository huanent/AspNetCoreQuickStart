using System;
using System.Linq;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Moq;
using MyCompany.MyProject.Application.Dtos.Demo;
using MyCompany.MyProject.Application.Entities.DemoAggregate;
using MyCompany.MyProject.Infrastructure.Repository;
using Xunit;

namespace MyCompany.MyProject.UnitTests.Repository
{
    public class DemoRepositoryTests
    {
        [Fact]
        private async System.Threading.Tasks.Task GetPageAsyncTestAsync()
        {
            var ops = new DbContextOptionsBuilder().UseInMemoryDatabase("test").Options;
            var dateTime = new Mock<IDateTime>();
            dateTime.Setup(s => s.Now).Returns(DateTime.Now);
            var session = new Mock<IAppSession>();
            session.Setup(s => s.CancellationToken).Returns(default(CancellationToken));
            var ctx = new AppDbContext(ops, dateTime.Object);
            ctx.Demo.AddRange(new[] {
                new Demo("a"),
                new Demo("b"),
                new Demo("c"),
                new Demo("d"),
            });
            ctx.SaveChanges();
            var rsy = new DemoRepository(ctx, session.Object);
            var dto = new GetDemoPageDto
            {
                Index = 1,
                Size = 2,
            };

            var rsp = await rsy.GetPageAsync(dto);

            Assert.Equal(2, rsp.List.Count());
            Assert.Equal("a", rsp.List.First().Name);
            Assert.Equal("b", rsp.List.Last().Name);
            Assert.Equal(4, rsp.Total);
        }
    }
}
