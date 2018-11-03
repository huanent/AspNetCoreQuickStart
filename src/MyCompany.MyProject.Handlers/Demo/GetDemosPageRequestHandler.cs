using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyCompany.MyProject.Commands.Demo;
using MyCompany.MyProject.Data;
using MyCompany.MyProject.Dto.Demo;
using MyCompany.MyProject.Dtos;

namespace MyCompany.MyProject.Handlers.Demo
{
    public class GetDemosPageRequestHandler : IRequestHandler<GetDemosPageRequest, PageDto<DemoDto>>
    {
        private readonly AppDbContext _appDbContext;

        public GetDemosPageRequestHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Task<PageDto<DemoDto>> Handle(GetDemosPageRequest request, CancellationToken cancellationToken)
        {
            var data = _appDbContext.Demo.AsNoTracking();
            data = data.IfHaveValue(request.MaxAge, q => q.Where(w => w.Age <= request.MaxAge));

            var demos = data
                .Select(s => new DemoDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Age = s.Age
                })
                .GetPage(request.PageIndex, request.PageSize)
                .ToArray()
                .AsEnumerable();

            var result = new PageDto<DemoDto>(data.Count(), demos);
            return Task.FromResult(result);
        }
    }
}
