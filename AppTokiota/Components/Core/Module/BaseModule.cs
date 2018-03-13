using System;
using Prism.Ioc;
using AppTokiota.Components.Core.Module;

namespace AppTokiota.Components.Core.Module
{
    public class BaseModule : IBaseModule
    {
        protected String Tag;

        public string GetTag()
        {
            return Tag;
        }
    }
}
