using System;

namespace MyCompany.MyProject.Persistence
{
    public interface ISequentialGuidGenerator
    {
        Guid Next();
    }
}
