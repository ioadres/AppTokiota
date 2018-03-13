using System;
using Prism.Ioc;
using AppTokiota.Components.Core.Module;

namespace AppTokiota.Components.Core.Module
{
    public class BaseModule : IBaseModule
    {
        public String _tag;
        
        public string GetTag()
        {
            return _tag;
        }
        public virtual void Register(IContainerRegistry containerRegistry)
        {
            throw new NotImplementedException();
        }

        public void SetTag(string value)
        {
            _tag = value;
        }
    }


}
