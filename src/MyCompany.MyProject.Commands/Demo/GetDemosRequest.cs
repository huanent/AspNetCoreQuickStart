using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using MyCompany.MyProject.Dto.Demo;

namespace MyCompany.MyProject.Commands.Demo
{
    public class GetDemosRequest : IRequest<IEnumerable<DemoDto>>
    {
    }
}
