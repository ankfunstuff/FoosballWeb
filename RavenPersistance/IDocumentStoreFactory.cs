using Raven.Client;

namespace RavenPersistance
{
    public interface IDocumentStoreFactory
    {
        IDocumentStore BuildStore { get; }
    }
}