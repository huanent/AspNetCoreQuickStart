using System;

namespace MyCompany.MyProject.Domain
{
    public class ValueBase<T> where T : struct
    {
        public T Id { get; set; }
    }

    public class ValueBase : ValueBase<Guid>
    {
    }
}
