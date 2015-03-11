using System;
using System.Text.RegularExpressions;

namespace Active_Dictionary.Classes
{
    internal static class Inteligence
    {
        /// <summary>
        ///     Checks to see wether this is a url string or not...
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        internal static bool IsTheTextUrl(String txt)
        {
            var reg =
                new Regex(
                    @"^http(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&amp;%\$#_]*)?$");

            while (reg.Match(txt).Success)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        ///     checks to see if the text has more than one word in it.
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        internal static bool IsTheTextLong(string txt)
        {
            if (txt.Contains(" ")) return true;

            return false;
        }

        /// <summary>
        ///     checks to see if the text is in english or any latin alphabet or not.
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        internal static bool IsTheTextLatin(string txt)
        {
            return Regex.IsMatch(txt, "^[a-zA-Z0-9]*$");
        }
    }
}