namespace MyCompany.MyProject.Entities
{
    public class Demo : EntityBase
    {
        public Demo(string name, int age = 10)
        {
            Name = name;
            Age = age;
        }

        private Demo()
        {
        }

        public int Age { get; private set; }

        public string DemoInfo => $"{Name}今年{Age}岁";

        public string Name { get; private set; }

        public void Update(int age)
        {
            Age = age;
        }
    }
}
