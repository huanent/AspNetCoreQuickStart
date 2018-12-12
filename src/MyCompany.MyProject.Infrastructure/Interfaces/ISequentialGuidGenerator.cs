using System;

namespace MyCompany.MyProject.Infrastructure
{
    public interface ISequentialGuidGenerator
    {
        Guid Next();
    }
}
