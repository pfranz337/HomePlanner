using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPAdmin.UI.EventAgregators
{
    public class OnLoginEvent : PubSubEvent<string>
    {
    }

    public class OnLogoutEvent : PubSubEvent
    {
    }

    public class OnTasksNavigateEvent : PubSubEvent
    {
    }
}
