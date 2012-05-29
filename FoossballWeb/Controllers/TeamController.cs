using System.Web.Mvc;
using FoossballPlayars.QueryContext.Teams;

namespace FoossballWeb.Controllers
{
	public class TeamController : Controller
	{
		private readonly ITeamService _teamService;

		public TeamController(ITeamService teamService)
		{
			_teamService = teamService;
		}

		public ActionResult Index()
		{
			return View(_teamService.GetAllTeams());
		}

		public ActionResult BestTeams()
		{
			return PartialView(_teamService.GetBestTeams());
		}
	}
}