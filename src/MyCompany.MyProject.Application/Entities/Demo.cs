using System.Collections.Generic;
using System.Linq;

namespace MyCompany.MyProject.Application.Entities
{
    public class Demo : EntityBase
    {
        private readonly List<DemoItem> _demoItems = new List<DemoItem>();

        public Demo(string name)
        {
            Name = name;
        }

        private Demo()
        {
        }

        public IReadOnlyCollection<DemoItem> DemoItems => _demoItems.AsReadOnly();

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; private set; }

        public void AddItem(string itemName)
        {
            int sort = _demoItems.Max(m => m.Sort);
            var item = new DemoItem(itemName, ++sort);
            _demoItems.Add(item);
        }

        public void Update(string name)
        {
            Name = name;
        }
    }
}
