using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MANDUU.Models
{
    public class Offer
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string offerCode { get; set; }
        public Color BgColor { get; set; }


        public Offer(string title, string description, string offerCode, Color bgColor)
        {
            Title = title;
            Description = description;
            this.offerCode = offerCode;
            BgColor = bgColor;
        }

        public static readonly string[] _lightColors = new string[]
        {
            "#e1f1e7","#dad1f9", "#d0f200", "#e28083", "#7fbdc7","#ea978d"
        };

        private static Color RandomColor => Color.FromArgb(_lightColors.OrderBy(c => Guid.NewGuid()).First());
        public static IEnumerable<Offer> GetOffers()
        {
            yield return new Offer("Up to 30% off", "Enjoy upto 30% off on all electronic Gadgets form Grandma Store", "FRT30", RandomColor);
            yield return new Offer("Big May Sales", "Buy 1 get 50% on 4 more", "YyuO90", RandomColor);
            yield return new Offer("Print Bulk at low Price", "Print 40 pages for only 20 cedis this Month", "gryyahh09", RandomColor);
        }
    }
}
