namespace ApplicationCore.Entities
{
    public class Demo : EntityBase
    {
        private Demo()
        {

        }

        public Demo(string name)
        {
            Name = name;
        }

        public void Update(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

    }
}
