using ACS.Nutrition.Application.Contracts.Services;
using System;
using System.IO;
using System.Text;

namespace ACS.Nutrition.Application.Impl.Services
{
    public class TxtService: ITxtService
    {
        public void UpsertFile(string fileName, string content)
        {
            // Check if file already exists. If yes, delete it.     
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            // Create a new file     
            using (FileStream fs = File.Create(fileName))
            {
                // Add some text to file    
                Byte[] title = new UTF8Encoding(true).GetBytes(content);
                fs.Write(title, 0, title.Length);
            }
        }
    }
}
