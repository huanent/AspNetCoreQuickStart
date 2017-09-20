using System.Collections.Generic;

namespace ApplicationCore.Entities
{
    public class Demo : Entity
    {
        public string Name { get; set; }

        public ICollection<DemoItem> DemoItems { get; set; }
    }
}
