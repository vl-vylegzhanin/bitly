using System;

namespace bitlyTest.Services
{
    public class UserIdentifierManager : IUserIdentifierManager
    {
        public string GetOrGenerateUserIdentifier(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
                return userId;

            return Guid.NewGuid().ToString();
        }
    }
}
