using System;
using AppTokiota.Users.Models;
using Xamarin.Forms;

namespace AppTokiota.Users.Cell
{
	public class DayTemplateSelector : DataTemplateSelector
    {
        private readonly DataTemplate template1;
        private readonly DataTemplate template2;

        public DayTemplateSelector() {
            template1 = new DataTemplate(typeof(ProjectDataTemplate));
            template2 = new DataTemplate(typeof(NoProjectDataTemplate));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if(item is ReviewTimeLine) {
                var timeLine = (ReviewTimeLine)item;
                if (timeLine.ProjectsForDay > 0) return template1;
            }

            return template2;
        }
    }
}
