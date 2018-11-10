using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyCompany.MyProject.Commands.Demo;
using MyCompany.MyProject.Data;

namespace MyCompany.MyProject.Handlers.Demo
{
    public class ModifyDemoRequestHandler : IRequestHandler<ModifyDemoRequest>
    {
        private readonly AppDbContext _appDbContext;

        public ModifyDemoRequestHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Task<Unit> Handle(ModifyDemoRequest request, CancellationToken cancellationToken)
        {
            var demo = _appDbContext.Demo.Find(request.Id);
            if (demo == null) throw new BadRequestException("未能找到要修改的数据");
            demo.Update(request.Age);
            _appDbContext.SaveChangesAsync(cancellationToken);
            return Unit.Task;
        }
    }
}
