﻿using AppTokiota.Users.Components.Core;
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
using Xamarin.Forms;
using Microsoft.AppCenter.Crashes;

namespace AppTokiota.Users.Components.DashBoard
{
    public class DashBoardPageViewModel : ViewModelBase
    {
        private readonly IDashBoardModule _dashBoardModule;

		public DashBoardPageViewModel(IViewModelBaseModule baseModule, IDashBoardModule dashBoardModule) : base(baseModule)
        {
            Title = "Dashboard";
            _dashBoardModule = dashBoardModule;
            IsBusy = true;
			ChartProjectsImputedIsVisible = true;
			ChartImputedVsDeviationIsVisible = true;
            LoadDataAsync();
        }
        
		private Microcharts.DonutChart _chartConsumedMonthVsHourMonthExpected;
        public Microcharts.DonutChart ChartConsumedMonthVsHourMonthExpected
        {
			get { return _chartConsumedMonthVsHourMonthExpected; }
			set { SetProperty(ref _chartConsumedMonthVsHourMonthExpected, value); }
        }

		private bool _chartProjectsImputedIsVisible;
		public bool ChartProjectsImputedIsVisible
        {
			get { return _chartProjectsImputedIsVisible; }
			set { SetProperty(ref _chartProjectsImputedIsVisible, value); }
        }

        private Microcharts.RadarChart _chartProjectsImputed;
        public Microcharts.RadarChart ChartProjectsImputed
        {
			get { return _chartProjectsImputed; }
			set { SetProperty(ref _chartProjectsImputed, value); }
        }

		private bool _chartImputedVsDeviationIsVisible;
		public bool ChartImputedVsDeviationIsVisible
        {
			get { return _chartImputedVsDeviationIsVisible; }
			set { SetProperty(ref _chartImputedVsDeviationIsVisible, value); }
        }

		private Microcharts.DonutChart _chartImputedVsDeviation;
		public Microcharts.DonutChart ChartImputedVsDeviation
        {
			get { return _chartImputedVsDeviation; }
			set { SetProperty(ref _chartImputedVsDeviation, value); }
        }

		private string _statusMonth = "0.00 %";
		public string StatusMonth 
		{
			get { return _statusMonth; }
			set { SetProperty(ref _statusMonth, value); }
		}

		private bool _isHolidayTomorrow;
		public bool IsHolidayTomorrow
        {
			get { return _isHolidayTomorrow; }
			set { SetProperty(ref _isHolidayTomorrow, value); }
        }

        private bool _reDraw = false;
        public bool ReDraw
        {
            get { return _reDraw; }
            set { SetProperty(ref _reDraw, value); }
        }

        #region MethodLoadDataAsync
        protected async void LoadDataAsync()
        {
		   try
            {
                if (this.IsInternetAndCloseModal())
                {
					var now = DateTime.Now;
                    var minMonth = new DateTime(now.Year, now.Month, 1);
                    var maxMonth = minMonth.AddMonths(1).AddDays(-1);

					var timesheet = await _dashBoardModule.TimesheetService.GetTimesheetBeetweenDates(minMonth, maxMonth);
                    
                    IsHolidayTomorrow = false;
					var tomorrowDate = new DateTime(now.Year, now.Month, now.Day);
					tomorrowDate = tomorrowDate.AddDays(1);

					var dayTimesheet = timesheet.Days.Where(x => x.Date.Equals(tomorrowDate)).FirstOrDefault();
					if(dayTimesheet != null && dayTimesheet.Holiday != null) {
						IsHolidayTomorrow = dayTimesheet.Holiday.IsHolyday;
					}

					await Task.WhenAll(GenerateChartActivitiesImputationVsDeviation(timesheet), GenerateChartImputationMonthVsHourMonthExpected(timesheet), GenerateChartActivitiesImputedGroupByTaskAndProject(timesheet));

                    if (Device.RuntimePlatform == Device.Android)
                    {
                        Device.StartTimer(new TimeSpan(0, 0, 0, 1, 100), () =>
                        {
                            IsBusy = false;
                            ReDraw = true;
                            return false;
                        });
                    } else {
                        IsBusy = false;
                        ReDraw = true;
                    }
                }
            }
            catch (Exception ex)
            {
                IsBusy = false;
                BaseModule.DialogErrorCustomService.DialogErrorCommonTryAgain();
                Crashes.TrackError(ex);
            }
        }
        #endregion
      
		private Task GenerateChartActivitiesImputationVsDeviation(Models.Timesheet timesheet) {

			return Task.Run(() => {
				var entries = _dashBoardModule.ChartService.GenerateChartActivitiesImputationVsDeviation(timesheet);
                ChartImputedVsDeviation = new DonutChart()
                {
					LabelTextSize = Device.Idiom == TargetIdiom.Tablet? 30:25,
					Entries = entries
                };
				ChartImputedVsDeviationIsVisible = entries != null && entries.Sum(x=>x.Value) > 0 ? true : false;
			});             
		}

		private Task GenerateChartImputationMonthVsHourMonthExpected(Models.Timesheet timesheet)
        {
			return Task.Run(() => {
				var entries = _dashBoardModule.ChartService.GenerateChartImputationMonthVsHourMonthExpected(timesheet);
                ChartConsumedMonthVsHourMonthExpected = new DonutChart()
                {
					LabelTextSize = Device.Idiom == TargetIdiom.Tablet ? 30 : 25,
                    Entries = entries,
                };

                var consumed = entries.FirstOrDefault();
                var desviation = entries.LastOrDefault();
                if (consumed != null && desviation != null)
                {
                    var total = consumed.Value + desviation.Value;
					StatusMonth = $"{(consumed.Value * 100 / total).ToString("0.00")} %";
                }
            });
        }
        
		private Task GenerateChartActivitiesImputedGroupByTaskAndProject(Models.Timesheet timesheet)
        {
			return Task.Run(() => {
				var entries = _dashBoardModule.ChartService.GenerateChartActivitiesImputedGroupByTaskAndProject(timesheet);
                var chartT = new RadarChart()
                {
					Entries = entries
                };

				chartT.LabelTextSize = Device.Idiom == TargetIdiom.Tablet ? 30 : 20;


				ChartProjectsImputed = chartT;

				ChartProjectsImputedIsVisible = entries != null && entries.Sum(x => x.Value) > 0 ? true : false;
            }); 
        }
   }
}
