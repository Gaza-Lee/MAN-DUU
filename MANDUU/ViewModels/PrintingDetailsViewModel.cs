using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MANDUU.Models;
using MANDUU.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MANDUU.ViewModels
{
    public partial class PrintingDetailsViewModel : ObservableObject, IQueryAttributable
    {

        private readonly PrintingStationService _printingStationService;

        [ObservableProperty]
        private int _numberOfPages;

        [ObservableProperty]
        private int _numberOfCopies;

        [ObservableProperty]
        private decimal _estimatedPrice;

        [ObservableProperty]
        private string _selectedPrintType;

        [ObservableProperty]
        private string _selectedPageSetting;  

        [ObservableProperty]
        private string _selectedPaperSize;

        [ObservableProperty]
        private PrintingStation _selectedPrintingStation;

        [ObservableProperty]
        private string _selectedDocument;

        [ObservableProperty]
        private ObservableCollection<string> _printTypes = new() { "Black and White", "Coloured" };

        [ObservableProperty]
        private ObservableCollection<string> _pageSettings = new() { "Front Only", "Front and Back" };

        [ObservableProperty]
        private ObservableCollection<string> _paperSizes = new() { "A4", "A3", "Letter", "Legal" };

        [ObservableProperty]
        private ObservableCollection<PrintingStation> _printStations;

        public PrintingDetailsViewModel(PrintingStationService printingStationService)
        {
            _printingStationService = printingStationService;
            PrintStations = new ObservableCollection<PrintingStation>(_printingStationService.GetAllStations());
            SelectedPrintType = PrintTypes.First();
            SelectedPageSetting = PageSettings.First();
            SelectedPaperSize = PaperSizes.First();
            NumberOfCopies = 1;
            NumberOfPages = 1;
        }



        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("SelectedStation"))
            {
                SelectedPrintingStation = query["SelectedStation"] as PrintingStation;
            }
            if (query.ContainsKey("SelectedDocument"))
            {
                SelectedDocument = query["SelectedDocument"] as string;
            }
        }



        [RelayCommand]
        private void RemoveDocumentAsync()
        {
            SelectedDocument = string.Empty;
        }

        [RelayCommand]
        private async Task SelectDocumentAsync()
        {
            try
            {
                var customFileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.iOS, new[] { "com.adobe.pdf", "org.openxmlformats.wordprocessingml.document", "public.text" } }, // iOS UTTypes
                    { DevicePlatform.Android, new[] { "application/pdf", "application/msword", "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "text/plain" } }, // MIME types
                    { DevicePlatform.WinUI, new[] { ".pdf", ".doc", ".docx", ".txt" } }, // Extensions
                    { DevicePlatform.MacCatalyst, new[] { "com.adobe.pdf", "org.openxmlformats.wordprocessingml.document", "public.text" } }
                });

                var result = await FilePicker.Default.PickAsync(new PickOptions
                {
                    PickerTitle = "Select a document",
                    FileTypes = customFileTypes
                });

                if (result != null)
                {
                    SelectedDocument = result.FullPath ?? result.FileName;
                }
            }
            catch (Exception)
            {
                // User cancelled or error occurred – safe to ignore or log
                SelectedDocument = string.Empty;
            }
        }

        partial void OnNumberOfPagesChanged(int value) => CalculatePrice();
        partial void OnNumberOfCopiesChanged(int value) => CalculatePrice();
        partial void OnSelectedPrintTypeChanged(string value) => CalculatePrice();
        partial void OnSelectedPaperSizeChanged(string value) => CalculatePrice();
        partial void OnSelectedPageSettingChanged(string value) => CalculatePrice();
        public bool HasSelectedDocument => !string.IsNullOrEmpty(SelectedDocument);
        public string EstimatedPriceDisplay => $"₵{EstimatedPrice:F2}";

        partial void OnSelectedDocumentChanged(string value)
        {
            OnPropertyChanged(nameof(HasSelectedDocument));
        }

        private void CalculatePrice()
        {
            if (NumberOfCopies <= 0 || NumberOfPages <= 0)
            {
                EstimatedPrice = 0;
                return;
            }

            // how many sheets based on page setting
            int sheets = SelectedPageSetting == "Front and Back"
                ? (int)System.Math.Ceiling(NumberOfPages / 2.0)
                : NumberOfPages;

            // price per sheet depends on combination
            decimal pricePerSheet = GetPricePerSheet();

            // final calculation
            EstimatedPrice = sheets * NumberOfCopies * pricePerSheet;
        }

        private decimal GetPricePerSheet()
        {
            switch (SelectedPaperSize)
            {
                case "A4":
                    if (SelectedPrintType == "Black and White" && SelectedPageSetting == "Front Only") return 0.5m;
                    if (SelectedPrintType == "Black and White" && SelectedPageSetting == "Front and Back") return 1.0m;
                    if (SelectedPrintType == "Coloured" && SelectedPageSetting == "Front Only") return 2.0m;
                    if (SelectedPrintType == "Coloured" && SelectedPageSetting == "Front and Back") return 4.0m;
                    break;

                case "A3":
                    if (SelectedPrintType == "Black and White" && SelectedPageSetting == "Front Only") return 0.8m;
                    if (SelectedPrintType == "Black and White" && SelectedPageSetting == "Front and Back") return 1.6m;
                    if (SelectedPrintType == "Coloured" && SelectedPageSetting == "Front Only") return 8.0m;
                    if (SelectedPrintType == "Coloured" && SelectedPageSetting == "Front and Back") return 16.0m;
                    break;

                case "Letter":
                    if (SelectedPrintType == "Black and White" && SelectedPageSetting == "Front Only") return 0.6m;
                    if (SelectedPrintType == "Black and White" && SelectedPageSetting == "Front and Back") return 1.2m;
                    if (SelectedPrintType == "Coloured" && SelectedPageSetting == "Front Only") return 6.0m;
                    if (SelectedPrintType == "Coloured" && SelectedPageSetting == "Front and Back") return 12.0m;
                    break;

                case "Legal":
                    if (SelectedPrintType == "Black and White" && SelectedPageSetting == "Front Only") return 0.7m;
                    if (SelectedPrintType == "Black and White" && SelectedPageSetting == "Front and Back") return 1.4m;
                    if (SelectedPrintType == "Coloured" && SelectedPageSetting == "Front Only") return 7.0m;
                    if (SelectedPrintType == "Coloured" && SelectedPageSetting == "Front and Back") return 14.0m;
                    break;
            }

            return 0.5m;
        }
        partial void OnEstimatedPriceChanged(decimal value)
        {
            OnPropertyChanged(nameof(EstimatedPriceDisplay));
        }
    }
}
