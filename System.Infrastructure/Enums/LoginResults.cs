namespace SYF.Infrastructure.Enums
{
    public enum LoginResults
    {
        Success = 0,
        InvalidUserName = 1,
        InvalidPassword = 2,
        PasswordNotSet = 3,
        SystemAccount = 4,
        LockedOut = 5,
        Disabled = 6
    }
}
