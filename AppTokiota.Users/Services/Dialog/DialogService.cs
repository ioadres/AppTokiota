using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTokiota.Users.Services
{
    public class DialogService : IDialogService
    {
        public Task ShowAlertAsync(string message, string title, string buttonLabel)
        {
            return UserDialogs.Instance.AlertAsync(message, title, buttonLabel);
        }

        public async void ShowLoading() {
			UserDialogs.Instance.ShowLoading("Please Wait...", MaskType.Black);
			await Task.FromResult(true);
        }

		public async void HideLoading()
        {
            UserDialogs.Instance.HideLoading();
			await Task.FromResult(true);
        }


        public void ShowToast(string message, int duration = 2000)
        {
            var toastConfig = new ToastConfig(message);
			toastConfig.Position = ToastPosition.Top;
            toastConfig.SetDuration(duration);            
            toastConfig.SetMessageTextColor(System.Drawing.Color.White);
            toastConfig.SetBackgroundColor(System.Drawing.Color.FromArgb(33, 44, 55));
            UserDialogs.Instance.Toast(toastConfig);
        }

        public Task<bool> ShowConfirmAsync(string message, string title, string okLabel, string cancelLabel)
        {
            return UserDialogs.Instance.ConfirmAsync(message, title, okLabel, cancelLabel);
        }

        public Task<string> SelectActionAsync(string message, string title, IEnumerable<string> options)
        {
            return SelectActionAsync(message, title, "Cancel", options);
        }

        public async Task<string> SelectActionAsync(string message, string title, string cancelLabel, IEnumerable<string> options)
        {
            try
            {
                if (options == null)
                {
                    throw new ArgumentNullException(nameof(options));
                }

                if (!options.Any())
                {
                    throw new ArgumentException("No options provided", nameof(options));
                }

                string result =
                    await UserDialogs.Instance.ActionSheetAsync(message, cancelLabel, null, buttons: options.ToArray());

                return options.Contains(result)
                    ? result
                    : cancelLabel;
            }
            catch
            {
                return string.Empty;
            }
        }
    
    }
}