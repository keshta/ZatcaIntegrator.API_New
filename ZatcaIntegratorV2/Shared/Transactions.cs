using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZatcaIntegratorV2.Shared
{
    public static class Transactions
    {
        public static string B2C = "0100";
        public static string B2B = "1000";
        public static string B2BAndB2C = "1100";
        public static string Issued = "ISSUED";
        public const string CRN = "CRN";
        public const string CTH = "CTH";
        public const string OTH = "OTH";
        public const string VAT = "VAT";
        
        public static string DefaultCountryCode = "SA";
        public static string DefaultCurrency = "SAR";

        public static decimal VatStandard = 15;
        public static decimal VatZero = 0;

        public static string StandardCode = "0100000";
        public static string SimplifiedCode = "0200000";
    }
}
