using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Contracts.ApiServices
{
    public interface IRouteManager
    {
        string GetNotesRoute();

        string GetNotesRouteName();
    }
}
