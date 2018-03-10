using System;
using Prism.Ioc;

namespace AppTokiota.Components.Core.Interfaces
{
    public abstract class BaseLoginModule : BaseModule
    {
        private const String UrlCompany = "http://tokiota.com/";
        public String GetUrlCompamy() {
            return UrlCompany;
        }
    }
}
