using System.Configuration;
using Ankiro.Framework.Tools.Bus;
using EventSourcingTest;
using EventSourcingTest.Interfaces;
using FoossballPlayars.QueryContext;
using FoossballPlayars.QueryContext.Teams;
using FoossballPlayars.Services;
using FoossballWeb.Controllers;
using RavenPersistance;

[assembly: WebActivator.PreApplicationStartMethod(typeof(FoossballWeb.App_Start.NinjectMVC3), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(FoossballWeb.App_Start.NinjectMVC3), "Stop")]

namespace FoossballWeb.App_Start
{
	using Microsoft.Web.Infrastructure.DynamicModuleHelper;
	using Ninject;
	using Ninject.Web.Mvc;

	public static class NinjectMVC3
	{
		private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

		/// <summary>
		/// Starts the application
		/// </summary>
		public static void Start()
		{
			DynamicModuleUtility.RegisterModule(typeof(OnePerRequestModule));
			DynamicModuleUtility.RegisterModule(typeof(HttpApplicationInitializationModule));
			Bootstrapper.Initialize(CreateKernel);
		}

		/// <summary>
		/// Stops the application.
		/// </summary>
		public static void Stop()
		{
			Bootstrapper.ShutDown();
		}

		/// <summary>
		/// Creates the kernel that will manage your application.
		/// </summary>
		/// <returns>The created kernel.</returns>
		private static IKernel CreateKernel()
		{
			var kernel = new StandardKernel();
			RegisterServices(kernel);
			return kernel;
		}

		/// <summary>
		/// Load your modules or register your services here!
		/// </summary>
		/// <param name="kernel">The kernel.</param>
		private static void RegisterServices(IKernel kernel)
		{
			var scoreCalculator = new EloCalculator();
			var scoreQuery = new ScoreQuery(new WebSignaler(), scoreCalculator);
			var eventBus = new DomainBus();
			var teamService = new TeamService();
			eventBus.RegisterHandler(() => new GameHandler(scoreQuery));
			eventBus.RegisterHandler(() => teamService);
			kernel.Bind<IScoreQuery>().ToConstant(scoreQuery);
			kernel.Bind<ITeamService>().ToConstant(teamService);
			var eventStorage = GetEventStorage(eventBus);
			var sessionFactory = new SessionFactory(eventStorage);
			kernel.Bind<IEventStorage>().ToConstant(eventStorage);
			kernel.Bind<ISessionFactory>().ToConstant(sessionFactory);
			var gameService = new GameService(sessionFactory, eventBus);
			var commandbus = new DomainBus();
			commandbus.RegisterHandler(() => gameService);
			kernel.Bind<IBus>().ToConstant(commandbus);
		}

		private static RaventEventStorage GetEventStorage(IBus eventBus)
		{
//#if DEBUG
//            var eventStorage = new ReadOnlyRavenEventStorage(new ServerStoreFactory(), eventBus);
//#else
    		var eventStorage = new RaventEventStorage(new ServerStoreFactory(), eventBus);
//#endif
			return eventStorage;
		}
	}
}
