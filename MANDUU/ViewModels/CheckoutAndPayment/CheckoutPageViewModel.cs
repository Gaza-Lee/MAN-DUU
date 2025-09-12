using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MANDUU.RegexValidation;
using MANDUU.Services;
using MANDUU.ViewModels.Base;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Devices.Sensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MANDUU.ViewModels.CheckoutAndPayment
{
    public partial class CheckoutPageViewModel : BaseViewModel
    {
        private readonly IUserService _userService;

        [ObservableProperty]
        private string _fullName = string.Empty;

        [ObservableProperty]
        private string _residentialAddress = string.Empty;

        [ObservableProperty]
        private string _city = string.Empty;

        [ObservableProperty]
        private string _currentLocation = string.Empty;

        public CheckoutPageViewModel(INavigationService navigationService, IUserService userService) : base(navigationService)
        {          
            _userService = userService;
        }

        [RelayCommand]
        private async Task UseCurrentLocationAsync()
        {
            await IsBusyFor(async () =>
            {
                try
                {
                    var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

                    if (status != PermissionStatus.Granted)
                    {
                        status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                        if (status != PermissionStatus.Granted)
                        {
                            ShowToast("Location permission denied. Please enable in settings.");
                            return;
                        }
                    }

                    var request = new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(10));
                    var location = await Geolocation.Default.GetLocationAsync(request);

                    if (location == null)
                    {
                        ShowToast("Unable to get location. Please enter manually.");
                        return;
                    }

                    var placemarks = await Geocoding.Default.GetPlacemarksAsync(location.Latitude, location.Longitude);
                    var placemark = placemarks?.FirstOrDefault();

                    if (placemark != null)
                    {
                        var addressLines = new List<string>
                        {
                            placemark.Thoroughfare ?? "",
                            placemark.SubThoroughfare ?? "",
                            placemark.Locality ?? placemark.AdminArea ?? ""
                        };

                        CurrentLocation = string.Join(", ", addressLines.Where(s => !string.IsNullOrWhiteSpace(s)));

                        ResidentialAddress = (placemark.Thoroughfare ?? "") + " " + (placemark.SubThoroughfare ?? "").Trim();
                        City = placemark.Locality ?? placemark.AdminArea ?? "";
                    }
                    else
                    {
                        CurrentLocation = $"Lat: {location.Latitude:F4}, Lng: {location.Longitude:F4}";
                        ShowToast("Location retrieved but no readable address found. Please fill in manually.");
                    }
                }
                catch (Exception ex)
                {
                    ShowToast($"Error retrieving location: {ex.Message}");
                }
            });
        }

        [RelayCommand]
        private async Task ProceedToPaymentAsync()
        {
            await IsBusyFor(async () =>
            {
                if (!InputValidation.IsValidName(FullName))
                {
                    ShowToast("Please enter a valid full name.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(ResidentialAddress))
                {
                    ShowToast("Residential address is required.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(City))
                {
                    ShowToast("City is required.");
                    return;
                }

                await NavigationService.NavigateToAsync("paymentpage", new Dictionary<string, object>
                {
                    ["FullName"] = FullName,
                    ["Address"] = ResidentialAddress,
                    ["City"] = City,

                });
            });
        }

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            var currentuser =await _userService.GetCurrentUserAsync();
            if (currentuser != null)
            {
                FullName = currentuser.FullName;
            }
        }
    }
}