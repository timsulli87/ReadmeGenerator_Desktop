using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadmeGenerator_Desktop.Models
{
    /// <summary>
    /// Class the represents the method elements of the XML document file.
    /// </summary>
    internal class MethodObj
    {
        /// <summary>
        /// Method name
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Method summary
        /// </summary>
        public string? Summary { get; set; }
        /// <summary>
        /// String list of method parameters
        /// </summary>
        public List<string>? Parameters { get; set; }
        /// <summary>
        /// Method return type
        /// </summary>
        public string? Returns { get; set; }
    }
}
