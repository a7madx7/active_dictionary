using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using Active_Dictionary.Properties;
using FirstFloor.ModernUI.Windows.Controls;

namespace Active_Dictionary.Classes
{
    internal static class Updater
    {
        internal static async Task<bool> CheckForUpdateAsync()
        {
            return await Task.Run(() =>
            {
                try
                {
                    var currentMajor =
                        Assembly.GetExecutingAssembly().GetName().Version.Major.ToString(CultureInfo.InvariantCulture);
                    var currentMinor =
                        Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString(CultureInfo.InvariantCulture);

                    var currentVersion = (currentMajor + currentMinor);

                    var newVersion = GetValue("version", "updates",
                        Settings.Default.UpdateConnectionString,
                        string.Format("name = 'active dictionary'"));

                    if (int.Parse(newVersion) > int.Parse(currentVersion))
                    {
                        const MessageBoxButton btn = MessageBoxButton.YesNo;

                        var result =
                            ModernDialog.ShowMessage("هناك تحديث جديد للبرنامج متوفر هل ترغب في تحميله ؟", "تحديث جديد",
                                btn);

                        switch (result.ToString())
                        {
                            case "yes":
                                var updateLink = GetValue("updateLink", "updates",
                                    Settings.Default.UpdateConnectionString,
                                    string.Format(
                                        "name = 'active dictionary'"));
                                Process.Start(updateLink);
                                return true;

                            case "no":
                                return false;
                            default:
                                return false;
                        }
                    }
                    return false;
                }
                catch
                {
                    return false;
                }
            });
        }

        private static string GetValue(string columnName, string tableName, string conString, string condition = "")
        {
            var selectCommand = !string.IsNullOrEmpty(condition)
                ? string.Format("SELECT {0} FROM {1} WHERE {2}", columnName, tableName, condition)
                : string.Format("SELECT {0} FROM {1}", columnName, tableName);
            try
            {
                using (var conn = new SqlConnection(conString))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand(selectCommand, conn))
                    {
                        var val = cmd.ExecuteScalar().ToString();
                        return !string.IsNullOrEmpty(val) ? val : null;
                    }
                }
            }
            catch
            {
                return null;
            }
        }
    }
}