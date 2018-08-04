using AppTokiota.Users.Components.Core;
using AppTokiota.Users.Components.Core.Module;
using AppTokiota.Users.Models;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AppTokiota.Users.Components.Activity
{
    public class AddActivityProjectPageViewModel : ViewModelBase
    {
        #region Services
        protected readonly IAddActivityModule _addActivityModule;
        #endregion

        private Models.Imputed _context;
        public Models.Imputed Context
        {
            get { return _context; }
            set { SetProperty(ref _context, value); }
        }

        private Models.Project _selectedProject;
        public Models.Project SelectedProject
        {
            get { return _selectedProject; }

            set
            {
                SetProperty(ref _selectedProject, value);
                ProjectSelected = true;
                ConfirmVisibility = false;
                if (_selectedProject != null)
                {
                    Tasks = _selectedProject?.Tasks?.Select(x => x.Value).ToList();
                }
            }
        }

        private List<Models.Project> _projects;
        public List<Models.Project> Projects
        {
            get { return _projects; }

            set { SetProperty(ref _projects, value); }
        }


        private List<Models.TaskT> _tasks;
        public List<Models.TaskT> Tasks
        {
            get { return _tasks; }

            set { SetProperty(ref _tasks, value); }
        }

        private Models.TaskT _selectedTask;
        public Models.TaskT SelectedTask
        {
            get { return _selectedTask; }

            set
            {
                SetProperty(ref _selectedTask, value);
                if (ConfirmVisibility == false) ConfirmVisibility = true;
                if (_selectedTask == null)
                {
                    Consumed = 0;
                    Deviation = 0;
                    ConfirmVisibility = false;
                }
                if (_selectedTask != null && Context.CurrentTimesheet != null)
                {
                    Consumed = _selectedTask.Consumed + Context.Consumed.GetMinutes();
                    Deviation = _selectedTask.Deviation + Context.Deviation.GetMinutes();
                }
                if (_selectedTask != null && Context.CurrentTimesheetMultipleDay != null)
                {
                    Consumed = _selectedTask.Consumed + (Context.Consumed.GetMinutes() * Context.CurrentTimesheetMultipleDay.Days.Count());
                    Deviation = _selectedTask.Deviation + (Context.Deviation.GetMinutes() * Context.CurrentTimesheetMultipleDay.Days.Count());
                }
            }
        }

        public float _consumed;
        public float Consumed
        {
            get { return _consumed; }

            set { SetProperty(ref _consumed, value); }
        }

        public float _deviation;
        public float Deviation
        {
            get { return _deviation; }

            set { SetProperty(ref _deviation, value); }
        }

        public string _description;
        public string Description
        {
            get { return _description; }

            set { SetProperty(ref _description, value); }
        }

        private bool _confirmVisibility;
        public bool ConfirmVisibility
        {
            get { return _confirmVisibility; }
            set { SetProperty(ref _confirmVisibility, value); }
        }

        private bool _projectSelected;
        public bool ProjectSelected
        {
            get { return _projectSelected; }
            set { SetProperty(ref _projectSelected, value); }
        }

        #region GoBack
        public DelegateCommand EndCommand => new DelegateCommand(End);
        public async void End()
        {
            try
            {
                IsBusy = true;
                if (IsInternetAndCloseModal())
                {

                    if (Context.CurrentTimesheet == null)
                    {
                        BaseModule.AnalyticsService.TrackEvent("[BatchActivity] :: Start");
                        var multiplesDay = Context.CurrentTimesheetMultipleDay.Days?.Select(x => new TimesheetAddActivityBatch()
                        {
                            Day = int.Parse(x.Date.ToString("dd")),
                            Month = int.Parse(x.Date.ToString("MM")),
                            Year = int.Parse(x.Date.ToString("yyyy")),
                            AssignementId = SelectedTask.AssignementId,
                            ProjectId = SelectedProject.Id,
                            Description = Description,
                            Deviation = Convert.ToInt16(Context.Deviation.GetMinutes()),
                            Imputed = Convert.ToInt16(Context.Consumed.GetMinutes()),
                            TaskId = SelectedTask.Id
                        });
                        await _addActivityModule.TimesheetService.BatchActivity(multiplesDay.ToList());
                        IsBusy = false;
                        CloseCommand.Execute();
                        BaseModule.AnalyticsService.TrackEvent("[BatchActivity] :: Success");
                    }
                    else
                    {

                        BaseModule.AnalyticsService.TrackEvent("[PostActivity] :: Start");

                        try
                        {
                            var response = await _addActivityModule.TimesheetService.PostActivity(new TimesheetAddActivity()
                            {
                                AssignementId = SelectedTask.AssignementId,
                                ProjectId = SelectedProject.Id,
                                Description = Description,
                                Deviation = Convert.ToInt16(Context.Deviation.GetMinutes()),
                                Imputed = Convert.ToInt16(Context.Consumed.GetMinutes()),
                                TaskId = SelectedTask.Id
                            }, Context.CurrentTimesheet.Day.Date);
                            var activityDay = ActivityDay.Map(response, Context.CurrentTimesheet);
                            var navigationParameters = new NavigationParameters();
                            navigationParameters.Add(AppTokiota.Users.Models.ActivityDay.Tag, activityDay);
                            IsBusy = false;
                            await BaseModule.NavigationService.GoBackAsync(navigationParameters);
                            BaseModule.AnalyticsService.TrackEvent("[PostActivity] :: Success");

                        }
                        catch (Exception e)
                        {
                            IsBusy = false;
                            if (e.Message == ("{\"message\":\"You can not add more hours than estimated in fixed assignements\"}"))
                            {
                                BaseModule.DialogService.ShowToast("You can not add more hours than estimated in fixed assignements");
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                IsBusy = false;
                BaseModule.DialogErrorCustomService.DialogErrorCommonTryAgain();
            }

        }
        #endregion

        #region GoBack
        public DelegateCommand GoBackCommand => new DelegateCommand(GoBack);
        protected void GoBack()
        {
            var navigationParameters = new NavigationParameters();
            navigationParameters.Add(Imputed.Tag, Context);
            BaseModule.NavigationService.NavigateAsync(PageRoutes.GetKey<AddActivityTimeDesviationPage>(), navigationParameters, false, false);
        }
        #endregion

        #region CloseAction
        public DelegateCommand CloseCommand => new DelegateCommand(Close);
        protected void Close()
        {
            BaseModule.NavigationService.GoBackAsync();
        }
        #endregion

        public AddActivityProjectPageViewModel(IViewModelBaseModule baseModule, IAddActivityModule addActivityModule) : base(baseModule)
        {
            _addActivityModule = addActivityModule;
            Title = "Select Project";
            ConfirmVisibility = false;
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey(Imputed.Tag))
            {
                Context = parameters.GetValue<Imputed>(Imputed.Tag);
                if (Context.CurrentTimesheet == null)
                {
                    Projects = Context.CurrentTimesheetMultipleDay.Projects;
                }
                else
                {
                    Projects = Context.CurrentTimesheet.Projects;
                }
            }
        }

        public override void OnNavigatedFrom(NavigationParameters parameters)
        {
            if (parameters.ContainsKey(Imputed.Tag))
            {
                Context = parameters.GetValue<Imputed>(Imputed.Tag);
                if (Context.CurrentTimesheet == null)
                {
                    Projects = Context.CurrentTimesheetMultipleDay.Projects;
                }
                else
                {
                    Projects = Context.CurrentTimesheet.Projects;
                }
            }
        }
    }
}
