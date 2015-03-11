using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Active_Dictionary.Properties;

namespace Active_Dictionary.Classes
{
    internal abstract class DBDataGetter : IDBDataGetter
    {
        internal static string Mechanism;

        /// <summary>
        ///     looks up the database and returns a key, value dictionary.
        /// </summary>
        /// <param name="key">the string to look up</param>
        /// <returns>updates a dictionary of type string as a key and string as a value</returns>
        internal static void GetTranslationAsync(string key)
        {
            try
            {
                GetValueAsync(key);
            }
            catch
            {
            }
        }

        /// <summary>
        ///     gets the required translation from a given dataset object in memory
        /// </summary>
        /// <param name="ds"></param>
        internal static void GetTranslationAsync(DataSet ds)
        {
        }

        /// <summary>
        ///     fired when the search is completed and results are returned from the database.
        /// </summary>
        public static event EventHandler<SearchEventArgs> SearchCompleted;

        /// <summary>
        ///     the actual implementation of the GetTranslationAsync function for more object oriented design patterns match.
        /// </summary>
        /// <param name="key">the string to look up</param>
        private static async void GetValueAsync(string key)
        {
            var results = new Dictionary<string, string>();


            await Task.Run(() =>
            {
                Thread.CurrentThread.Priority = ThreadPriority.Highest;
                Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.RealTime;

                string searchCmd;
                switch (Mechanism)
                {
                    //todo: GET ARABIC AND ENGLISH SEPARATELY
                    // search for english if he writes in english otherwise search in arabic.
                    case "after":
                        searchCmd =
                            string.Format(
                                Regex.IsMatch(key, "^[a-zA-Z0-9]*$")
                                    ? "SELECT [A], [E] FROM [en-ar] WHERE [E] LIKE '{0}%'"
                                    : "SELECT [A], [E] FROM [en-ar] WHERE [A] LIKE '{0}%'", key);

                        break;

                    case "before":
                        searchCmd =
                            string.Format(
                                Regex.IsMatch(key, "^[a-zA-Z0-9]*$")
                                    ? "SELECT [A], [E] FROM [en-ar] WHERE [E] LIKE '%{0}'"
                                    : "SELECT [A], [E] FROM [en-ar] WHERE [A] LIKE '%{0}'", key);

                        break;

                    case "any":
                        searchCmd =
                            string.Format(
                                Regex.IsMatch(key, "^[a-zA-Z0-9]*$")
                                    ? "SELECT [A], [E] FROM [en-ar] WHERE [E] LIKE '%{0}%'"
                                    : "SELECT [A], [E] FROM [en-ar] WHERE [A] LIKE '%{0}%'", key);

                        break;

                    default:
                        searchCmd =
                            string.Format(
                                Regex.IsMatch(key, "^[a-zA-Z0-9]*$")
                                    ? "SELECT [A], [E] FROM [en-ar] WHERE [E] LIKE '{0}%'"
                                    : "SELECT [A], [E] FROM [en-ar] WHERE [A] LIKE '{0}%'", key);
                        break;
                }
                try
                {
                    using (var conn = new OleDbConnection(Settings.Default.ConnectionString))
                    {
                        using (var cmd = new OleDbCommand(searchCmd, conn))
                        {
                            using (var adapter = new OleDbDataAdapter(cmd))
                            {
                                using (var dataSet = new DataSet("Active Dictionary"))
                                {
                                    if (results.Count > 0)
                                    {
                                        dataSet.Clear();
                                        results.Clear();
                                    }
                                    adapter.Fill(dataSet, "en-ar");

                                    foreach (DataRow result in dataSet.Tables[0].Rows)
                                    {
                                        try
                                        {
                                            if (!results.ContainsKey(result["E"].ToString()) &&
                                                !results.ContainsKey(result["A"].ToString()))
                                            {
                                                results.Add(result["E"].ToString(),
                                                    result["A"].ToString());
                                            }
                                        }
                                        catch
                                        {
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch
                {
                }
            });
            // i didn't count on the return type of this function, an event firing would be much better and give more accurate results
            // because of some asyncronous issues.


            var searchArgs = new SearchEventArgs {KeyLength = key.Length, Results = results};
            SearchCompleted(null, searchArgs);
        }
    }

    internal class SearchEventArgs : EventArgs
    {
        public int KeyLength;
        public Dictionary<string, string> Results;
    }
}