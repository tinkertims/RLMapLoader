using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RLMapLoader
{
    public class MapPackageJson
    {
        [JsonProperty("Map")]
        public MapPackage mapPackage { get; set; }
    }


    public class MapPackage
    {
        [DisplayName(@"Preview")]
        public string PreviewUrl { get; set; }
        [DisplayName(@"Map Name")]
        public string Name { get; set; }
        [DisplayName(@"Created By")]
        public string CreatedBy { get; set; }
        public string Version { get; set; }
        [DisplayName(@"Date Added")]
        public DateTime DateAdded { get; set; }
        [DisplayName(@"Download Link")]
        public string FileUrl { get; set; }


    }

    
}
