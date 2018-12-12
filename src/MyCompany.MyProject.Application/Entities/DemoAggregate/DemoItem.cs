using System;
using System.Collections.Generic;

namespace MyCompany.MyProject.Application.Entities.DemoAggregate
{
    public class DemoItem : ValueBase
    {
        public DemoItem(string name, int sort)
        {
            Name = name;
            Sort = sort;
        }

        private DemoItem()
        {
        }

        public Guid DemoId { get; private set; }

        public string Name { get; private set; }

        public int Sort { get; private set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Name;
            yield return Sort;
        }
    }
}
