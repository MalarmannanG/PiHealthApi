using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PiHealth.Web.Helper
{
    public static class BarcodeHelper
    {           

        public static bool GenerateBarCode(string value, string hostingPath)
        {
            try
            {            
            //var barcode = new Barcode(value, NetBarcode.BarcodeType.Code128A);
            //var FileNameUploaded = value + ".png";
            //var path = Path.Combine(PiHealthGlobal.BarCodePath, FileNameUploaded);
            //var fileName = Path.Combine(hostingPath, path);
            //await PiHealthFileHelper.SaveAsync(barcode.GetByteArray(), fileName);
            return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }        

    }
}
