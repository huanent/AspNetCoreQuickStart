using System;
using System.Collections.Generic;
using System.Text;

namespace MyCompany.MyProject
{
    public class InjectScopedAttribute : InjectAbstractAttribute
    {
        public InjectScopedAttribute(Type type = null) : base(type)
        {
        }
    }
}
