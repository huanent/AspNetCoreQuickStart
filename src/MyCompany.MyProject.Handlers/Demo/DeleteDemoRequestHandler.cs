using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyCompany.MyProject.Commands.Demo;
using MyCompany.MyProject.Data;
using Dapper;

namespace MyCompany.MyProject.Handlers.Demo
{
    public class DeleteDemoRequestHandler : IRequestHandler<DeleteDemoRequest>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public DeleteDemoRequestHandler(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<Unit> Handle(DeleteDemoRequest request, CancellationToken cancellationToken)
        {
            var conn = _dbConnectionFactory.Default();
            var result = await conn.ExecuteAsync(@"
delete from Demo where Id=@Id
", new { request.Id });
            if (result == 0) throw new AppException("未能找到要删除的数据");
            return Unit.Value;
        }
    }
}
