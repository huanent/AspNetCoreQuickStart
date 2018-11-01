using System;
using System.Collections.Generic;
using System.Text;

namespace MyCompany.MyProject.Data
{
    public interface ISequenceGuidGenerator
    {
        Guid MySqlKey(bool oldGuids);
    }
}
