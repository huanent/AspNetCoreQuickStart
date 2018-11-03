using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyCompany.MyProject.Commands.Demo;
using MyCompany.MyProject.Data;

namespace MyCompany.MyProject.Handlers.Demo
{
    public class CreateDemoRequestHandler : IRequestHandler<CreateDemoRequest>
    {
        private readonly AppDbContext _appDbContext;

        public CreateDemoRequestHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(CreateDemoRequest request, CancellationToken cancellationToken)
        {
            var demo = new Entities.Demo(request.Name);
            await _appDbContext.Demo.AddAsync(demo, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
