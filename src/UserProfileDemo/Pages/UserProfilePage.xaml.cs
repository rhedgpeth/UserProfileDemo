using System;
using UserProfileDemo.Core;
using UserProfileDemo.Core.Respositories;
using UserProfileDemo.Core.Services;
using UserProfileDemo.Core.ViewModels;
using Xamarin.Forms;

namespace UserProfileDemo.Pages
{
    public partial class UserProfilePage : ContentPage
    {
        UserProfileViewModel ViewModel { get; set; }

        public UserProfilePage(Action logoutSuccesful)
        {
            InitializeComponent();

            // Set up in place of having a dependency on a DI solution
            var userProfileRepository = ServiceContainer.Resolve<IUserProfileRepository>();
            var alertService = ServiceContainer.Resolve<IAlertService>();
            var mediaService = ServiceContainer.Resolve<IMediaService>();

            BindingContext = ViewModel = new UserProfileViewModel(userProfileRepository, alertService, mediaService, logoutSuccesful);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            ViewModel.Dispose();
        }
    }
}
