using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Kindasimple.Toolkit.Storage
{
    public static class FileIO
    {
        public static bool DoesPackageFileExist(string filePath)
        {
            return Application.GetResourceStream(new Uri(filePath, UriKind.Relative)) != null;
        }

        public static bool DoesFileExist(string filePath)
        {
            IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication();
            return (myIsolatedStorage.FileExists(filePath));
        }

        public static async Task<bool> DoesFileExistAsync(string path)
        {
            try
            {
                await Windows.Storage.StorageFile.GetFileFromPathAsync(path);

                // file found
                return (true);
            }
            catch
            {
                // file not found
                return (false);
            }
        }
    }
}
