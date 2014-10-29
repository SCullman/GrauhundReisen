using System.Collections.Generic;
using Nancy;
using Grauhundreisen.Infrastructure;

namespace GrauhundReisen.WebPortal.Controller
{
    public class Admin : NancyModule
    {
        public Admin(EventStoreClient client)
        {
            Get["show-all-my-events"] = _ =>
            {
                //dirty, sollte über Service laufen
                var events = client.RetrieveAllAsString();

				return View["show-all-my-events", events];
            };
        }
    }
}