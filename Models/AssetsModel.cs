using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDeskCore.Models
{
    public class AssetsModel
    {
        public int Id { get; set; }
        public string AssetName { get; set; }
        public string Serial { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
    }
}
