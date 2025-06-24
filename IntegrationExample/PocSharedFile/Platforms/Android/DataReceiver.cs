using Android.Content;
using Android.OS;
using CommunityToolkit.Mvvm.Messaging;

namespace PocSharedFile.Platforms.Android
{
    [BroadcastReceiver(Name = "br.com.share.DataReceiver", Enabled = true, Exported = true)]
    public class DataReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context? context, Intent? intent)
        {
            var receiverObj = CreateReceiverObj(intent?.Extras);
            WeakReferenceMessenger.Default.Send(receiverObj);
        }

        private DataReceiverModel CreateReceiverObj(Bundle? bundle)
        {
            return new DataReceiverModel
            {
                StatusCode = bundle?.GetInt(Constants.HTTP_STATUS_CODE),
                ReasonPhrase = bundle?.GetString(Constants.REASON_PHRASE),
                Response = bundle?.GetString(Constants.RESPONSE),
                ExceptionMessage = bundle?.GetString(Constants.EXCEPTION_MESSAGE),
                ErrorCode = bundle?.GetInt(Constants.ERROR_CODE),
                ErrorCodeDescription = bundle?.GetString(Constants.ERROR_CODE_DESCRIPTION),
                RedirectService = bundle?.GetInt(Constants.REDIRECT_SERVICE),
            };
        }


    }
}
