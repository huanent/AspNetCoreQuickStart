using System;

namespace MyCompany.MyProject.Core.Internal
{
    public class CurrentIdentity : ICurrentIdentity
    {
        public Guid Id { get; private set; }

        public bool IsAuth { get; private set; }

        public void SetClaims(Guid id)
        {
            IsAuth = true;
            Id = id;
        }
    }
}
