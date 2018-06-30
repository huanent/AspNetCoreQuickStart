using MyCompany.MyProject.ApplicationCore;
using MyCompany.MyProject.ApplicationCore.SharedKernel;
using System;

namespace MyCompany.MyProject.Infrastructure.Implements
{
    public class CurrentIdentity : ICurrentIdentity
    {
        public void SetIdentity(bool isLogin, Guid id)
        {
            IsLogin = isLogin;
            Id = id;
        }

        public bool IsLogin { get; private set; }

        public Guid Id { get; private set; }

    }
}
