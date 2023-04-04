using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Utils
{
    public static class StringExtensions
    {

        public static string CleanText(this String str)
        {

            Regex regex = new Regex("^[a-zA-Z0-9-_@.]*$", RegexOptions.IgnoreCase);
            if (regex.IsMatch(str))
            {
                return str;
            }

            return "";

        }

    }
}
