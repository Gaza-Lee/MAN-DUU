using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace MANDUU.ViewModels
{
    public partial class EPrintingViewModel : ObservableObject
    {
        [ObservableProperty]
        private string selectedDocument;

        [ObservableProperty]
        private int numberOfPages;

        [ObservableProperty]
        private int numberOfCopies;

        [ObservableProperty]
        private string selectedPrintType;

        [ObservableProperty]
        private string selectedPaperSize;

        [ObservableProperty]
        private decimal estimatedPrice;

        [ObservableProperty]
        private string selectedPageSetting;

        public ObservableCollection<string> PrintTypes { get; }
        public ObservableCollection<string> PaperSizes { get; }
        public ObservableCollection<string> PageSettings { get; }

        public EPrintingViewModel()
        {
            PrintTypes = new ObservableCollection<string>
            {
                "Coloured",
                "Black and White"
            };

            PaperSizes = new ObservableCollection<string>
            {
                "A4",
                "A3",
                "Letter",
                "Legal"
            };

            PageSettings = new ObservableCollection<string>
            {
                "Front Only",
                "Front and Back"
            };

            // Defaults
            SelectedPrintType = PrintTypes[0];
            SelectedPaperSize = PaperSizes[0];
            SelectedPageSetting = PageSettings[0];
            NumberOfPages = 1;
            NumberOfCopies = 1;

            CalculatePrice();
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
                    // Use FullPath on Windows/Mac, FileName otherwise
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
    }
}
