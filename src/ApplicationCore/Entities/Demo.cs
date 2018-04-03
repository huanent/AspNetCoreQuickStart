namespace ApplicationCore.Entities
{
    public class Demo : EntityBase
    {
        private Demo()
        {

        }

        public Demo(string name, int Age = 10)
        {
            Name = name;
        }

        public void Update(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public int Age { get; private set; }

        public string DemoInfo => $"{Name}今年{Age}岁";
    }
}
