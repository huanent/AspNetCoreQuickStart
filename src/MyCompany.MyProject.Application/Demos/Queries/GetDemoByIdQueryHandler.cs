using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyCompany.MyProject.Application.Demos.Models;
using MyCompany.MyProject.Persistence;

namespace MyCompany.MyProject.Application.Demos.Queries
{
    public class GetDemoByIdQueryHandler : IRequestHandler<GetDemoByIdQuery, DemoModel>
    {
        private readonly DefaultDbContext _appDbContext;

        public GetDemoByIdQueryHandler(DefaultDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Task<DemoModel> Handle(GetDemoByIdQuery request, CancellationToken cancellationToken)
        {
            return Task.Run(() => _appDbContext.Demo
                .Select(s => new DemoModel
                {
                    Id = s.Id,
                    Name = s.Name
                }).FirstOrDefault());
        }
    }
}
