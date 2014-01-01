using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Data;
using System.Xml.Linq;
using System.Windows;
using System.Xml;
namespace XRebirthSaveEditorC
{
    public class EntityValueConverter : IValueConverter
    {
        private Dictionary<string, string> FromMacroToName;
        private Dictionary<string, string> FromNameToMacro;
        public EntityValueConverter()
        {
            XNamespace xNamespace = XNamespace.Get("");
            XName name = xNamespace.GetName("Entities");
            XName name2 = xNamespace.GetName("Entity");

            FromMacroToName = new Dictionary<string, string>();
            FromNameToMacro = new Dictionary<string, string>();

            string appPath = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);


            if (File.Exists(appPath + "\\MacroTranslation.xml"))
            {
                XDocument xml = XDocument.Load(new Uri(appPath + "\\MacroTranslation.xml").ToString());

                IEnumerable<XElement> enumerator = xml.Elements(name).Descendants(name2);

                foreach (XElement node in enumerator)
                {
                    try
                    {
                        FromMacroToName.Add(node.Attribute("macro").Value, node.Attribute("name").Value);
                        FromNameToMacro.Add(node.Attribute("name").Value, node.Attribute("macro").Value);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Duplicate Key:" + node.Attribute("macro").Value, "Warning", MessageBoxButton.OK);
                    }
                }

            }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType().ToString() == "System.Xml.XmlElement")
            {
                value = ((XmlElement) value).InnerText;
            }

            if (this.FromMacroToName.ContainsKey(System.Convert.ToString(value)))
            {
                return this.FromMacroToName[System.Convert.ToString(value)];
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (this.FromNameToMacro.ContainsKey(System.Convert.ToString(value)))
            {
                return this.FromNameToMacro[System.Convert.ToString(value)];
            }
            return value;
        }
    }
}