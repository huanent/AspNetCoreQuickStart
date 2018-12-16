using System;
using System.Collections.Generic;
using System.Text;

namespace MyCompany.MyProject
{
    public class InjectTransientAttribute : InjectAbstractAttribute
    {
        public InjectTransientAttribute(Type type = null) : base(type)
        {
        }
    }
}
