using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using SocialLoginApp.iOS.Authetication;
using SocialLoginApp.Authentication;

[assembly: Xamarin.Forms.Dependency(typeof(SocialAuthentication))]
namespace SocialLoginApp.iOS.Authetication
{
    public class SocialAuthentication : IAuthentication
    {
        public async Task<MobileServiceUser> LoginAsync(MobileServiceClient client, MobileServiceAuthenticationProvider provider, IDictionary<string, string> parameters = null)
        {

            try
            {
                var current = GetController();
                var user = await client.LoginAsync(current, provider);
                SocialLoginApp.Helpers.Settings.AuthToken = user?.MobileServiceAuthenticationToken ?? string.Empty;
                SocialLoginApp.Helpers.Settings.UserId = user?.UserId;
                return user;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private UIKit.UIViewController GetController()
        {
            var window = UIKit.UIApplication.SharedApplication.KeyWindow;
            var root = window.RootViewController;
            if (root == null) return null;
            var current = root;
            while (current.PresentedViewController != null)
                current = current.PresentedViewController;
            return current;
        }
    }
}