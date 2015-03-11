using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Xml;

namespace Active_Dictionary.Classes
{
    public class Configurator : IConfigurator
    {
        private static string _settingsFile;
        private readonly List<ConfigEntry> _entries;

        /// <summary>
        ///     Default constructor.
        /// </summary>
        public Configurator()
        {
            _entries = new List<ConfigEntry>();
        }

        /// <summary>
        ///     Constructor to read configuration from a configuration file.
        /// </summary>
        /// <param name="configFilePath">Configuration file's path.</param>
        public Configurator(string configFilePath)
        {
            _settingsFile = configFilePath;
            if (!File.Exists(configFilePath))
            {
                MainWindow.IsFirstTimeUse = true;
                return;
            }
            _entries = new List<ConfigEntry>();

            try
            {
                // open configuration file and read its contents
                TextReader reader = new StreamReader(configFilePath);
                var configurations = reader.ReadToEnd();
                reader.Close();

                // import the configurations
                ImportConfigurations(configurations);
            }
            catch
            {
            }
        }

        /// <summary>
        ///     Get all entries with the matching name.
        /// </summary>
        /// <param name="name">Entry name to search for.</param>
        /// <returns>List of entries with the matching name.</returns>
        public List<ConfigEntry> GetEntriesByName(string name)
        {
            if (!File.Exists(_settingsFile)) return null;
            var entries = new List<ConfigEntry>();

            // find entries with matching name
            foreach (
                var entry in
                    _entries.Where(entry => String.Equals(entry.Name, name, StringComparison.CurrentCultureIgnoreCase)))
            {
                entries.Add(entry);
                break;
            }

            return entries;
        }

        /// <summary>
        ///     Get all entries.
        /// </summary>
        /// <returns>List of all entries.</returns>
        public List<ConfigEntry> GetAllEntries()
        {
            return _entries;
        }

        /// <summary>
        ///     Add a new entry to the configuration.
        /// </summary>
        /// <param name="entry">The new entry to be added to the configuration.</param>
        public void AddEntry(ConfigEntry entry)
        {
            _entries.Add(entry);
        }

        /// <summary>
        ///     Remove an entry from the configuration.
        /// </summary>
        /// <param name="entry">The entry to be removed from the configuration.</param>
        public void RemoveEntry(ConfigEntry entry)
        {
            _entries.Remove(entry);
        }

        /// <summary>
        ///     Export configuration to XML.
        /// </summary>
        /// <returns>Configuration in XML format.</returns>
        public string ExportXml()
        {
            // return the XML document
            return GetXmlDocument().OuterXml;
        }

        /// <summary>
        ///     Save configuration to a file.
        /// </summary>
        /// <param name="fileName">File name to save to.</param>
        public void Save(string fileName)
        {
            try
            {
                var doc = GetXmlDocument();
                var writer = new XmlTextWriter(fileName, null) {Formatting = Formatting.Indented};
                doc.WriteTo(writer);
                writer.Close();
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
        }

        /// <summary>
        ///     Import XML configuration.
        /// </summary>
        /// <param name="xmlConfigurations">Configurations in XML format.</param>
        private void ImportConfigurations(string xmlConfigurations)
        {
            // create an XML document object
            var doc = new XmlDocument {InnerXml = xmlConfigurations};

            // get the configuration
            var nodes = doc.GetElementsByTagName("configuration");

            // if no configuration node was found, then there's nothing we can do
            if ((nodes.Count == 0))
            {
                return;
            }

            // there should only be one "configuration" node, so we only read the first one
            var config = nodes[0];

            // get all child nodes in the configuration
            var children = config.ChildNodes;

            // process each child node
            foreach (XmlNode child in children)
            {
                // create an H2ConfigEntry for this node
                var entry = new ConfigEntry {Name = child.Name, Value = child.InnerText};

                // get attributes
                var attributes = child.Attributes;
                if (attributes != null)
                    foreach (XmlAttribute attribute in attributes)
                    {
                        // add attribute to the entry
                        entry.AddAttribute(attribute.Name, attribute.Value);
                    }

                // add node to entries list
                _entries.Add(entry);
            }
        }

        private XmlDocument GetXmlDocument()
        {
            // create an XML doc and the configuration node
            var doc = new XmlDocument();
            var declaration = doc.CreateXmlDeclaration("1.0", "utf-8", null);
            doc.AppendChild(declaration);

            // create configuration root
            var root = doc.CreateElement("configuration");
            doc.AppendChild(root);

            // go through each entry
            foreach (var entry in _entries)
            {
                var element = doc.CreateElement(entry.Name);
                if (!string.IsNullOrEmpty(entry.Value))
                {
                    element.InnerText = entry.Value;
                }

                // add attributes
                var attributes = entry.GetAllAttributes();
                foreach (var attribute in attributes)
                {
                    element.SetAttribute(attribute.Name, attribute.Value);
                }

                root.AppendChild(element);
            }

            return doc;
        }
    }

    /// <summary>
    ///     Contains the information of a configuration entry. For example,
    ///     <employee fname="John" />
    ///     is an entry in the configuration.
    /// </summary>
    public class ConfigEntry
    {
        // variables
        private readonly List<ConfigAttribute> _attributes;

        /// <summary>
        ///     Constructor
        /// </summary>
        public ConfigEntry()
        {
            _attributes = new List<ConfigAttribute>();
        }

        // properties
        public string Name { get; set; }
        public string Value { get; set; }
        // methods

        /// <summary>
        ///     Find and retrieve the attribute's value using its name.
        /// </summary>
        /// <param name="name">The attribute's name to find.</param>
        /// <returns>The value associated with the attribute name.</returns>
        private ConfigAttribute GetAttributeByName(string name)
        {
            // search attributes list
            return
                _attributes.FirstOrDefault(
                    attribute => String.Equals(attribute.Name, name, StringComparison.CurrentCultureIgnoreCase));

            // no match found
        }

        /// <summary>
        ///     Get all attributes.
        /// </summary>
        /// <returns>List of all attributes</returns>
        public IEnumerable<ConfigAttribute> GetAllAttributes()
        {
            return _attributes;
        }

        /// <summary>
        ///     Add an attribute to the current configuration entry.
        /// </summary>
        /// <param name="name">Attribute name.</param>
        /// <param name="value">Attribute value.</param>
        public void AddAttribute(string name, string value)
        {
            // create a new attribute and add to the attribute list
            var attribute = new ConfigAttribute {Name = name, Value = value};
            _attributes.Add(attribute);
        }

        /// <summary>
        ///     Add an attribute to the entry.
        /// </summary>
        /// <param name="attribute">Attribute to be added.</param>
        public void AddAttribute(ConfigAttribute attribute)
        {
            _attributes.Add(attribute);
        }

        /// <summary>
        ///     Remove the attribute from the entry.
        /// </summary>
        /// <param name="attribute">Attribute to be removed.</param>
        public void RemoveAttribute(ConfigAttribute attribute)
        {
            _attributes.Remove(attribute);
        }

        /// <summary>
        ///     Remove the attribute with the matching name.
        /// </summary>
        /// <param name="attributeName">Name of the attribute to be removed.</param>
        public void RemoveAttribute(string attributeName)
        {
            // find the attribute and remove it
            var attribute = GetAttributeByName(attributeName);
            if (attribute != null)
            {
                _attributes.Remove(attribute);
            }
        }
    }

    /// <summary>
    ///     Entry attribute in configuration. For example, in the entry <employee fname="John" />
    ///     fname="John" is an attribute where "fname" is the name of the attribute and "John" is
    ///     the value of the attribute.
    /// </summary>
    public class ConfigAttribute
    {
        /// <summary>
        ///     Name of the attribute.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Value of the attribute.
        /// </summary>
        public string Value { get; set; }
    }
}