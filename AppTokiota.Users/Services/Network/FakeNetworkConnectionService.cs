using System;
using System.Collections.Generic;
using System.Text;

namespace AppTokiota.Users.Services
{
    public class FakeNetworkConnectionService : INetworkConnectionService
    {
        public FakeNetworkConnectionService()
        {
        }

        public bool IsAvailable()
        {
           return true;
        }


    }
}
