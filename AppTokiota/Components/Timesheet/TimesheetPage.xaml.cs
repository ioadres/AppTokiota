using AppTokiota.Components.Core.Module;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTokiota.Components.Timesheet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TimesheetPage : ContentPage
    {
        private ITimesheetModule _timesheetModule;
        public TimesheetPage(ITimesheetModule timesheetModule)
        {
            _timesheetModule = timesheetModule;
            InitializeComponent();
        }
    }
}
