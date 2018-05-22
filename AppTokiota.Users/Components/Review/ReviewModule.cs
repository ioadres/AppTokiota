using AppTokiota.Users.Components.Core.Module;
using AppTokiota.Users.Services;


namespace AppTokiota.Users.Components.Review
{
    public class ReviewModule : IReviewModule
    {
        private readonly IReviewService _reviewService;
        private readonly ITimeLineService _timeLineService; 
        public IReviewService ReviewService => _reviewService;
        public ITimeLineService TimeLineService => _timeLineService;

        public ReviewModule (IAuthenticationService authenticationService, IDialogService dialogService, IReviewService reviewService, ITimeLineService timeLineService)
        {
            _reviewService = reviewService;
            _timeLineService = timeLineService; 
        }
    }
}
