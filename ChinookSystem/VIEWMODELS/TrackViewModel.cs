using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinookSystem.VIEWMODELS
{
    public class TrackViewModel
    {
        public string Name { get; set; }
        public string Composer { get; set; }
        public int Milliseconds { get; set; }
        public int? Bytes { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
