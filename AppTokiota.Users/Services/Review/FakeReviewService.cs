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

        public async Task<bool> PatchReview(int from, int To)
        {
            Random gen = new Random();
            int prob = gen.Next(100);
            return await Task.Run(() =>
            {
                return prob <= 20;
            });
        }
    }
}
