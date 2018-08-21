using AppTokiota.Users.Models;
using Microsoft.AppCenter.Crashes;
using Polly;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppTokiota.Users.Services
{
    public class ReviewService : IReviewService, ISubscribeMessagingCenter
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
                try
                {
                    Review review = await _cacheService.GetObjectAsync<Review>($"/{year}/{month}/review");
                    if (review == null)
                    {
                        var nowLess3Month = DateTime.Now.AddMonths(-3);
                        var requestDate = new DateTime(year, month, 1);

                        var url = $"{AppSettings.TimesheetUrlEndPoint}/{year}/{month}/review";
                     
                        review = await Policy.Handle<Exception>()
                                .WaitAndRetryAsync(1, retryAtemp => TimeSpan.FromMilliseconds(100), async (exception, timeSpan, retryCount, context) => { await _authenticationService.UserIsAuthenticatedAndValidAsync(true); })
                                .ExecuteAsync<Review>(async () => {
                                    return await _requestService.GetAsync<Review>(url, AppSettings.AuthenticatedUserResponse.AccessToken);
                                });
                
                        if (requestDate < nowLess3Month)
                        {
                            await _cacheService.InsertObjectAsync($"/{year}/{month}/review", review);
                        }
                    }
                    

                    return review;
                }
                catch (Exception e)
                {
                    MessagingCenter.Send<ISubscribeMessagingCenter>(this, nameof(UnauthorizedAccessException));
                    Crashes.TrackError(e);
                    throw e;
                }   
        }


        public async Task<bool> PatchReview(int year, int month)
        {
            try
            {
                if (!AppSettings.SendReview) return true;

                var reviewDatos = new Review(); 
                var url = $"{AppSettings.TimesheetUrlEndPoint}/{year}/{month}/review";

                var response = await Policy.Handle<Exception>()
                            .WaitAndRetryAsync(1, retryAtemp => TimeSpan.FromMilliseconds(100), async (exception, timeSpan, retryCount, context) => { await _authenticationService.UserIsAuthenticatedAndValidAsync(true); })
                            .ExecuteAsync<bool>(async () => {
                                return await _requestService.PatchAsync<bool>(url, AppSettings.AuthenticatedUserResponse.AccessToken);
                            });
                return response;

            }
            catch (Exception e)
            {
                MessagingCenter.Send<ISubscribeMessagingCenter>(this, nameof(UnauthorizedAccessException));
                Crashes.TrackError(e);
                throw e;
            }   
        }

    }
}
