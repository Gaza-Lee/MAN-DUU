using MANDUU.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MANDUU.Services
{
    public class PrintingStationService
    {
        private readonly List<PrintingStation> _printingStations;

        public PrintingStationService()
        {
            _printingStations = new List<PrintingStation>
            {
                new PrintingStation
                {
                    Id = 1,
                    Name = "Main Campus Printing Hub",
                    ShortLocation = "Building A",
                    LongLocation = "Main Campus, Building A, Floor 1, Room 101",
                    PhoneNumber = "+1-555-0101",
                    Status = PrintingStationStatus.Active
                },
                new PrintingStation
                {
                    Id = 2,
                    Name = "Library Print Center",
                    ShortLocation = "Library",
                    LongLocation = "Central Library, Ground Floor, Near Reference Section",
                    PhoneNumber = "+1-555-0102",
                    Status = PrintingStationStatus.Active
                },
                new PrintingStation
                {
                    Id = 3,
                    Name = "Science Wing Printer",
                    ShortLocation = "Science Bldg",
                    LongLocation = "Science Building, Floor 2, Room 205, Chemistry Department",
                    PhoneNumber = "+1-555-0103",
                    Status = PrintingStationStatus.Inactive
                },
                new PrintingStation
                {
                    Id = 4,
                    Name = "Student Union Station",
                    ShortLocation = "Student Union",
                    LongLocation = "Student Union Building, Food Court Area",
                    PhoneNumber = "+1-555-0104",
                    Status = PrintingStationStatus.Active
                },
                new PrintingStation
                {
                    Id = 5,
                    Name = "Engineering Lab Printer",
                    ShortLocation = "Eng Building",
                    LongLocation = "Engineering Building, Computer Lab 3B",
                    PhoneNumber = "+1-555-0105",
                    Status = PrintingStationStatus.Active
                },
                new PrintingStation
                {
                    Id = 6,
                    Name = "Business School Kiosk",
                    ShortLocation = "Business Sch",
                    LongLocation = "Business School, Floor 1, Student Lounge",
                    PhoneNumber = "+1-555-0106",
                    Status = PrintingStationStatus.Active
                },
                new PrintingStation
                {
                    Id = 7,
                    Name = "Dormitory Commons Printer",
                    ShortLocation = "North Dorms",
                    LongLocation = "North Dormitory Complex, Commons Area",
                    PhoneNumber = "+1-555-0107",
                    Status = PrintingStationStatus.Inactive
                },
                new PrintingStation
                {
                    Id = 8,
                    Name = "Administration Office Printer",
                    ShortLocation = "Admin Bldg",
                    LongLocation = "Administration Building, Floor 2, Reception Area",
                    PhoneNumber = "+1-555-0108",
                    Status = PrintingStationStatus.Active
                },
                new PrintingStation
                {
                    Id = 9,
                    Name = "Arts Department Station",
                    ShortLocation = "Arts Building",
                    LongLocation = "Arts Building, Floor 3, Studio Wing",
                    PhoneNumber = "+1-555-0109",
                    Status = PrintingStationStatus.Active
                },
                new PrintingStation
                {
                    Id = 10,
                    Name = "Sports Complex Printer",
                    ShortLocation = "Gym",
                    LongLocation = "Sports Complex, Main Entrance, Near Ticket Booth",
                    PhoneNumber = "+1-555-0110",
                    Status = PrintingStationStatus.Active
                }
            };
        }

        public List<PrintingStation> GetAllStations() => _printingStations;

        public PrintingStation GetStationById(int id) =>
            _printingStations.FirstOrDefault(s => s.Id == id);

        public List<PrintingStation> GetActiveStations() =>
            _printingStations.Where(s => s.Status == PrintingStationStatus.Active).ToList();

        public List<PrintingStation> GetStationsByLocation(string location) =>
            _printingStations.Where(s =>
                s.ShortLocation.Contains(location, System.StringComparison.OrdinalIgnoreCase) ||
                s.LongLocation.Contains(location, System.StringComparison.OrdinalIgnoreCase))
            .ToList();

        public PrintingStation UpdateStationStatus(int id, PrintingStationStatus status)
        {
            var station = GetStationById(id);
            if (station != null)
            {
                station.Status = status;
            }
            return station;
        }

        public List<PrintingStation> SearchStations(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return _printingStations;

            return _printingStations.Where(s =>
                s.Name.Contains(searchTerm, System.StringComparison.OrdinalIgnoreCase) ||
                s.ShortLocation.Contains(searchTerm, System.StringComparison.OrdinalIgnoreCase) ||
                s.LongLocation.Contains(searchTerm, System.StringComparison.OrdinalIgnoreCase) ||
                s.PhoneNumber.Contains(searchTerm, System.StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }
}
