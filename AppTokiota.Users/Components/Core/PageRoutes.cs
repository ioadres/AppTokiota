using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.WindowsSpecific;
namespace AppTokiota.Users.Components.Core
{
    public static class PageRoutes
    {      
        private static Dictionary<string, string> Keys = new Dictionary<string, string>();

        public static void AddKey<T>(string route) { 
            if(!Keys.ContainsKey(typeof(T).Name)) {
                Keys.Add(typeof(T).Name, route);    
            }
        }

        public static void AddKey(string key,string route)
        {
            if (!Keys.ContainsKey(key))
            {
                Keys.Add(key, route);
            }
        }

        public static string GetKey<T>()
        {
            string result;
            Keys.TryGetValue(typeof(T).Name, out result);
            return result;
        }

        public static string GetKey(string key)
        {
            string result;
            Keys.TryGetValue(key, out result);
            return result;
        }
    }
}
