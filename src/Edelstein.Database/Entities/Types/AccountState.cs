namespace Edelstein.Database.Entities.Types
{
    public enum AccountState
    {
        LoggedOut = 0,
        LoggingIn = 1,
        MigratingIn = 2,
        LoggedIn = 3
    }
}