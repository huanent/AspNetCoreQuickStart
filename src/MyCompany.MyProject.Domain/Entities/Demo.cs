using MyCompany.MyProject.Domain.Enumerations;

namespace MyCompany.MyProject.Domain.Entities
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

        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; private set; }

        public string DemoInfo => $"{Name}今年{Age}岁";

        /// <summary>
        /// 性别
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// 名字
        /// </summary>
        public string Name { get; private set; }

        public void Update(int age)
        {
            Age = age;
        }
    }
}
