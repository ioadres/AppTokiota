using AppTokiota.Users.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppTokiota.Users.Services
{
    public interface IReviewService
    {
        Task<Review> GetReviewForMonth(DateTime from, DateTime To);
    }
}
