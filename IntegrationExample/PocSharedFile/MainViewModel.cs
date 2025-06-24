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
        private string? optionalBody;

        [ObservableProperty]
        private bool optionalBodyIsVisible;

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

        [ObservableProperty]
        private bool awardsIsChecked;

        [ObservableProperty]
        private bool requirementsIsChecked;

        [ObservableProperty]
        private bool targetTypeProductsIsChecked;

        [ObservableProperty]
        private bool considerAllIndustriesIsChecked;

        [ObservableProperty]
        private bool returnNotAcquiredPromotionsIsChecked;

        [ObservableProperty]
        private bool optionalAwardsIsChecked;

        [ObservableProperty]
        private bool optionalRequirementsIsChecked;

        [ObservableProperty]
        private bool optionalTargetTypeProductsIsChecked;

        [ObservableProperty]
        private bool optionalConsiderAllIndustriesIsChecked;

        [ObservableProperty]
        private bool optionalReturnNotAcquiredPromotionsIsChecked;

        [ObservableProperty]
        private bool includeOptionsIsVisible;

        [ObservableProperty]
        private bool optionalIncludeOptionsIsVisible;

        [ObservableProperty]
        private bool applyIncludeOptionsIsVisible;


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
            Services.Add("Close Order");
            Services.Add("Consolidated Recommendations");
            Services.Add("Get Authorization");

            WeakReferenceMessenger.Default.Register<DataReceiverModel>(this, (r, m) =>
            {
                GetResponse(m);
                //_userDialogs.HideHud();
            });
        }

        [RelayCommand]
        public void Submit()
        {
            List<string> includeOptions = new();
            List<string>? optionalIncludeOptions = new();
            DataReceiver = new DataReceiverModel
            {
                Body = Body,
                OptionalBody = OptionalBody,
                RedirectService = PickerIndex == 10 ? PickerIndex + 2 : PickerIndex + 1,
                Token = Token,
                Code = DistributorCode,
            };

            if (AwardsIsChecked) includeOptions.Add("awards");
            if (RequirementsIsChecked) includeOptions.Add("requirements");
            if (TargetTypeProductsIsChecked) includeOptions.Add("targetTypeProducts");
            if (ConsiderAllIndustriesIsChecked) includeOptions.Add("considerAllIndustries");
            if (ReturnNotAcquiredPromotionsIsChecked) includeOptions.Add("returnNotAcquiredPromotions");
            if (OptionalAwardsIsChecked) optionalIncludeOptions.Add("awards");
            if (OptionalRequirementsIsChecked) optionalIncludeOptions.Add("requirements");
            if (OptionalTargetTypeProductsIsChecked) optionalIncludeOptions.Add("targetTypeProducts");
            if (OptionalConsiderAllIndustriesIsChecked) optionalIncludeOptions.Add("considerAllIndustries");
            if (OptionalReturnNotAcquiredPromotionsIsChecked) optionalIncludeOptions.Add("returnNotAcquiredPromotions");

            //_userDialogs.ShowLoading("Aguarde...");
            _broadcast.SendRequest(DataReceiver, includeOptions.ToArray(), optionalIncludeOptions?.ToArray());
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
            OptionalBodyIsVisible = PickerIndex == 8;
            OptionalBody = string.Empty;
            AwardsIsChecked = false;
            RequirementsIsChecked = false;
            TargetTypeProductsIsChecked = false;
            ConsiderAllIndustriesIsChecked = false;
            ReturnNotAcquiredPromotionsIsChecked = false;
            OptionalAwardsIsChecked = false;
            OptionalRequirementsIsChecked = false;
            OptionalTargetTypeProductsIsChecked = false;
            OptionalConsiderAllIndustriesIsChecked = false;
            OptionalReturnNotAcquiredPromotionsIsChecked = false;
            IncludeOptionsIsVisible = PickerIndex == 1 || PickerIndex == 2 || PickerIndex == 8;
            OptionalIncludeOptionsIsVisible = PickerIndex == 8;
            ApplyIncludeOptionsIsVisible = PickerIndex == 2;
        }

        [RelayCommand]
        public async Task CopyToClipboard() => await Clipboard.Default.SetTextAsync(Response);

        private async void GetResponse(DataReceiverModel data)
        {
            if (data.RedirectService == 11)
            {
                await _userDialogs.AlertAsync(data.Response, "Authorization", "ok");
                return;
            }

            Response = data.Response;
            StatusCode = data.StatusCode;
            ReasonPhrase = data.ReasonPhrase;
            ErrorCode = data.ErrorCode;
            ExceptionMessage = data.ExceptionMessage;
            ErrorCodeDescription = data.ErrorCodeDescription;


        }
    }
}
