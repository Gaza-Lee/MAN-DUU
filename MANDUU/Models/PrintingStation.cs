using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MANDUU.Models
{
    public class PrintingStation
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ShortLocation { get; set; } = string.Empty;
        public string LongLocation { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public PrintingStationStatus Status { get; set; }
    }
    public enum PrintingStationStatus
    {
        Active,
        Inactive
    }
}
