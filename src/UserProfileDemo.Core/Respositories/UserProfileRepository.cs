using System;
using Couchbase.Lite;
using UserProfileDemo.Core.Models;

namespace UserProfileDemo.Core.Respositories
{
    public class UserProfileRepository : BaseRepository
    {
        #region Properties

        static readonly Lazy<UserProfileRepository> lazy = new Lazy<UserProfileRepository>(() => new UserProfileRepository());
        public static UserProfileRepository Instance => lazy.Value;

        #endregion

        UserProfileRepository() : base("cbsample")
        { }

        public UserProfile GetUserProfile(string userProfileId)
        {
            UserProfile userProfile = null;

            var document = Database.GetDocument(userProfileId);

            if (document != null)
            {
                userProfile = new UserProfile
                {
                    Id = document.Id,
                    Name = document.GetString("Name"),
                    Email = document.GetString("Email"),
                    Address = document.GetString("Address"),
                    ImageData = document.GetBlob("ImageData")?.Content
                };
            }

            return userProfile;
        }

        public void SaveUserProfile(UserProfile userProfile)
        {
            if (userProfile != null)
            {
                var mutableDocument = new MutableDocument(userProfile.Id);
                mutableDocument.SetString("Name", userProfile.Name);
                mutableDocument.SetString("Email", userProfile.Email);
                mutableDocument.SetString("Address", userProfile.Address);

                if (userProfile.ImageData != null)
                {
                    mutableDocument.SetBlob("ImageData", new Blob("image/jpeg", userProfile.ImageData));
                }

                Database.Save(mutableDocument);
            }
        }
    }
}


