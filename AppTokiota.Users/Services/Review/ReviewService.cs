using AppTokiota.Users.Models;
using AppTokiota.Users.Services.Cache;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppTokiota.Users.Services
{
    public class ReviewService : IReviewService
    {
        private IRequestService _requestService;
        private ICacheEntity _cacheService;

        public ReviewService(IRequestService requestService, ICacheEntity cacheService)

        {
            _cacheService = cacheService;
            _requestService = requestService;
        }

        public async Task<Review> GetReviewForMonth (DateTime year, DateTime month)
        {
            //Todo 
            var url = $"{AppSettings.TimesheetUrlEndPoint}/{year}/{month}/review";
            var review = await _requestService.GetAsync<Review>(url, AppSettings.AuthenticatedUserResponse.AccessToken);
            return review;
        }

        //TODO
        //public async Task<Review> PutReviewForMonth(DateTime from, DateTime to)
        //{

        //}
    }
}
