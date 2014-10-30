using System;
using GrauhundReisen.Funktional;
using Nancy;
using Nancy.TinyIoc;
using Grauhundreisen.Infrastructure;

namespace GrauhundReisen.WebPortal
{
    public class Boostrapper : DefaultNancyBootstrapper
    {
		static string ConnectionString = System.Web.Hosting.HostingEnvironment.MapPath(@"\Content\DataStore\Bookings\");

        protected override void ApplicationStartup(TinyIoCContainer container, Nancy.Bootstrapper.IPipelines pipelines)
        {
            StaticConfiguration.DisableErrorTraces = false;

            SetupIoC(container);

            base.ApplicationStartup(container, pipelines);
        }

        private static void SetupIoC(TinyIoCContainer container)
        {
            container.Register(EventStoreClient.InitWith(EventStogeConfig));
            container.Register<ReadModel.BookingForm>();
            container.Register(new EventHandlers.Booking(ConnectionString));
            container.Register(new ReadModel.Bookings(ConnectionString));
        }

        private static EventStoreClientConfiguration EventStogeConfig
        {
            get
            {
                var esConfig = new EventStoreClientConfiguration
                {
                    AccountId = "neu_alt",
                    InitActionName = "init",
                    RemoveActionName = "remove",
                    RetrieveActionName = "events",
                    StoreActionName = "store",
                    ServerUri = new Uri("http://openspace2014.azurewebsites.net"),
                    DeserializeEvent =  Events.DeserializeEvent
                };

                return esConfig;
            }
        }
    }
}
