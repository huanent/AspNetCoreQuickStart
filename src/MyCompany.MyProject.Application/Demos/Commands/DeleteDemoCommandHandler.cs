using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using MyCompany.MyProject.Persistence;

namespace MyCompany.MyProject.Application.Demos.Commands
{
    public class DeleteDemoCommandHandler : IRequestHandler<DeleteDemoCommand>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public DeleteDemoCommandHandler(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<Unit> Handle(DeleteDemoCommand request, CancellationToken cancellationToken)
        {
            var conn = _dbConnectionFactory.Default();
            var result = await conn.ExecuteAsync(@"
delete from Demo where Id=@Id
", new { request.Id });
            if (result == 0) throw new BadRequestException(Message.notFound);
            return Unit.Value;
        }
    }
}
