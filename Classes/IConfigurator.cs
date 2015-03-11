using System.Collections.Generic;

namespace Active_Dictionary.Classes
{
    public interface IConfigurator
    {
        /// <summary>
        ///     Get all entries with the matching name.
        /// </summary>
        /// <param name="name">Entry name to search for.</param>
        /// <returns>List of entries with the matching name.</returns>
        List<ConfigEntry> GetEntriesByName(string name);

        /// <summary>
        ///     Get all entries.
        /// </summary>
        /// <returns>List of all entries.</returns>
        List<ConfigEntry> GetAllEntries();

        /// <summary>
        ///     Add a new entry to the configuration.
        /// </summary>
        /// <param name="entry">The new entry to be added to the configuration.</param>
        void AddEntry(ConfigEntry entry);

        /// <summary>
        ///     Remove an entry from the configuration.
        /// </summary>
        /// <param name="entry">The entry to be removed from the configuration.</param>
        void RemoveEntry(ConfigEntry entry);

        /// <summary>
        ///     Export configuration to XML.
        /// </summary>
        /// <returns>Configuration in XML format.</returns>
        string ExportXml();

        /// <summary>
        ///     Save configuration to a file.
        /// </summary>
        /// <param name="fileName">File name to save to.</param>
        void Save(string fileName);
    }
}