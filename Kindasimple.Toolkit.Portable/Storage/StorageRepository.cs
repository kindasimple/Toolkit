using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kindasimple.Toolkit.Storage
{
    public interface StorageRepository
    {
        Task<T> ReadAsync<T>(string key);
        Task<bool> WriteAsync<T>(string key, T data);

        bool Exists(string key);
    }
}
