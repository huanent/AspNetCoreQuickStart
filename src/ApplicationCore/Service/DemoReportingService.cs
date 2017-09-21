using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ApplicationCore.Service
{
    public class DemoReportingService : IDemoReportingService
    {
        IRepository<Demo> _demoRepository;
        IJsonConventer _jsonConventer;

        public DemoReportingService(
            IRepository<Demo> demoRepository,
            IJsonConventer jsonConventer
            )
        {
            _demoRepository = demoRepository;
            _jsonConventer = jsonConventer;
        }
        public string ExportDemoToJson()
        {
            var demoList = _demoRepository.Query(new List<Expression<Func<Demo, object>>> {
                d=>d.DemoItems
            });
            return _jsonConventer.ToJson(demoList);
        }
    }
}
