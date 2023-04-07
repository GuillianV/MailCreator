using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataView.Entries
{
    internal static class Promos
    {
        public const string B1 = "B1";
        public const string B2 = "B2";
        public const string B3 = "B3";
        public const string B3_DIG = "B3 Dig";
        public const string B3_ECO = "B3 Eco";
        public const string B3_CN = "B3 CN";
        public const string B3_WSM = "B3WSM";
        public const string B3_DEV = "B3 Dev";
        public const string M1_EMD = "M1 EMD";
        public const string M1_DEV = "M1 Dev";
        public const string M1_UIUX = "M1UIUX";
        public const string M1_MID = "M1 MID";
        public const string M2_EMD = "M2 EMD";
        public const string M2_MID = "M2 MID";
        public const string M2_DEV = "M2 Dev";
        public const string M2_UIUX = "M2UIUX";

        private static List<String> _allPromos = new List<string>() { B1, B2, B3, B3_CN, B3_DEV, B3_DIG, B3_ECO, B3_WSM, M1_DEV, M1_EMD,M1_MID,M1_UIUX,M2_DEV,M2_EMD,M2_MID, M2_UIUX };

        public static string? MatchPromo(string text)
        {
            return _allPromos.FirstOrDefault(promo => promo == text);
        }

    }
}
