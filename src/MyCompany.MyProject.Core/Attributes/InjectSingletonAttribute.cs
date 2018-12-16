using System;
using System.Collections.Generic;
using System.Text;

namespace MyCompany.MyProject
{
    public class InjectSingletonAttribute : InjectAbstractAttribute
    {
        public InjectSingletonAttribute(Type type = null) : base(type)
        {
        }
    }
}
