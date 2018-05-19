using AppTokiota.Users.Components.Core.Module;
using AppTokiota.Users.Services;


namespace AppTokiota.Users.Components.Review
{
    public class ReviewModule : IReviewModule
    {
        private readonly IReviewService _reviewService;
        public IReviewService ReviewService => _reviewService;

        public ReviewModule (IAuthenticationService authenticationService, IDialogService dialogService, IReviewService reviewService)
        {
            _reviewService = reviewService;
        }
    }
}
