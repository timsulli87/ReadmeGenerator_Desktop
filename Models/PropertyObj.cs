using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadmeGenerator_Desktop.Models
{
    /// <summary>
    /// Class the represents the property elements of the XML document file.
    /// </summary>
    internal class PropertyObj
    {
        /// <summary>
        /// Property name
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Property summary
        /// </summary>
        public string? Summary { get; set; }
    }
}
