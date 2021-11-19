using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoredProc.Models
{
    public class Car
    {
        public int id { get; set; }
        public int model_year { get; set; }
        public string model { get; set; }
        public string manufacturer  { get; set; }
        public string VIN { get; set; }
    }
}
