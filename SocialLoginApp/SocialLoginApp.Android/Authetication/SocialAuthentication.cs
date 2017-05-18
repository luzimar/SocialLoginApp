using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using SocialLoginApp.Authentication;
using SocialLoginApp.Droid.Authetication;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(SocialAuthentication))]
namespace SocialLoginApp.Droid.Authetication
{
    public class SocialAuthentication : IAuthentication
    {
        public async Task<MobileServiceUser> LoginAsync(MobileServiceClient client, MobileServiceAuthenticationProvider provider, IDictionary<string, string> parameters = null)
        {

            try
            {
                var user = await client.LoginAsync(Forms.Context, provider);
                SocialLoginApp.Helpers.Settings.AuthToken = user?.MobileServiceAuthenticationToken ?? string.Empty;
                SocialLoginApp.Helpers.Settings.UserId = user?.UserId;
                return user;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}