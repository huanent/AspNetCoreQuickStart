using ApplicationCore.ISharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.SharedKernel
{
    public class CurrentIdentity : ICurrentIdentity
    {
        public void SetIdentity(bool isLogin, Guid id)
        {
            IsLogin = IsLogin;
            Id = id;
        }

        public bool IsLogin { get; private set; }

        public Guid Id { get; private set; }

    }
}
