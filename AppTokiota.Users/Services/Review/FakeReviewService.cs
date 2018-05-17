using AppTokiota.Users.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppTokiota.Users.Services
{
    class FakeReviewService: IReviewService
    {
        public async Task<Review> GetReviewForMonth(DateTime from, DateTime to)
        {
            return await Task.Run(() => JsonConvert.DeserializeObject<Review>(FakeReviewData.Review));
        }
    }
}
