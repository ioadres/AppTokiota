using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppTokiota.Services.Authentication;

namespace AppTokiota.Components.Core.Module
{
    public interface IMenuModule
    {
        IAuthenticationService AuthenticationService { get; }
    }
}
