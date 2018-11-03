using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyCompany.MyProject.Commands.Demo;
using MyCompany.MyProject.Data;
using MyCompany.MyProject.Dto.Demo;

namespace MyCompany.MyProject.Handlers.Demo
{
    public class GetDemosRequestHandler : IRequestHandler<GetDemosRequest, IEnumerable<DemoDto>>
    {
        private readonly AppDbContext _appDbContext;

        public GetDemosRequestHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Task<IEnumerable<DemoDto>> Handle(GetDemosRequest request, CancellationToken cancellationToken)
        {
            var demos = _appDbContext.Demo
                .Select(s => new DemoDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Age = s.Age
                })
                .ToArray()
                .AsEnumerable();

            return Task.FromResult(demos);
        }
    }
}
