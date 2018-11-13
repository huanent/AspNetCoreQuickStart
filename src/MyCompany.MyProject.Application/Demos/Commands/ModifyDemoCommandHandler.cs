using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyCompany.MyProject.Persistence;

namespace MyCompany.MyProject.Application.Demos.Commands
{
    internal class ModifyDemoCommandHandler : IRequestHandler<ModifyDemoCommand>
    {
        private readonly AppDbContext _appDbContext;

        public ModifyDemoCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Task<Unit> Handle(ModifyDemoCommand request, CancellationToken cancellationToken)
        {
            var demo = _appDbContext.Demo.Find(request.Id);
            if (demo == null) throw new BadRequestException("未能找到要修改的数据");
            demo.Update(request.Age);
            _appDbContext.SaveChangesAsync(cancellationToken);
            return Unit.Task;
        }
    }
}
