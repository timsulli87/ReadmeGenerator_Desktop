using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadmeGenerator_Desktop.Models
{
    internal class MethodObj
    {
        public string? Name { get; set; }
        public string? Summary { get; set; }
        public List<string>? Parameters { get; set; }
        public string? Returns { get; set; }
    }
}
