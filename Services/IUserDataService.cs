using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace newWebAPI.Services
{
    public interface IUserDataService
    {
        List<string> GetValues();
    }
}