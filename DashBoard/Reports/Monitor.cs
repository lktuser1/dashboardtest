using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDSIntranet.Reports
{
    public class Monitor
    {
        public int Id { get; set; }
        public string Instance { get; set; }
        public string Application { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public string Frequency { get; set; }


    }
}