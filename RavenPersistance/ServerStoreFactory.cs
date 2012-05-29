using EventSourcingTest;
using Raven.Abstractions.Data;
using Raven.Client;

namespace RavenPersistance
{
    public class ServerStoreFactory : IDocumentStoreFactory
    {
    	private readonly string _connectionStringName;

    	public ServerStoreFactory(string connectionStringName = "RavenDB")
    	{
    		_connectionStringName = connectionStringName;
    	}

    	public IDocumentStore BuildStore
        {
            get
            {
                var parser = ConnectionStringParser<RavenConnectionStringOptions>.FromConnectionStringName(_connectionStringName);
                parser.Parse();
               
                var documentStore = new Raven.Client.Document.DocumentStore
                                        {
                                            ApiKey = parser.ConnectionStringOptions.ApiKey,
                                            Url = parser.ConnectionStringOptions.Url,
                                            Conventions =
                                                {
                                                    FindTypeTagName = type => typeof(Event).IsAssignableFrom(type) ? "events" : null,

                                                }
                                        };
                documentStore.Initialize();
                return documentStore;
            }
        }
    }
}