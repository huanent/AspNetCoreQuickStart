using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class DemoItem : Entity
    {
        public int Order { get; set; }

        public Guid DemoId { get; set; }
    }
}
