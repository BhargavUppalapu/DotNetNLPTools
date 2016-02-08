using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Com.Research.ML
{
    public class TokenClass
    {
        public static Regex NumberRegex = new Regex(
            "^(CAD|C|US|EUR|HK)?[#$()+\\-,./\\\\]*\\d+([#$()+\\-,./\\\\]+\\d+)*[#$()+\\-,./\\\\XY]*$",
            RegexOptions.IgnoreCase);
        public static Regex DateRegex = new Regex(
            "^("
                + "\\d{1,2}/\\d{1,2}/\\d{4}"
                + "|(\\dQ|Q\\d)?[F|C]Y\\d+[EA]?"
                + "|\\dQ(\\d+[EA]?)?"
                + "|(\\d+[\\-/])?(JAN(UARY)?|FEB(RUARY)?|MAR(CH)?|APR(IL)?|MAY|JUN(E)?|JUL(Y)?|AUG(UST)?|SEP(TEMBER)?|NOV(EMBER)?|DEC(EMBER)?)([\\-/]\\d+)?"
            + ")$",
            RegexOptions.IgnoreCase);

        public static Regex URLRegex = new Regex("^(https?://|www\\.)|\\.(com|org|net|edu|gov)$", RegexOptions.IgnoreCase);
        public static Regex EmailRegex = new Regex("^[A-Z0-9._%+-]+@[A-Z0-9.-]+\\.[A-Z]+$", RegexOptions.IgnoreCase);
        public static Regex AlphaNumericRegex = new Regex("^[A-Z]*\\d+([A-Z]+\\d+)*[A-Z]*$", RegexOptions.IgnoreCase);
        public static Regex DelimiterRegex = new Regex("^[^A-Z\\d]+$", RegexOptions.IgnoreCase);
        public static Regex NormalizeRegex = new Regex("[^A-Z]+", RegexOptions.IgnoreCase);


        public static string NormalizeToken(string token)
        {
            if (string.IsNullOrEmpty(token))
                return token;
            else if (NumberRegex.Match(token).Success)
                return "__number__";
            else if (DateRegex.Match(token).Success)
                return "__date__";
            else if (AlphaNumericRegex.Match(token).Success)
                return "__alphanumerical__";
            else if (EmailRegex.Match(token).Success)
                return "__email__";
            else if (URLRegex.Match(token).Success)
                return "__url__";
            else if (DelimiterRegex.Match(token).Success)
                return "__delimiter__";

            return NormalizeRegex.Replace(token, string.Empty).ToLower();
        }
    }
}
