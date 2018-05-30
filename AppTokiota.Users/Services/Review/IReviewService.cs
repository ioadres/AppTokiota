using AppTokiota.Users.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppTokiota.Users.Services
{
    public interface IReviewService
    {
        Task<Review> GetReview(int from, int To);
        Task<Review> PutReview(int from, int To);
    }
}
