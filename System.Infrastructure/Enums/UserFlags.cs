using System;

namespace SYF.Infrastructure.Enums
{
    [Flags]
    public enum UserFlags
    {
        None = 0,
        SystemAccount = 1,
        Locked = 2,
        SupportWidget = 4,
        Disabled = 8,
        PasswordBug = 16
    }
}
