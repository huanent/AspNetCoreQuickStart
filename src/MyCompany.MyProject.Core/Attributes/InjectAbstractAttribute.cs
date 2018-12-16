using System;
using System.Collections.Generic;
using System.Text;

namespace MyCompany.MyProject
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public abstract class InjectAbstractAttribute : Attribute
    {
        public InjectAbstractAttribute(Type type = null)
        {
            Type = type;
        }

        public Type Type { get; private set; }
    }
}
