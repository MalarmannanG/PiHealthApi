using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PiHealth.Web.Helper
{
    public static class PiHealthFileHelper
    {       
        public static void Save(byte[] data, string fileName)
        {
            try
            {
                CheckDirectory(fileName);
                System.IO.File.WriteAllBytes(fileName, data);                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public static async Task SaveAsync(byte[] data, string fileName)
        {
            try
            {
                CheckDirectory(fileName);
                await System.IO.File.WriteAllBytesAsync(fileName, data);                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
      
        public static async Task<bool> SaveToFile(this string fileContent, string fileName)
        {
            try
            {
                CheckDirectory(fileName);

                await System.IO.File.WriteAllTextAsync(fileName, fileContent);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //return false;
        }

        public static async Task<bool> AppendToFile(this string fileContent, string fileName)
        {
            try
            {
                CheckDirectory(fileName);

                await System.IO.File.AppendAllLinesAsync(fileName, new List<string> { fileContent });
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //return false;
        }

        public static async Task<bool> AppendToFile(this List<string> fileContent, string fileName)
        {
            try
            {
                CheckDirectory(fileName);

                await System.IO.File.AppendAllLinesAsync(fileName, fileContent);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //return false;
        }


        public static bool Delete(string fileName)
        {
            try
            {
                if (File.Exists(fileName))
                    File.Delete(fileName);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //return false;
        }


        private static void CheckDirectory(string fileName)
        {
            var directory = Path.GetDirectoryName(fileName);
            if (!Directory.Exists(directory))
                System.IO.Directory.CreateDirectory(directory);
        }
        public static string Combine(string path1, string path2)
        {
            if (string.IsNullOrEmpty(path1) || string.IsNullOrWhiteSpace(path1) || string.IsNullOrEmpty(path2) || string.IsNullOrWhiteSpace(path2))
            {
                throw new ArgumentNullException();
            }

            string combinedPath = "";

            path2 = path2.TrimStart(Path.DirectorySeparatorChar);
            path2 = path2.TrimStart(Path.AltDirectorySeparatorChar);

            combinedPath = Path.Combine(path1, path2);
            return combinedPath;
        }

        public static async Task<bool> WriteToFile(this string fileContent, string fileName)
        {
            try
            {
                CheckDirectory(fileName);

                var LogFile = new FileInfo(fileName);
                var swLogFile = LogFile.AppendText();
                await swLogFile.WriteLineAsync(fileContent);
                swLogFile.Close();

                //await System.IO.File.AppendAllLinesAsync(fileName, fileContent);
                return true;
            }
            catch (Exception ex)
            {
            }
            return false;
        }

    }
}
