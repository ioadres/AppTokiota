using AppTokiota.Users.Components.Core;
using AppTokiota.Users.Components.Core.Module;
using AppTokiota.Users.Models;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        }

        private Models.Project _selectedProject;
        public Models.Project SelectedProject
        {
            get { return _selectedProject; }

            set {
                SetProperty(ref _selectedProject, value);
                Tasks = _selectedProject.Tasks.Select(x => x.Value).ToList();
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

            set { SetProperty(ref _selectedTask, value); }
        }

        public float _consumed;
        public float Consumed
        {
            get { return _consumed; }

            set { SetProperty(ref _consumed, value); }
        }

        public float _desviation;
        public float Desviation
        {
            get { return _desviation; }

            set { SetProperty(ref _desviation, value); }
        }

        private bool _confirmVisibility;
        public bool ConfirmVisibility
        {
            get { return _confirmVisibility; }
            set { SetProperty(ref _confirmVisibility, value); }
        }

        

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
            Title = "New Activity";
            ConfirmVisibility = true;
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            _context = parameters.GetValue<Imputed>(Imputed.Tag);
            Projects = _context.CurrentTimesheet.Projects;
            Title = _context.CurrentTimesheet.Day.Date.ToString("dd-MM-yyyy");

        }
    }
}
