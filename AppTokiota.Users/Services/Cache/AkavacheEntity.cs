using Akavache;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;

namespace AppTokiota.Users.Services.Cache
{
    public class AkavacheEntity : ICacheEntity
    {
        #region IEntity implementation

        public async Task<T> GetObjectAsync<T>(string key)
        {
            try
            {
                var res = await BlobCache.UserAccount.GetObject<T>(key);
                return res;
            }
            catch (Exception e)
            {
                return default(T);
            }
        }

        public async Task InsertObjectAsync<T>(string key, T value)
        {
            await BlobCache.UserAccount.InsertObject<T>(key, value);
        }

        public async Task RemoveObjectAsync<T>(string key)
        {
            await BlobCache.UserAccount.InvalidateObject<T>(key);
        }

        public async Task DeleteAll()
        {
            await BlobCache.UserAccount.InvalidateAll();
        }

        public async Task InsertObjectAsync<T>(string key, T value, DateTimeOffset dateTimeOffset)
        {
            await BlobCache.UserAccount.InsertObject<T>(key, value, dateTimeOffset);
        }

        #endregion
    }
}
