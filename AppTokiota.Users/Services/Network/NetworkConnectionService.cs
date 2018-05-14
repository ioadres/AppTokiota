using System;
using System.Net.NetworkInformation;

using Plugin.Connectivity;
using System.Threading.Tasks;

namespace AppTokiota.Users.Services
{
	public class NetworkConnectionService: INetworkConnectionService
    {
		public NetworkConnectionService()
        {
        }
        
		public bool IsAvailable() {
			var task = Task.Run(() =>
			{
				return CrossConnectivity.Current.IsConnected;//IsReachable(AppSettings.TimesheetDomain, 1000);
			});

			task.Wait();

            return task.Result;
		}


    }
}
