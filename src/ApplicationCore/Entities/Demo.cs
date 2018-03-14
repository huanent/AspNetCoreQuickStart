using ApplicationCore.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Demo : EntityBase
    {
        public Demo()
        {

        }

        public Demo(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

    }
}
