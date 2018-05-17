using System;
namespace AppTokiota.Users.Services
{
	public interface IDialogErrorCustomService
	{
		void DialogErrorConnection();
		void DialogErrorCommonTryAgain();
	}
}
