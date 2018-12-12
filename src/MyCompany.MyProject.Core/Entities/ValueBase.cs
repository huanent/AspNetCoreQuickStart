using System;
using System.Collections.Generic;
using System.Linq;

namespace MyCompany.MyProject
{
    public class ValueBase<T> where T : struct
    {
        public T Id { get; set; }
    }

    public abstract class ValueBase : ValueBase<Guid>
    {
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }
            var other = (ValueBase)obj;
            var thisValues = GetAtomicValues().GetEnumerator();
            var otherValues = other.GetAtomicValues().GetEnumerator();
            while (thisValues.MoveNext() && otherValues.MoveNext())
            {
                if (ReferenceEquals(thisValues.Current, null) ^ ReferenceEquals(otherValues.Current, null))
                {
                    return false;
                }
                if (thisValues.Current != null && !thisValues.Current.Equals(otherValues.Current))
                {
                    return false;
                }
            }
            return !thisValues.MoveNext() && !otherValues.MoveNext();
        }

        public ValueBase GetCopy()
        {
            return MemberwiseClone() as ValueBase;
        }

        public override int GetHashCode()
        {
            return GetAtomicValues()
             .Select(x => x != null ? x.GetHashCode() : 0)
             .Aggregate((x, y) => x ^ y);
        }

        protected abstract IEnumerable<object> GetAtomicValues();
    }
}
