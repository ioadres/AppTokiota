using System;
using AppTokiota.Components.Core.Interfaces;
using Prism.Ioc;

namespace AppTokiota.Components.Core
{
    public class BaseModule
    {
        public String Tag;

        public virtual void Register(IContainerRegistry containerRegistry)
        {
        }
    }


}
