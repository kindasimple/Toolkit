using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Kindasimple.Toolkit.Storage
{
    public static class FileIO
    {
        public static async Task<bool> DoesPackageFileExistAsync(string filePath)
        {
            bool found = false;
            try
            {
                Windows.ApplicationModel.Package package = Windows.ApplicationModel.Package.Current;
                Windows.Storage.StorageFolder installedLocation = package.InstalledLocation;
                StorageFile file = await installedLocation.GetFileAsync("Assets\\" + filePath);
                if (file != null)
                {
                    found = true;
                }
            }
            catch (System.IO.FileNotFoundException ex)
            {
                found = false;
            }
            return found;
        }

        public static async Task<bool> DoesFileExistAsync(string filePath)
        {
            try
            {
                var file = await Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync(filePath);
                if (file != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
