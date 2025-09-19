using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MANDUU.Models;
using MANDUU.RegexValidation;
using MANDUU.Services;
using MANDUU.ViewModels.Base;
using CommunityToolkit.Maui.Core;   
using CommunityToolkit.Maui.Storage; 
using CommunityToolkit.Maui.Alerts;   
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Graphics;

namespace MANDUU.ViewModels
{
    public partial class GetVerifiedPageViewModel : BaseViewModel
    {
        private readonly IUserService _userService;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(StepText))]
        [NotifyPropertyChangedFor(nameof(PercentText))]
        [NotifyPropertyChangedFor(nameof(ProgressValue))]
        [NotifyPropertyChangedFor(nameof(NextButtonText))]
        [NotifyPropertyChangedFor(nameof(CanShowPreviousButton))]
        private int _selectedTabIndex = 0;

        #region Personal Info Properties
        [ObservableProperty] private string _firstName = string.Empty;
        [ObservableProperty] private string _lastName = string.Empty;
        [ObservableProperty] private DateTime _dateOfBirth = DateTime.Today.AddYears(-18);
        [ObservableProperty] private string _email = string.Empty;
        [ObservableProperty] private string _phoneNumber = string.Empty;
        [ObservableProperty] private string _residentialAddress = string.Empty;
        #endregion

        #region Location Info
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(LocationLabelText))]
        private bool _isStudent = false;

        [ObservableProperty] private string _location = string.Empty;
        public string LocationLabelText => IsStudent ? "Hall/Hostel of Residence" : "Store/Shop Location";
        #endregion

        #region ID Identification
        [ObservableProperty]
        private ObservableCollection<string> _idType = new() { "Student ID", "National Identity Card" };

        [ObservableProperty] private string _selectedIdType;
        [ObservableProperty] private string _idNumber = string.Empty;
        [ObservableProperty] private string _selectedDocument;
        [ObservableProperty] private FileResult _selectedFile;
        [ObservableProperty] private bool _isDocumentSelected = false;
        #endregion

        #region Face Verification Properties
        [ObservableProperty] private bool _isFaceCaptured = false;
        [ObservableProperty] private ImageSource _capturedImage;
        [ObservableProperty] private string _verificationStatus = "Not Started";
        [ObservableProperty] private Color _verificationStatusColor = Colors.Gray;
        [ObservableProperty] private bool _isVerificationSuccessful = false;
        [ObservableProperty] private bool _isProcessing = false;
        #endregion

        #region Computed Properties
        public string StepText => $"Step {SelectedTabIndex + 1} of 4";
        public string PercentText => $"{(SelectedTabIndex + 1) * 25}%";
        public double ProgressValue => (SelectedTabIndex + 1) * 0.25;
        public string NextButtonText => SelectedTabIndex == 3 ? "Complete Verification" : "Next";
        public bool CanShowPreviousButton => SelectedTabIndex > 0;
        #endregion

        public GetVerifiedPageViewModel(INavigationService navigationService,
                                     IUserService userService) : base(navigationService)
        {
            _userService = userService;
            SelectedIdType = IdType.FirstOrDefault();
        }

        [RelayCommand]
        private void Previous()
        {
            if (SelectedTabIndex > 0)
                SelectedTabIndex--;
        }

        [RelayCommand]
        private async Task NextAsync()
        {
            await IsBusyFor(async () =>
            {
                bool isValid = false;

                switch (SelectedTabIndex)
                {
                    case 0:
                        if (!IsPersonalInfoFilled())
                        {
                            ShowToast("Please fill in all personal info fields.");
                            return;
                        }
                        isValid = await IsValidPersonalInfo();
                        break;

                    case 1:
                        if (string.IsNullOrWhiteSpace(Location))
                        {
                            ShowToast("Please enter your location.");
                            return;
                        }
                        isValid = true;
                        break;

                    case 2:
                        if (!IsIdVerificationFilled())
                        {
                            ShowToast("Please complete all ID verification fields.");
                            return;
                        }
                        if (!IsDocumentSelected)
                        {
                            ShowToast("Please upload your ID document.");
                            return;
                        }
                        isValid = true;
                        break;

                    case 3:
                        if (!IsVerificationSuccessful)
                        {
                            ShowToast("Please complete face verification first.");
                            return;
                        }
                        isValid = true;
                        break;

                    default:
                        isValid = true;
                        break;
                }

                if (!isValid) return;

                if (SelectedTabIndex < 3)
                    SelectedTabIndex++;
                else
                    await CompleteVerificationAsync();
            });
        }

        [RelayCommand]
        private async Task TakeSelfieAsync()
        {
            if (IsProcessing) return;

            await IsBusyFor(async () =>
            {
                IsProcessing = true;

                try
                {
                    var status = await Permissions.CheckStatusAsync<Permissions.Camera>();
                    if (status != PermissionStatus.Granted)
                    {
                        status = await Permissions.RequestAsync<Permissions.Camera>();
                        if (status != PermissionStatus.Granted)
                        {
                            ShowToast("Camera permission is required to take a selfie");
                            IsProcessing = false;
                            return;
                        }
                    }

                    var photo = await MediaPicker.Default.CapturePhotoAsync(new MediaPickerOptions
                    {
                        Title = "Take a selfie for verification"
                    });

                    if (photo != null)
                    {
                        var stream = await photo.OpenReadAsync();
                        CapturedImage = ImageSource.FromStream(() => stream);
                        IsFaceCaptured = true;

                        VerificationStatus = "Processing...";
                        VerificationStatusColor = Colors.Orange;
                        IsVerificationSuccessful = false;

                        ShowToast("Selfie captured! Processing verification...");
                        await ProcessFaceVerificationAsync();
                    }
                }
                catch (FeatureNotSupportedException)
                {
                    ShowToast("Camera is not supported on this device");
                }
                catch (PermissionException)
                {
                    ShowToast("Camera permission denied");
                }
                catch (Exception ex)
                {
                    ShowToast($"Error capturing image: {ex.Message}");
                }
                finally
                {
                    IsProcessing = false;
                }
            });
        }

        [RelayCommand]
        private async Task RetakeSelfieAsync()
        {
            IsFaceCaptured = false;
            CapturedImage = null;
            VerificationStatus = "Not Started";
            VerificationStatusColor = Colors.Gray;
            IsVerificationSuccessful = false;
            ShowToast("Ready to take another selfie");
        }

        private async Task ProcessFaceVerificationAsync()
        {
            await Task.Delay(2000);

            try
            {
                var user = await _userService.GetCurrentUserAsync();
                var isSuccess = await SimulateFaceVerification(user);

                if (isSuccess)
                {
                    IsVerificationSuccessful = true;
                    VerificationStatus = "Verified Successfully";
                    VerificationStatusColor = Colors.Green;
                    ShowToast("Face verification completed!");
                }
                else
                {
                    IsVerificationSuccessful = false;
                    VerificationStatus = "Verification Failed";
                    VerificationStatusColor = Colors.Red;
                    ShowToast("Face verification failed. Please try again.");
                }
            }
            catch (Exception ex)
            {
                IsVerificationSuccessful = false;
                VerificationStatus = "Error";
                VerificationStatusColor = Colors.Red;
                ShowToast($"Error during verification: {ex.Message}");
            }
        }

        private async Task<bool> SimulateFaceVerification(User user)
        {
            if (user?.Email?.Contains("bernardo", StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            var random = new Random();
            return random.Next(100) < 80;
        }

        private async Task CompleteVerificationAsync()
        {
            await IsBusyFor(async () =>
            {
                try
                {
                    var user = await _userService.GetCurrentUserAsync();
                    if (user != null)
                    {
                        user.IsFaceVerified = true;
                        user.VerificationStatus = "Completed";
                        user.VerificationDate = DateTime.UtcNow;

                        await _userService.UpdateUserAsync(user);

                        ShowToast("Verification process completed successfully!");
                        await NavigationService.PopAsync();
                    }
                }
                catch (Exception ex)
                {
                    ShowToast($"Error completing verification: {ex.Message}");
                }
            });
        }

        [RelayCommand]
        private async Task CaptureCurrentLocationAsync()
        {
            await IsBusyFor(async () =>
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
                    Location = "Unable to capture location";
                    ShowToast("Unable to get location. Please enter manually.");
                    return;
                }

                var placemarks = await Geocoding.Default.GetPlacemarksAsync(location.Latitude, location.Longitude);
                var placemark = placemarks?.FirstOrDefault();

                if (placemark != null)
                {
                    Location = $"{placemark.Thoroughfare ?? ""} {placemark.SubThoroughfare ?? ""}".Trim();
                    if (string.IsNullOrWhiteSpace(Location))
                        Location = placemark.Locality ?? placemark.AdminArea ?? "Unknown Location";
                }
                else
                {
                    Location = $"Lat: {location.Latitude:F4}, Lng: {location.Longitude:F4}";
                    ShowToast("Location retrieved but no readable address found. Please fill in manually.");
                }
            });
        }

        [RelayCommand]
        private async Task ChooseFileAsync()
        {
            await IsBusyFor(async () =>
            {
                try
                {
                    var customFileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                    {
                        { DevicePlatform.iOS, new[] { "com.adobe.pdf", "org.openxmlformats.wordprocessingml.document", "public.text" } },
                        { DevicePlatform.Android, new[] { "application/pdf", "application/msword", "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "text/plain" } },
                        { DevicePlatform.WinUI, new[] { ".pdf", ".doc", ".docx", ".txt" } },
                        { DevicePlatform.MacCatalyst, new[] { "com.adobe.pdf", "org.openxmlformats.wordprocessingml.document", "public.text" } }
                    });

                    var result = await FilePicker.Default.PickAsync(new PickOptions
                    {
                        PickerTitle = "Select a document",
                        FileTypes = customFileTypes
                    });

                    if (result != null)
                    {
                        SelectedDocument = result.FileName;
                        SelectedFile = result;
                        IsDocumentSelected = true;
                        ShowToast($"{result.FileName} selected");
                    }
                }
                catch (Exception ex)
                {
                    ShowToast($"Error: {ex.Message}");
                }
            });
        }

        #region Validation Methods
        private bool IsPersonalInfoFilled()
        {
            var missing = new List<string>();
            if (string.IsNullOrWhiteSpace(FirstName)) missing.Add("First Name");
            if (string.IsNullOrWhiteSpace(LastName)) missing.Add("Last Name");
            if (string.IsNullOrWhiteSpace(Email)) missing.Add("Email");
            if (string.IsNullOrWhiteSpace(PhoneNumber)) missing.Add("Phone Number");
            if (string.IsNullOrWhiteSpace(ResidentialAddress)) missing.Add("Home Address");

            if (missing.Any())
            {
                ShowToast($"Missing: {string.Join(", ", missing)}");
                return false;
            }
            return true;
        }

        private async Task<bool> IsValidPersonalInfo()
        {
            if (!InputValidation.IsValidName(FirstName))
            {
                ShowToast("Invalid First Name");
                return false;
            }

            if (!InputValidation.IsValidName(LastName))
            {
                ShowToast("Invalid Last Name");
                return false;
            }

            if (!InputValidation.IsValidEmail(Email))
            {
                ShowToast("Invalid Email Address");
                return false;
            }

            if (!InputValidation.IsValidPhoneNumber(PhoneNumber))
            {
                ShowToast("Invalid Phone Number");
                return false;
            }

            var age = DateTime.Today.Year - DateOfBirth.Year;
            if (DateOfBirth.AddYears(age) > DateTime.Today) age--;
            if (age < 15)
            {
                ShowToast("You must be at least 15 years old.");
                return false;
            }

            return true;
        }

        private bool IsIdVerificationFilled()
        {
            var missing = new List<string>();
            if (string.IsNullOrWhiteSpace(SelectedIdType)) missing.Add("ID Type");
            if (string.IsNullOrWhiteSpace(IdNumber)) missing.Add("ID Number");

            if (missing.Any())
            {
                ShowToast($"Missing: {string.Join(", ", missing)}");
                return false;
            }
            return true;
        }
        #endregion

        #region Initialization
        private async Task GetCurrentUserDetailsAsync()
        {
            var user = await _userService.GetCurrentUserAsync();
            if (user != null)
            {
                FirstName = user.FirstName;
                LastName = user.LastName;
                Email = user.Email;
                PhoneNumber = user.PhoneNumber;

                if (user.IsFaceVerified)
                {
                    IsVerificationSuccessful = true;
                    VerificationStatus = "Already Verified";
                    VerificationStatusColor = Colors.Green;
                }
            }
        }

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();
            await GetCurrentUserDetailsAsync();
        }
        #endregion
    }
}