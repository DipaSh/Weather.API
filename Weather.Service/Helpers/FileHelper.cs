using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace Weather.Service.Helpers
{
    public static class FileHelper
    {
        readonly static string OutputFolderPath = ConfigurationManager.AppSettings["CitiesOutputFolder"];
        public static Dictionary<long, string> RetrieveCities(string filePath)
        {
            Dictionary<long, string> dctResult = new Dictionary<long, string>();
            try
            {
                List<string> fileContent = ReadFileContent(filePath);
                foreach (var line in fileContent)
                {
                    string[] cities = line.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                    dctResult.Add(Convert.ToInt64(cities[0].Trim()), cities[1]);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return dctResult;
        }

        public static void SaveAPIResponse(long cityId, string fileContent)
        {
            try
            {
                string filePath = GetOutputFileName(cityId);
                WriteFileContent(filePath, fileContent);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static string GetOutputFileName(long cityId)
        {
            string filePath;
            try
            {
                filePath = $"{OutputFolderPath}\\{cityId}_{DateTime.Now.ToShortDateString()}";
            }
            catch (Exception)
            {

                throw;
            }
            return filePath;
        }

        public static List<string> ReadFileContent(string filePath)
        {
            List<string> fileContent =new List<string>();
            string line;
            try
            {
                using (StreamReader stream = new StreamReader(filePath))
                {
                    while ((line = stream.ReadLine()) != null)
                    {
                        if (!String.IsNullOrEmpty(line))
                        {
                            fileContent.Add(line);
                        }
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            return fileContent;
        }

        public static void WriteFileContent(string filePath, string fileContent)
        {
            try
            {
                using (StreamWriter writer = File.CreateText(filePath))
                {
                    writer.Write(fileContent);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}