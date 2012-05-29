using EventSourcingTest;
using Raven.Client;
using Raven.Client.Embedded;

namespace RavenPersistance
{
    public class DataFileDocumentStoreFactory : IDocumentStoreFactory
    {
        public IDocumentStore BuildStore
        {
            get
            {
                var documentStore = new EmbeddableDocumentStore
                                        {
                                            DataDirectory = "Data",
                                            UseEmbeddedHttpServer = true,
                                            Conventions =
                                                {
                                                    FindTypeTagName = type => typeof(Event).IsAssignableFrom(type) ? "events" : null,

                                                }
                                        };
                Raven.Database.Server.NonAdminHttp.EnsureCanListenToWhenInNonAdminContext(8080);
                documentStore.Initialize();
                return documentStore;
            }
        }
    }
}