using System;

namespace MyCompany.MyProject.Domain.DemoAggregate
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
    }
}
