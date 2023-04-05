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

        public static string MatchMailtypeText(this String str)
        {

            Regex regex = new Regex("^[a-zA-Z0-9-_@.éàèôç]*$", RegexOptions.IgnoreCase);
            if (regex.IsMatch(str))
            {
                return str;
            }

            return "";

        }


        public static string MatchJsonFilename(this String str)
        {

            Regex regex = new Regex("^[a-zA-Z0-9]+\\.json$", RegexOptions.IgnoreCase);
            if(!regex.IsMatch(str))
                throw new ArgumentException("string given does not match json filename");
            return str;
        }



    }
}
