using System;

namespace MyCompany.MyProject.Persistence
{
    public interface ISequenceGuidGenerator
    {
        Guid MySqlKey(bool oldGuids);
    }
}
