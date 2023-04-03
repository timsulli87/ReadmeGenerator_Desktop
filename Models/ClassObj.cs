using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadmeGenerator_Desktop.Models
{
    /// <summary>
    /// Class the represents the class elements of the XML document file.
    /// The Methods, Properties, and Events class properties are lists of class objects of their corresponding elements.
    /// </summary>
    internal class ClassObj
    {
        /// <summary>
        /// Class name
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Class summary
        /// </summary>
        public string? Summary { get; set; }
        /// <summary>
        /// List of methods contained in the class
        /// </summary>
        public List<MethodObj>? Methods { get; set; }
        /// <summary>
        /// List of properties contained in the class
        /// </summary>
        public List<PropertyObj>? Properties { get; set; }
        /// <summary>
        /// List of events contained in the class
        /// </summary>
        public List<EventObj>? Events { get; set; }

        //public ClassObj()
        //{
        //    Name = "";
        //    Summary = "";
        //    Methods = new List<MethodObj>();
        //    Properties = new List<PropertyObj>();
        //    Events = new List<EventObj>();
        //}
    }
}
