using System;
using Ankiro.Framework.Tools.Bus;
using RavenPersistance;

namespace RavenMigration
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			//throw new UnauthorizedAccessException("This migration is complete dont run twice.");
			Console.WriteLine("Starting migration");
			var bus = new DomainBus();
			var destinationStorage = new RaventEventStorage(new ServerStoreFactory("RavenHq"), bus);
			var handler = new MigrationHandler(destinationStorage);
			bus.RegisterHandler(()=>handler);
			new RaventEventStorage(new ServerStoreFactory(), bus);
			Console.WriteLine("Migration complete");
			Console.ReadKey();
		}
	}
}
