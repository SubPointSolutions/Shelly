using System;
using System.IO;

namespace SubPointSolutions.Shelly.Core.Services
{
    public class ShAppPersistentStorageDataService : ShAppDataServiceBase
    {
        #region constructors

        public ShAppPersistentStorageDataService()
        {
            AppDataFolder = "app-data";
        }

        #endregion

        #region properties

        private string AppDataFolder { get; set; }

        private string AppDirectory
        {
            get { return AppDomain.CurrentDomain.BaseDirectory; }
        }

        #endregion

        #region methods

        private string GetFilePath(string fileName)
        {
            var appDataDirectory = Path.Combine(AppDirectory, AppDataFolder);
            Directory.CreateDirectory(appDataDirectory);

            var filePath = Path.Combine(appDataDirectory, fileName);

            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            return filePath;
        }

        public void SaveTextData(string containerName, string data)
        {
            var filePath = GetFilePath(containerName);

            File.WriteAllText(filePath, data);
        }

        public string LoadTextData(string containerName)
        {
            var result = string.Empty;

            var filePath = GetFilePath(containerName);

            if (File.Exists(filePath))
                return File.ReadAllText(filePath);

            return result;
        }

        #endregion
    }
}
