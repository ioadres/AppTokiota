using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppTokiota.Users.Services.Cache
{
    public interface ICacheEntity
    {
        Task<T> GetObjectAsync<T>(string key);
        Task InsertObjectAsync<T>(string key, T value);
        Task RemoveObjectAsync<T>(string key);
        Task InsertObjectAsync<T>(string idAppCache, T v, DateTimeOffset dateTimeOffset);
        Task DeleteAll();
    }
}
