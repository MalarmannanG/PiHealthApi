using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PiHealth.Web.Model.Account
{
    public class LabInfoModel:BaseResponseModel
    {
        public long id { get; set; }
        public string name { get; set; }
        public string area { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string ulIdFormat { get; set; }
        public string country { get; set; }
        public string pincode { get; set; }
        public string telNo { get; set; }
        public string phoneNo1 { get; set; }
        public string phoneNo2 { get; set; }
        public string email { get; set; }
        public string website { get; set; }
        public string termsAndConditions { get; set; }
        public string logo { get; set; }
        public string labIncharge { get; set; }
        public string manager { get; set; }
        public string footerLeftText { get; set; }
        public string footerRightText { get; set; }
        public string receiptHeader { get; set; }
        public string receiptPaperSize { get; set; }
        public string invoicePaperSize { get; set; }
        public string reportPaperSize { get; set; }
        public bool enableReceiptHeader { get; set; }
        public bool enableReceiptFooter { get; set; }
        public bool enableInvoiceFooter { get; set; }
        public bool enableReportFooter { get; set; }
        public FileUploadModel labLogo { get; set; }

    }

    public class FileUploadModel
    {
        public string name { get; set; }
        public string extension { get; set; }
        public string nameWithExtension { get; set; }
        public string byteArray { get; set; }
    }
}
