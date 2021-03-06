﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace Kindasimple.Toolkit.Storage
{
    public static class FileHelper
    {
        async public static Task<WriteableBitmap> ReadImageFromStorageAsync(string fileName, StorageType fileStorage = StorageType.Local)
        {
            StorageFolder storageFolder = GetStorageFolder(fileStorage);
            StorageFile file = await storageFolder.GetFileAsync(fileName).AsTask();
            WriteableBitmap image = new WriteableBitmap(1, 1);

            using (var stream = await file.OpenReadAsync().AsTask())
            {
                await image.SetSourceAsync(stream).AsTask();
            }

            return image;
        }

        async public static Task<bool> WriteImageToStorageAsync(string fileName, WriteableBitmap image, StorageType fileStorage = StorageType.Local)
        {
            StorageFolder storageFolder = GetStorageFolder(fileStorage);

            return await image.SaveToFile(storageFolder, fileName) != null;
        }

        async public static Task<bool> WriteFileToIsolatedStorageAsync(string filePath, StorageFile imageFile, StorageType fileStorage = StorageType.Local)
        {
            StorageFolder storageFolder = GetStorageFolder(fileStorage);

            return await imageFile.CopyAsync(storageFolder, filePath, NameCollisionOption.ReplaceExisting) != null;
        }

        async public static Task<T> ReadObjectFromStorageAsync<T>(string fileName, StorageType fileStorage = StorageType.Local)
        {
            try
            {
                StorageFolder storageFolder = GetStorageFolder(fileStorage);

                //load saved results from disk
                var file = await storageFolder.OpenStreamForReadAsync(fileName).ConfigureAwait(false);
                if (file != null)
                {
                    using (StreamReader sr = new StreamReader(file))
                    {
                        return JsonConvert.DeserializeObject<T>(sr.ReadToEnd());
                    }
                }
                else
                {
                    throw new FileNotFoundException();
                }
            }
            catch (Exception ex)
            {
                throw new IOException("Error reading file: " + fileName, ex);
            }
        }

        async public static Task<bool> WriteObjectToStorageAsync<T>(string fileName, T data, StorageType fileStorage = StorageType.Local)
        {
            try
            {
                StorageFolder storageFolder = GetStorageFolder(fileStorage);

                var content = JsonConvert.SerializeObject(data);
                var file = await storageFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
                using (var fs = await file.OpenAsync(FileAccessMode.ReadWrite))
                {
                    using (var outStream = fs.GetOutputStreamAt(0))
                    {
                        using (var dataWriter = new DataWriter(outStream))
                        {
                            if (content != null)
                                dataWriter.WriteString(content);

                            await dataWriter.StoreAsync().AsTask().ConfigureAwait(false);
                            dataWriter.DetachStream();
                        }

                        await outStream.FlushAsync();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error writing to isolated storage. [{0}]", ex.Message);
                return false;
            }
        }

        private static StorageFolder GetStorageFolder(StorageType fileStorage)
        {
            StorageFolder storageFolder = (fileStorage == StorageType.Local) ? ApplicationData.Current.LocalFolder : ApplicationData.Current.RoamingFolder;
            return storageFolder;
        }

        //async public static Task<bool> ExistsInStorageAsync(string fileName, FileStorage fileStorage = FileStorage.Local)
        //{
        //    StorageFolder storageFolder = (fileStorage == FileStorage.Local) ? ApplicationData.Current.LocalFolder : ApplicationData.Current.RoamingFolder;

        //    try
        //    {
        //        var file = await storageFolder.GetFileAsync(fileName).AsTask().ConfigureAwait(false);
        //        return file != null;
        //    }
        //    catch { return false; }
        //}

        async public static Task<bool> ExistsInStorageAsync(string fileName, StorageType fileStorage = StorageType.Local)
        {
            StorageFolder storageFolder = (fileStorage == StorageType.Local) ? ApplicationData.Current.LocalFolder : ApplicationData.Current.RoamingFolder;

            IReadOnlyList<StorageFile> storageFileList = await storageFolder.GetFilesAsync();
            StorageFile storageFile = (from StorageFile s in storageFileList
                                       where s.Name == fileName
                                       select s).FirstOrDefault();
            if (storageFile != null)
            {
                // Do it with your file here.
                return true;
            }
            else
            {
                // File does not exist.
                return false;
            }
        }

        async public static Task<bool> ExistsInAppPackage(string fileName)
        {
            try
            {
                return await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFileAsync(fileName).AsTask().ConfigureAwait(false) != null;
            }
            catch (FileNotFoundException ex)
            {
                System.Diagnostics.Debug.WriteLine("File not found. [{0}:{1}]", fileName, ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("File not found. [{0}:{1}]", fileName, ex.Message);
                throw ex;
            }
        }

        async public static Task<bool> Delete(string fileName, StorageType fileStorage = StorageType.Local)
        {
            StorageFolder storageFolder = GetStorageFolder(fileStorage);

            var file = await storageFolder.GetFileAsync(fileName).AsTask().ConfigureAwait(false);
            if (file != null)
            {
                await file.DeleteAsync();
                return true;
            }

            return false;
        }
    }
}
