using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace newWebAPI.Services
{
    public class UserDataService : IUserDataService
    {
        private List<string> Elements {get;set;} 

        public UserDataService()
        {
            var rnd = new Random();
            Elements = new List<string>();
            Elements.Add($"Value {rnd.Next()}");
            Elements.Add($"Value {rnd.Next()}");
            Elements.Add($"Value {rnd.Next()}");
        }

        public List<string> GetValues()
        {
            return Elements;
        }
    }
}