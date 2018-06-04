using Akavache;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;
using System.Diagnostics;

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
            catch (Exception ex)
            {
				Debug.WriteLine(ex);
                return default(T);
            }
        }
        public async Task<T> GetLocalObjectAsync<T>(string key)
        {
            try
            {
                var res = await BlobCache.LocalMachine.GetObject<T>(key);
                return res;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return default(T);
            }
        }

        public async Task InsertObjectAsync<T>(string key, T value)
        {
            await BlobCache.UserAccount.InsertObject<T>(key, value);
        }

        public async Task InsertLocalObjectAsync<T>(string key, T value)
        {
            await BlobCache.LocalMachine.InsertObject<T>(key, value);
        }


        public async Task RemoveObjectAsync<T>(string key)
        {
            await BlobCache.UserAccount.InvalidateObject<T>(key);
        }

        public async Task DeleteAll()
        {
            await BlobCache.UserAccount.InvalidateAll();
        }

        public async Task DeleteLocalAll()
        {
            await BlobCache.LocalMachine.InvalidateAll();
        }

        public async Task InsertObjectAsync<T>(string key, T value, DateTimeOffset dateTimeOffset)
        {
            await BlobCache.UserAccount.InsertObject<T>(key, value, dateTimeOffset);
        }

        #endregion
    }
}
