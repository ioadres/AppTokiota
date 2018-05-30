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
        public async Task<Review> GetReview(int mont, int year)
        {
            return await Task.Run(() => JsonConvert.DeserializeObject<Review>(FakeReviewData.Review));
        }

        //Todo
        public async Task<Review> PutReview(int from, int To)
        {
            return await Task.Run(() => JsonConvert.DeserializeObject<Review>(FakeReviewData.Review));
        }
    }
}
