using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using AndroidX.ConstraintLayout.Widget;

namespace PocSharedFile
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, Exported = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity, IBroadcast
    {
        public void SendRequest(DataReceiverModel data, string[]? includeOptions, string[]? optionalIncludeOptions)
        {

            var context = Android.App.Application.Context;

            Intent intent = new Intent("br.com.gera.gerapromo.BUNDLE_RECEIVER");
            Bundle extras = new Bundle();

            extras.PutString(Constants.BODY, data.Body);
            extras.PutString(Constants.OPTIONAL_BODY, data.OptionalBody);
            extras.PutString(Constants.DISTRIBUTOR_CODE, data.Code);
            extras.PutString(Constants.FV_INTENT_ACTION_NAME, "br.com.share.SEND_REQUEST");
            extras.PutString(Constants.TOKEN, data.Token);
            extras.PutString(Constants.FV_PACKAGE, "br.com.share");
            extras.PutString(Constants.FV_CLASS_NAME, "br.com.share.DataReceiver");
            extras.PutString(Constants.FV_VENDOR_CODE, "999");
            extras.PutString(Constants.FV_VENDOR_NAME, "Letícia Souza");
            extras.PutString(Constants.FV_APP_NAME, "Gera Hub Vendas");
            extras.PutInt(Constants.REDIRECT_SERVICE, (int)data.RedirectService);

            if (data.RedirectService is 2 || data.RedirectService is 3 || data.RedirectService is 9)
            {
                //string[] includeOptions = { "awards", "requirements", "targetTypeProducts"};
                extras.PutStringArray(Constants.INCLUDE_OPTIONS, includeOptions);
                extras.PutStringArray(Constants.OPTIONAL_INCLUDE_OPTIONS, optionalIncludeOptions);
            }

            intent.PutExtras(extras);
            intent.SetClassName("br.com.gera.gerapromo", "br.com.gera.gerapromo.DataReceiver");
            intent.SetPackage("br.com.gera.gerapromo");

            context.SendBroadcast(intent);
        }
    }
}
