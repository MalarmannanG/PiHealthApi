using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace PiHealth.Web.Helper
{
    public static class InvoiceHelper
    {

        public static string GenerateInvoiceNum(long ReportId, string invoicePrefix)
        {
            string Invoicenumber = string.Concat(invoicePrefix, "-", String.Format("{0:0000000}", ReportId));
            return Invoicenumber;
        }
    }
}
