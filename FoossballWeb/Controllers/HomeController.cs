using System;
using System.Linq;
using System.Web.Mvc;
using Ankiro.Framework.Tools.Bus;
using FoossballPlayars.Commands;
using FoossballPlayars.QueryContext;
using FoossballWeb.Models;

namespace FoossballWeb.Controllers
{
    public class HomeController : Controller
	{
        private readonly IScoreQuery _scoreQuery;
        private readonly IBus _commandBus;

    	public HomeController(IScoreQuery scoreQuery, IBus commandBus)
		{
            _scoreQuery = scoreQuery;
            _commandBus = commandBus;
		}

        public ActionResult Index()
		{
			return View();
		}

		public ActionResult Register(string playarName)
		{
            _commandBus.Raise(new RegisterPlayarCommand(playarName));
			ViewBag.Message = "Player was registered";
			return RedirectToAction("Index");
		}

		public ActionResult About()
		{
			return View();
		}

		public ActionResult Celebrate()
		{
            return PartialView(new CelebrationViewModel { Scores = _scoreQuery.GetAllPlayers() });
		}

        [HttpPost]
        public ActionResult Celebrate(CelebrationViewModel model)
        {
            _commandBus.Raise(new PlayGameCommand(model.RedOffensive, model.RedDefensive, model.BlueOffensive, model.BlueDefensive, model.ScoreRed, model.ScoreBlue));
            return RedirectToAction("Index");
        }

	    public ActionResult Scores()
	    {
            return PartialView(_scoreQuery.GetTopPlayers());
	    }

		public ActionResult AllPlayers()
		{
			return View(_scoreQuery.GetAllPlayers());
		}
		
	    public ActionResult Activity()
	    {
            return PartialView(_scoreQuery.GetActivities());
	    }

        public ActionResult Timeline()
        {
            return PartialView(null);
        }

		//public ActionResult Pie()
		//{
		//    return PartialView(_scoreQuery.GetStatistics());
		//}

        public ActionResult AddPlayar()
        {
            return PartialView();
        }

        public ActionResult GetScoreHistory(Guid id)
        {
			return View(new PlayarDetailsViewModel
			            	{
			            		PlayarStatisistics = _scoreQuery.GetStatistics(id),
								MaxScore = _scoreQuery.GetHighestHistoricalScore(),
								MinScore= _scoreQuery.GetLowestHistoricalScore(),
			            	});
        }

		
        public ActionResult CurrentVinkekat()
        {
            return PartialView(_scoreQuery.CurrentVinkekat);
        }
	}
}
