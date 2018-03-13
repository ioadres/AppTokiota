using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTokiota.Components.Core.Module
{
    public interface IBaseModule
    {
        string GetTag();
        void SetTag(string value);
        void Register(IContainerRegistry containerRegistry);
    }
}
