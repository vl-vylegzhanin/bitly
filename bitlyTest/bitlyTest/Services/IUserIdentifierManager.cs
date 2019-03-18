namespace bitlyTest.Services
{
    public interface IUserIdentifierManager
    {
        string GetOrGenerateUserIdentifier(string userId);
    }
}