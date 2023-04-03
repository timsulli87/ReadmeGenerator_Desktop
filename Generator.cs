using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ReadmeGenerator_Desktop.Models;

namespace ReadmeGenerator_Desktop
{
    internal static class Generator
    {
                
        /// <summary>
        /// Reads the XML document at the specified filepath and transforms it into a readme.md string.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>String contents of the readme.md</returns>
        public static string CreateDoc(string filePath, bool excludeProgram, bool excludeAppConfig)
        {
            string input = File.ReadAllText(filePath);
            XDocument xdoc = XDocument.Parse(input);

            //determine application name
            string? appName = (from app in xdoc.Element("doc").Elements("assembly").Elements("name")
                               select app.Value).FirstOrDefault();

            //assign Program.cs summary as overiew of repo
            string? overview = (from ov in xdoc.Element("doc").Elements("members").Elements()
                                where ov.Attribute("name").Value.Split(".")[1] == "Program"
                                select ov.Element("summary").Value).FirstOrDefault();

            List<ClassObj> classes = new List<ClassObj>();

            //populate list of all classes
            if (excludeProgram && excludeAppConfig)     //(default) exlclude Program.cs & ApplicationConfiguration.cs
            {
                classes = (from cs in xdoc.Element("doc").Elements("members").Elements()
                           where cs.Attribute("name").Value.Substring(0, 2) == "T:"
                                //this gets just the name out of the longer XML attribute - ex. "T:ReadmeGenerator_Desktop.Program"
                                && cs.Attribute("name").Value.Split(":")[1].Split(".")[cs.Attribute("name").Value.Split(":")[1].Split(".").Length - 1] != "Program"
                                && cs.Attribute("name").Value.Split(":")[1].Split(".")[cs.Attribute("name").Value.Split(":")[1].Split(".").Length - 1] != "ApplicationConfiguration"
                           select new ClassObj
                           {
                               Name = cs.Attribute("name").Value.Split(":")[1].Split(".")[cs.Attribute("name").Value.Split(":")[1].Split(".").Length - 1],
                               Summary = cs.Element("summary").Value
                           }).ToList();
            }
            else if (excludeProgram && !excludeAppConfig)   //only exclude Program.cs
            {
                classes = (from cs in xdoc.Element("doc").Elements("members").Elements()
                           where cs.Attribute("name").Value.Substring(0, 2) == "T:"
                                && cs.Attribute("name").Value.Split(":")[1].Split(".")[cs.Attribute("name").Value.Split(":")[1].Split(".").Length - 1] != "Program"
                           select new ClassObj
                           {
                               Name = cs.Attribute("name").Value.Split(":")[1].Split(".")[cs.Attribute("name").Value.Split(":")[1].Split(".").Length - 1],
                               Summary = cs.Element("summary").Value
                           }).ToList();
            }
            else if (!excludeProgram && excludeAppConfig)   //only exclude ApplicationConfiguration.cs
            {
                classes = (from cs in xdoc.Element("doc").Elements("members").Elements()
                           where cs.Attribute("name").Value.Substring(0, 2) == "T:"
                                && cs.Attribute("name").Value.Split(":")[1].Split(".")[cs.Attribute("name").Value.Split(":")[1].Split(".").Length - 1] != "ApplicationConfiguration"
                           select new ClassObj
                           {
                               Name = cs.Attribute("name").Value.Split(":")[1].Split(".")[cs.Attribute("name").Value.Split(":")[1].Split(".").Length - 1],
                               Summary = cs.Element("summary").Value
                           }).ToList();
            }
            else    //include both Program.cs & ApplicationConfiguration.cs
            {
                classes = (from cs in xdoc.Element("doc").Elements("members").Elements()
                           where cs.Attribute("name").Value.Substring(0, 2) == "T:"
                           select new ClassObj
                           {
                               Name = cs.Attribute("name").Value.Split(":")[1].Split(".")[cs.Attribute("name").Value.Split(":")[1].Split(".").Length - 1],
                               Summary = cs.Element("summary").Value
                           }).ToList();
            }

            //iterate through class list and assign their properties, methods, and events
            foreach (ClassObj classObj in classes)
            {
                //assign properties
                classObj.Properties = (from p in xdoc?.Element("doc")?.Elements("members")?.Elements()
                                       where p?.Attribute("name").Value.Substring(0, 2) == "P:"
                                            && p?.Attribute("name")?.Value.Split(".")[p.Attribute("name").Value.Split(".").Length - 2] == classObj.Name
                                       select new PropertyObj
                                       {
                                           Name = p?.Attribute("name")?.Value.Split(".")[p.Attribute("name").Value.Split(".").Length - 1] ?? "",
                                           Summary = p?.Element("summary")?.Value ?? ""
                                       }).ToList();

                //assign methods
                classObj.Methods = (from m in xdoc?.Element("doc")?.Elements("members").Elements()
                                    where m?.Attribute("name")?.Value.Substring(0, 2) == "M:"
                                            && m?.Attribute("name")?.Value.Split("(")[0].Split(".")[m.Attribute("name").Value.Split("(")[0].Split(".").Length - 2] == classObj.Name
                                    select new MethodObj
                                    {
                                        Name = m?.Attribute("name")?.Value.Split("(")[0].Split(".")[m.Attribute("name").Value.Split("(")[0].Split(".").Length - 1] ?? "",
                                        Summary = m?.Element("summary")?.Value ?? "",
                                        Returns = m?.Element("returns")?.Value ?? "",
                                        Parameters = new List<string>()
                                    }).ToList();

                //iterate through method list and assign their parameters
                foreach (MethodObj methodObj in classObj.Methods)
                {
                    string param = (from p in xdoc?.Element("doc").Elements("members").Elements()
                                    where p?.Attribute("name")?.Value.Substring(0, 2) == "M:"
                                        && p?.Attribute("name")?.Value.Split("(")[0].Split(".")[p.Attribute("name").Value.Split("(")[0].Split(".").Length - 1] == methodObj.Name
                                    select p?.Attribute("name")?.Value).FirstOrDefault() ?? "";

                    if (param.Contains("("))    //used to identify methods with parameters
                    {
                        string[] paramTypes = param.Split(",");     //string array to handle multiple parameters
                        int paramCount = paramTypes.Count();

                        //XElement object of the current method
                        XElement xMethod = (from m in xdoc?.Element("doc").Elements("members").Elements()
                                            where m?.Attribute("name")?.Value.Substring(0, 2) == "M:"
                                                && m?.Attribute("name")?.Value.Split("(")[0].Split(".")[m.Attribute("name").Value.Split("(")[0].Split(".").Length - 1] == methodObj.Name
                                            select m).FirstOrDefault() ?? new XElement("");

                        List<XElement> paramList = xMethod.Elements("param").ToList();      //list of parameter elements of the current method

                        //iterate through parameter list and build a string representing their collection type, data type, and name
                        for (int i = 0; i < paramCount; i++)
                        {
                            if (paramTypes[i].Contains("{"))    //used to identify a collection parameter
                            {
                                string collectionType = paramTypes[i].Split("{")[0].Split(".")[paramTypes[i].Split("{")[0].Split(".").Length - 1];
                                string dataType = paramTypes[i].Split(".")[paramTypes[i].Split(".").Length - 1].Replace("}", "");
                                string paramName = paramList[i].Attribute("name")?.Value ?? "";

                                methodObj.Parameters.Add($"{collectionType}<{dataType}> {paramName}");
                            }
                            else
                            {
                                string dataType = paramTypes[i].Split(".")[paramTypes[i].Split(".").Length - 1].Replace("}", "");
                                string paramName = paramList[i].Attribute("name")?.Value ?? "";

                                methodObj.Parameters.Add($"{dataType} {paramName}");
                            }
                        }

                    }
                }
            }

            //build the markdown code
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"# {appName}");
            sb.AppendLine("");
            sb.AppendLine("<br>");
            sb.AppendLine("");
            sb.AppendLine("## **Overview**");
            sb.AppendLine("<br>");
            sb.AppendLine("");
            sb.AppendLine(overview?.Replace("\n", "").Trim() ?? "");
            sb.AppendLine("");

            foreach (ClassObj clas in classes)
            {
                sb.AppendLine("<br>");
                sb.AppendLine("");
                sb.AppendLine($"## **{clas.Name}**");
                sb.AppendLine("");
                sb.AppendLine(clas.Summary?.Replace("\n", "").Trim() ?? "");
                sb.AppendLine("");

                if (clas.Properties.Count > 0)
                {
                    sb.AppendLine("");
                    sb.AppendLine("### **Properties**");

                    foreach (PropertyObj prop in clas.Properties)
                    {
                        sb.AppendLine("");
                        sb.AppendLine(prop.Name);
                        sb.AppendLine($"<ul><li>{prop.Summary?.Replace("\n", "").Trim() ?? ""}</li></ul>");
                    }
                }

                if (clas.Methods.Count > 0)
                {
                    sb.AppendLine("");
                    sb.AppendLine("### **Functions**");

                    foreach (MethodObj method in clas.Methods)
                    {
                        sb.AppendLine("");
                        sb.AppendLine(method.Name);
                        sb.AppendLine("<ul>");
                        sb.AppendLine("<li>");
                        sb.AppendLine(method.Summary?.Replace("\n", "").Trim() ?? "");
                        sb.AppendLine("</li>");

                        if (method.Parameters.Count > 0)
                        {
                            sb.AppendLine("<li>");
                            sb.AppendLine("Parameters");
                            sb.AppendLine("<ul>");

                            foreach (string p in method.Parameters)
                            {
                                sb.AppendLine($"<li>{p}</li>");
                            }

                            sb.AppendLine("</ul>");
                            sb.AppendLine("</li>");
                        }

                        if (method.Returns != "")
                        {
                            sb.AppendLine("<li>");
                            sb.AppendLine("Returns");
                            sb.AppendLine(method.Returns);
                            sb.AppendLine("</li>");
                        }

                        sb.AppendLine("</ul>");
                    }
                }
            }

            return sb.ToString();
        }
    }
}
