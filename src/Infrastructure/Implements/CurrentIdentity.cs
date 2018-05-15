using ApplicationCore;
using System;

namespace Infrastructure.Implements
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
