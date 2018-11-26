using System;

namespace MyCompany.MyProject.Web.Internal
{
    public class Identity : IIdentity
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
