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
        public string Name { get; set; }
        public string ShortLocation { get; set; }
        public string LongLocation { get; set; }
        public string PhoneNumber { get; set; }
        public PrintingStationStatus Status { get; set; }
    }
    public enum PrintingStationStatus
    {
        Active,
        Inactive
    }
}
