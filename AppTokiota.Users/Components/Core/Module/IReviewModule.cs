using AppTokiota.Users.Services;

namespace AppTokiota.Users.Components.Core.Module
{
    public interface IReviewModule: IBaseModule
    {
        IReviewService ReviewService { get; }
        ITimeLineService TimeLineService { get; }

    }
}
