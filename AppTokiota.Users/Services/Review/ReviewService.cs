using AppTokiota.Users.Models;
using AppTokiota.Users.Services.Cache;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace AppTokiota.Users.Services
{
    public class ReviewService : IReviewService
    {
        private IRequestService _requestService;
        private ICacheEntity _cacheService;
        private IAuthenticationService _authenticationService;

        public ReviewService(IRequestService requestService, ICacheEntity cacheService, IAuthenticationService authentication)

        {
            _cacheService = cacheService;
            _requestService = requestService;
            _authenticationService = authentication;
        }

        public async Task<Review> GetReview (int year, int month)
        {
            Review review = await _cacheService.GetObjectAsync<Review>($"/{year}/{month}/review");
            for (var i = 0; i < 2; i++)
            {
                try
                {
                    if (await _authenticationService.UserIsAuthenticatedAndValidAsync())
                    {
                        if (review == null)
                        {
                            var nowLess3Month = DateTime.Now.AddMonths(-3);
                            var requestDate = new DateTime(year, month, 1);

                            var url = $"{AppSettings.TimesheetUrlEndPoint}/{year}/{month}/review";
                            review = await _requestService.GetAsync<Review>(url, AppSettings.AuthenticatedUserResponse.AccessToken);
                            if (requestDate < nowLess3Month)
                            {
                                await _cacheService.InsertObjectAsync($"/{year}/{month}/review", review);
                            }
                        }
                        return review;
                    }
                    else
                    {
                        throw new UnauthorizedAccessException();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
            throw new UnauthorizedAccessException();
        }

        //TODO
        //public async Task<Review> PutReview(int year, int month)
        //{

        //}
    }
}
