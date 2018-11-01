using System.Linq;
using MediatR;
using MyCompany.MyProject.Commands.Demo;
using MyCompany.MyProject.Data;
using MyCompany.MyProject.Dto.Demo;

namespace MyCompany.MyProject.Handlers.Demo
{
    public class FindDemoByIdRequestHandler : RequestHandler<FindDemoByIdRequest, DemoDto>
    {
        private readonly AppDbContext _appDbContext;

        public FindDemoByIdRequestHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        protected override DemoDto Handle(FindDemoByIdRequest request)
        {
            return _appDbContext.Demo
               .Select(s => new DemoDto
               {
                   Id = s.Id,
                   Name = s.Name
               }).FirstOrDefault();
        }
    }
}
