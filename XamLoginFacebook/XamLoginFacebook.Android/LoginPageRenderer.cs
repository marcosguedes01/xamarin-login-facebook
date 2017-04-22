using Android.App;
using Newtonsoft.Json.Linq;
using System;
using Xamarin.Auth; // Versão 1.3.2
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamLoginFacebook;
using XamLoginFacebook.Droid;

[assembly: ExportRenderer(typeof(Login), typeof(LoginPageRenderer))]
namespace XamLoginFacebook.Droid
{
    public class LoginPageRenderer : PageRenderer
    {
        public LoginPageRenderer()
        {
            var activity = this.Context as Activity;

            var auth = new OAuth2Authenticator(
                clientId: "", // Criar em: https://developers.facebook.com/apps
                scope: "",
                authorizeUrl: new Uri("https://m.facebook.com/dialog/oauth/"),
                redirectUrl: new Uri("http://www.facebook.com/connect/login_success.html")
                );

            auth.Completed += async (sender, eventArgs) =>
            {
                if (eventArgs.IsAuthenticated)
                {
                    var accessToken = eventArgs.Account.Properties["access_token"].ToString();
                    var expiresIn = Convert.ToDouble(eventArgs.Account.Properties["expires_in"]);
                    var expiryDate = DateTime.Now + TimeSpan.FromSeconds(expiresIn);

                    var request = new OAuth2Request("GET", new Uri("https://graph.facebook.com/me"), null, eventArgs.Account);
                    var response = await request.GetResponseAsync();
                    var obj = JObject.Parse(response.GetResponseText());

                    var id = obj["id"].ToString().Replace("\"", "");
                    var name = obj["name"].ToString().Replace("\"", "");

                    App.NavigateToProfile(string.Format("Olá {0}", name));
                }
                else
                {
                    App.NavigateToProfile("Usuário cancelou o login");
                }
            };

            activity.StartActivity(auth.GetUI(activity));
        }
    }
}