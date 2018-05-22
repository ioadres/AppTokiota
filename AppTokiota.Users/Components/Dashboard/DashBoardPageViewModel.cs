using AppTokiota.Users.Components.Core;
using AppTokiota.Users.Components.Core.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Navigation;
using AppTokiota.Users.Components.Connection;
using SkiaSharp;
using Microcharts;
using System.Diagnostics;
using Unity.Utility;

namespace AppTokiota.Users.Components.DashBoard
{
    public class DashBoardPageViewModel : ViewModelBase
    {
        private readonly IDashBoardModule _dashBoardModule;

		public DashBoardPageViewModel(IViewModelBaseModule baseModule, IDashBoardModule dashBoardModule) : base(baseModule)
        {
            Title = "Dashboard";
            _dashBoardModule = dashBoardModule;

        }

		private Microcharts.DonutChart _chartConsumedMonthVsHourMonthExpected;
        public Microcharts.DonutChart ChartConsumedMonthVsHourMonthExpected
        {
			get { return _chartConsumedMonthVsHourMonthExpected; }
			set { SetProperty(ref _chartConsumedMonthVsHourMonthExpected, value); }
        }

		private Microcharts.DonutChart _chartProjectsImputed;
		public Microcharts.DonutChart ChartProjectsImputed
        {
			get { return _chartProjectsImputed; }
			set { SetProperty(ref _chartProjectsImputed, value); }
        }

		private Microcharts.DonutChart _chartImputedVsDeviation;
		public Microcharts.DonutChart ChartImputedVsDeviation
        {
			get { return _chartImputedVsDeviation; }
			set { SetProperty(ref _chartImputedVsDeviation, value); }
        }

		#region MethodLoadDataAsync
		protected async void LoadDataAsync()
        {
           IsBusy = true;
		   try
            {
                if (this.IsInternetAndCloseModal())
                {
					var now = DateTime.Now;
                    var minMonth = new DateTime(now.Year, now.Month, 1);
                    var maxMonth = minMonth.AddMonths(1).AddDays(-1);

					var timesheet = await _dashBoardModule.TimesheetService.GetTimesheetBeetweenDates(minMonth, maxMonth);

					GenerateChartActivitiesImputationVsDeviation(timesheet);
					GenerateChartImputationMonthVsHourMonthExpected(timesheet);
					GenerateChartActivitiesImputedGroupByTaskAndProject(timesheet);
  
                    IsBusy = false;
                }
            }
            catch (Exception ex)
            {
                IsBusy = false;
                BaseModule.DialogErrorCustomService.DialogErrorCommonTryAgain();
                Debug.WriteLine($"[GetTimesheet] Error: {ex}");
            }
        }
        #endregion
      
		private async void GenerateChartActivitiesImputationVsDeviation(Models.Timesheet timesheet) {
			ChartImputedVsDeviation = new DonutChart()
            {
                Entries = _dashBoardModule.ChartService.GenerateChartActivitiesImputationVsDeviation(timesheet)
            };

            await Task.FromResult(true);
		}

		private async void GenerateChartImputationMonthVsHourMonthExpected(Models.Timesheet timesheet)
        {
			ChartConsumedMonthVsHourMonthExpected = new DonutChart()
            {
                Entries = _dashBoardModule.ChartService.GenerateChartImputationMonthVsHourMonthExpected(timesheet)
            };

            await Task.FromResult(true);
        }

		private async void GenerateChartActivitiesImputedGroupByTaskAndProject(Models.Timesheet timesheet)
        {
            ChartProjectsImputed = new DonutChart()
            {
                Entries = _dashBoardModule.ChartService.GenerateChartActivitiesImputedGroupByTaskAndProject(timesheet)
            };

            await Task.FromResult(true);
        }

		public override void OnNavigatedTo(NavigationParameters parameters)
        {
			LoadDataAsync();
        }

    }
}
