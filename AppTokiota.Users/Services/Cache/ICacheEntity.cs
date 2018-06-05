using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppTokiota.Users.Services
{
    public interface ICacheEntity
    {
        Task<T> GetObjectAsync<T>(string key);
        Task InsertObjectAsync<T>(string key, T value);
        Task RemoveObjectAsync<T>(string key);
        Task InsertObjectAsync<T>(string idAppCache, T v, DateTimeOffset dateTimeOffset);
        Task DeleteAll();

        Task<T> GetLocalObjectAsync<T>(string key);
        Task DeleteLocalAll();
        Task InsertLocalObjectAsync<T>(string key, T value);
    }
}
