using System;
using UserProfileDemo.Models;

namespace UserProfileDemo.Core.Respositories
{
    public interface IUserProfileRepository : IDisposable
    {
        UserProfile GetUserProfile(string userProfileId);
        void SaveUserProfile(UserProfile userProfile);
    }
}
