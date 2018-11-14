using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyCompany.MyProject.Domain.DemoAggregate;
using MyCompany.MyProject.Persistence;

namespace MyCompany.MyProject.Application.Demos.Commands
{
    public class CreateDemoCommandHandler : IRequestHandler<CreateDemoCommand>
    {
        private readonly AppDbContext _appDbContext;

        public CreateDemoCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(CreateDemoCommand request, CancellationToken cancellationToken)
        {
            var demo = new Demo(request.Name);
            await _appDbContext.Demo.AddAsync(demo, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
