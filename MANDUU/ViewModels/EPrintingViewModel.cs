using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MANDUU.Models;
using MANDUU.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MANDUU.ViewModels
{
    public partial class EPrintingViewModel : ObservableObject
    {
        private readonly PrintingStationService _printingStationService;
        private readonly INavigationService _navigationService;

        [ObservableProperty]
        private ObservableCollection<PrintingStation> _allStations = new();

        [ObservableProperty]
        private ObservableCollection<PrintingStation> _displayStations = new();

        [ObservableProperty]
        private bool _isActive;

        [ObservableProperty]
        private string _selectedDocument;

        [ObservableProperty]
        private PrintingStation _selectedStation;

        [ObservableProperty]
        private string _searchText;

        [ObservableProperty]
        private bool _isDocumentSelected;

        [ObservableProperty]
        private bool _isStationSelected;

        public EPrintingViewModel(PrintingStationService printingStationService, INavigationService navigationService)
        {
            _printingStationService = printingStationService;
            _navigationService = navigationService;
            LoadStations();
            PropertyChanged += OnPropertyChanged;
        }

        [RelayCommand]
        private async Task SelectDocumentAsync()
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
                    IsDocumentSelected = true;


                    if (IsStationSelected && IsDocumentSelected)
                    {
                        await NavigateToPrintingDetailsAsync();
                    }
                }
            }
            catch (Exception)
            {
                SelectedDocument = string.Empty;
                IsDocumentSelected = false;
            }
        }

        [RelayCommand]
        private async Task SelectStationAsync(PrintingStation station)
        {
            // Toggle selection - if same station is clicked again, deselect it
            if (SelectedStation?.Id == station?.Id)
            {
                SelectedStation = null;
                IsStationSelected = false;
            }
            else
            {
                SelectedStation = station;
                IsStationSelected = station != null;
            }

            // Auto-navigate if both document and station are selected
            if (IsStationSelected && IsDocumentSelected)
            {
                await NavigateToPrintingDetailsAsync();
            }
        }

        [RelayCommand]
        private async Task NavigateToPrintingDetailsAsync()
        {
            if (SelectedStation == null || string.IsNullOrEmpty(SelectedDocument))
            {
                // Show error message or validation
                return;
            }

            // Navigate to printing details page using navigation service
            await _navigationService.NavigateToAsync("printingdetailspage",
                new Dictionary<string, object>
                {
                    { "SelectedStation", SelectedStation },
                    { "SelectedDocument", SelectedDocument }
                });
        }

        [RelayCommand]
        private void FilterStations()
        {
            UpdateDisplayStations();
        }

        [RelayCommand]
        private void ClearSearch()
        {
            SearchText = string.Empty;
            UpdateDisplayStations();
        }

        [RelayCommand]
        private async Task ProfileAsync()
        {
            // Navigate to profile page using navigation service
            await _navigationService.NavigateToAsync("userprofilepage");
        }

        private void LoadStations()
        {
            var stations = _printingStationService.GetAllStations();
            AllStations = new ObservableCollection<PrintingStation>(stations);
            UpdateDisplayStations();
        }

        private void UpdateDisplayStations()
        {
            IEnumerable<PrintingStation> filteredStations = AllStations;

            if (IsActive)
            {
                filteredStations = filteredStations.Where(s => s.Status == PrintingStationStatus.Active);
            }

            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                filteredStations = filteredStations.Where(s =>
                    s.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                    s.ShortLocation.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                    s.LongLocation.Contains(SearchText, StringComparison.OrdinalIgnoreCase));
            }

            var sortedStations = filteredStations
                .OrderByDescending(s => s.Status)
                .ThenBy(s => s.Name)
                .ToList();

            DisplayStations = new ObservableCollection<PrintingStation>(sortedStations);
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IsActive) || e.PropertyName == nameof(SearchText))
            {
                UpdateDisplayStations();
            }
            else if (e.PropertyName == nameof(SelectedStation))
            {
                IsStationSelected = SelectedStation != null;
            }
            else if (e.PropertyName == nameof(SelectedDocument))
            {
                IsDocumentSelected = !string.IsNullOrEmpty(SelectedDocument);
            }
        }
    }
}