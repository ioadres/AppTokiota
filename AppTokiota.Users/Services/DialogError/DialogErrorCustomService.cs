using System;
namespace AppTokiota.Users.Services
{
	public class DialogErrorCustomService: IDialogErrorCustomService
    {
		private IDialogService _dialogService;
		public DialogErrorCustomService(IDialogService dialogService)
        {
			_dialogService = dialogService;
        }


		public void DialogErrorConnection() {

			_dialogService.ShowAlertAsync(
                            "Check your internet connection and try again",
                            "Timeout",
                            "Ok");
		}

		public void DialogErrorCommonTryAgain() {
			_dialogService.ShowAlertAsync(
                        "An error ocurred, try again",
                        "Error",
                        "Ok");
		}
    }
}
