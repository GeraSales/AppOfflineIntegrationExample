using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;

namespace PocSharedFile
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, Exported = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity, IBroadcast
    {
        public void SendRequest(DataReceiverModel data)
        {
            
            var context = Android.App.Application.Context;

            Intent intent = new Intent("com.integration.action");
            Bundle extras = new Bundle();

            extras.PutString(Constants.BODY, data.Body);
            extras.PutString(Constants.DISTRIBUTOR_CODE, data.Code);
            extras.PutString(Constants.FV_INTENT_ACTION_NAME, "br.com.share.SEND_REQUEST");
            extras.PutString(Constants.TOKEN, data.Token);
            extras.PutString(Constants.FV_PACKAGE, "br.com.share");
            extras.PutString(Constants.FV_CLASS_NAME, "br.com.share.DataReceiver");
            extras.PutString(Constants.FV_VENDOR_CODE, "AVG785798");
            extras.PutString(Constants.FV_VENDOR_NAME, "Seu Valdemar");
            extras.PutString(Constants.FV_APP_NAME, "Força de Venda");
            extras.PutInt(Constants.REDIRECT_SERVICE, (int)data.RedirectService);

            if (data.RedirectService is 2 || data.RedirectService is 3)
            {
                string[] includeOptions = { "a", "r", "t", "c" };
                extras.PutStringArray(Constants.INCLUDE_OPTIONS, includeOptions);
            }

            intent.PutExtras(extras);
            intent.SetClassName("com.integration", "com.integration.class");
            intent.SetPackage("com.integration");

            context.SendBroadcast(intent);
        }
    }
}
