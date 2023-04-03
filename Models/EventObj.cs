using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadmeGenerator_Desktop.Models
{
    /// <summary>
    /// Class the represents the event elements of the XML document file.
    /// </summary>
    internal class EventObj
    {
        /// <summary>
        /// Event name
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Event summary
        /// </summary>
        public string? Summary { get; set; }
    }
}
