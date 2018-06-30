using System;
using System.Collections.Generic;
using System.Text;

namespace MyCompany.MyProject.ApplicationCore.SharedKernel
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class InjectedAttribute : Attribute
    {
        public Lifecycle Lifecycle { get; private set; }

        public InjectedAttribute(Lifecycle lifecycle)
        {
            Lifecycle = lifecycle;
        }
    }

    public enum Lifecycle
    {
        Transient = 1,
        Singleton = 2,
        Scoped = 3
    }
}
