using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Controls.UserDialogs.Maui;
using System.Collections.ObjectModel;

namespace PocSharedFile
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly IBroadcast _broadcast;
        private readonly IUserDialogs _userDialogs;

        [ObservableProperty]
        private ObservableCollection<string> services;

        [ObservableProperty]
        private DataReceiverModel dataReceiver;

        [ObservableProperty]
        private int pickerIndex;

        [ObservableProperty]
        private string? response;

        [ObservableProperty]
        private string? body;

        [ObservableProperty]
        private int? statusCode = 0;

        [ObservableProperty]
        private int? errorCode = 0;

        [ObservableProperty]
        private string? exceptionMessage;

        [ObservableProperty]
        private string? token;

        [ObservableProperty]
        private string? distributorCode;

        [ObservableProperty]
        private string? errorCodeDescription;

        [ObservableProperty]
        private string? reasonPhrase;

        public MainViewModel(IBroadcast broadcast, IUserDialogs userDialogs)
        {
            _broadcast = broadcast;
            _userDialogs = userDialogs;

            Services = new ObservableCollection<string>();
            Services.Add("Sync");
            Services.Add("Create Order");
            Services.Add("Apply Promotion");
            Services.Add("Change Status");
            Services.Add("Industry Segmentation");
            Services.Add("Portifolio");
            Services.Add("Product Price");
            Services.Add("Sales Restriction");
           
            WeakReferenceMessenger.Default.Register<DataReceiverModel>(this, (r, m) =>
            {
                GetResponse(m);
                _userDialogs.HideHud();
            });
        }

        [RelayCommand]
        public void Submit()
        {
            DataReceiver = new DataReceiverModel
            {
                Body = Body,
                RedirectService = PickerIndex + 1,
                Token = Token,
                Code = DistributorCode,
            };

            _userDialogs.ShowLoading("Aguarde...");
            _broadcast.SendRequest(DataReceiver);
        }

        [RelayCommand]
        public void PickerChangeIndex()
        {
            Response = string.Empty;
            StatusCode = 0;
            ReasonPhrase = string.Empty;
            Body = string.Empty;
            ErrorCode = 0;
            ErrorCodeDescription = string.Empty;
        }

        [RelayCommand]
        public async Task CopyToClipboard() => await Clipboard.Default.SetTextAsync(Response);
  
        private void GetResponse(DataReceiverModel data)
        {
            Response = data.Response;
            StatusCode = data.StatusCode;
            ReasonPhrase = data.ReasonPhrase;
            ErrorCode = data.ErrorCode;
            ExceptionMessage = data.ExceptionMessage;
            ErrorCodeDescription = data.ErrorCodeDescription;
        }
    }
}
