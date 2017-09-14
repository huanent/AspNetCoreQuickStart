using ApplicationCore.Dtos;
using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Services
{
    public interface IDemoService
    {
        Demo GetDemoByKey(Guid key);

        bool SaveDemo(DemoDto demoDto);
    }
}
