using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KKoKiri
{
    public class sio2_2nm_on_si_dat
    {
        public string wavelength { get; set; }
        public string AOI { get; set; }
        public string Alpha { get; set; }
        public string Beta { get; set; }
    }

    public class SiData
    {
        public string NM { get; set; }
        public string N { get; set; }
        public string K { get; set; }
    }

    public class NewData
    {
        public double NEWnm { get; set; }
        public double NEWN { get; set; }
        public double NEWK { get; set; }
    }
}

