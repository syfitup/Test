
namespace SYF.Infrastructure.Enums
{
    public enum AuthenticateFailureReason
    {
        InvalidCredentials = 1,
        SystemAccount = 2,
        LockedOut = 3,
        Disabled = 4,
        NotAllowed = 5
    }
}
