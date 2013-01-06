using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Configuration;
using XboxChallenge.ViewModels;
using XboxChallenge.Models;

namespace XboxChallenge.Controllers
{
    public class HomeController : Controller
    {
        private GameLibrary _library;
        /*
         * Backend work for the Index page. Gets a list of all games on the "Want It" list
         * and then orders them by their votes, highest to lowest
         */
        public ActionResult Index()
        {
            _library = new GameLibrary();
            IEnumerable<Game> desiredGames = _library.GetGamesByStatus("wantit");
            ViewBag.DesiredGames = desiredGames;
            ViewBag.GameCount = desiredGames.Count();
            return View();
        }

        /*
         * Request to vote for a game. Done in a Post request so the method can be called
         * by an AJAX function, which is done in the Index.cshtml file.
         */
        [HttpPost]
        public JsonResult VoteForGame(int gameId)
        {
            _library = new GameLibrary();
            VoteResultViewModel model = new VoteResultViewModel();
            DateTime lastVote = new DateTime();
            if (Request.Cookies["xboxChallengeUser"] != null)
            {
                DateTime.TryParse(Request.Cookies["xboxChallengeUser"].Values["lastVisit"], out lastVote);
            }
            if (lastVote.CompareTo(DateTime.Today) < 0 && 
                (DateTime.Today.DayOfWeek != DayOfWeek.Saturday && DateTime.Today.DayOfWeek != DayOfWeek.Sunday))
            {

                model.Success = _library.VoteForGame(gameId);
                if (model.Success)
                {
                    model.Message = "Success, thanks for your vote!";
                    Response.Cookies["xboxChallengeUser"].Values["IP"] = Request.ServerVariables["REMOTE_ADDR"].ToString(); // Storing the IP for later, could do some secondary validation with this
                    Response.Cookies["xboxChallengeUser"].Values["lastVisit"] = DateTime.Today.ToString(); 
                    Response.Cookies["xboxChallengeUser"].Expires = DateTime.Today.AddDays(4); // Give the cookie 4 days to live, longest need Fri - Monday
                }
                else
                {
                    model.Message = "Unable to add vote";
                }
                
            }
            else if (DateTime.Today.DayOfWeek == DayOfWeek.Saturday || DateTime.Today.DayOfWeek == DayOfWeek.Sunday)
            {
                model.Success = false;
                model.Message = "It is the weekend, go home and relax!";
            }
            else
            {
                model.Success = false;
                model.Message = "You have already voted for a game or added a game, please come back tomorrow";
            }
            return Json(model);
        }

        /*
         * Request to add a game. Done through a POST call to so the method can be called
         * by an AJAX function, which is done in the Index.cshtml file.
         */
        [HttpPost]
        public JsonResult AddGame(string gameName)
        {
            _library = new GameLibrary();
            AddGameResultViewModel model = new AddGameResultViewModel();
            if (String.IsNullOrEmpty(gameName)) // DOuble check here to make sure that the game name isn't empty
            {
                model.Success = false;
                model.Message = "Please enter a valid game name";
                return Json(model);
            }
            DateTime lastVote = new DateTime();
            if (Request.Cookies["xboxChallengeUser"] != null)
            {
                DateTime.TryParse(Request.Cookies["xboxChallengeUser"].Values["lastVisit"], out lastVote);
            }
            if (lastVote.CompareTo(DateTime.Today) < 0) //&& (DateTime.Today.DayOfWeek != DayOfWeek.Saturday && DateTime.Today.DayOfWeek != DayOfWeek.Sunday) Removed for testing purposes
            {
                if (!_library.CheckIfGameExists(gameName))
                {
                    model.Success = _library.AddGame(gameName);
                    if (model.Success)
                    {
                        model.Message = String.Format("Success, {0} has been added to the desired game list.", gameName);
                        Response.Cookies["xboxChallengeUser"].Values["IP"] = Request.ServerVariables["REMOTE_ADDR"].ToString(); // Storing the IP for later, could do some secondary validation with this
                        Response.Cookies["xboxChallengeUser"].Values["lastVisit"] = DateTime.Today.ToString();
                        Response.Cookies["xboxChallengeUser"].Expires = DateTime.Today.AddDays(4); // Give the cookie 4 days to live, longest need Fri - Monday
                    }
                    else
                    {
                        model.Message = "Unable to add game to desired list.";
                    }
                }
                else
                {
                    model.Success = false;
                    model.Message = "Game already exists";
                }
            }
            else if (DateTime.Today.DayOfWeek == DayOfWeek.Saturday || DateTime.Today.DayOfWeek == DayOfWeek.Sunday)
            {
                model.Success = false;
                model.Message = "It is the weekend, go home and relax!";
            }
            else
            {
                model.Success = false;
                model.Message = "You have already voted for a game or added a game, please come back tomorrow";
            }
            return Json(model);
        }

        /*
         * Backend work for the Approve Games page. Gets a list of all games on the "Want It" list
         * and then orders them by their votes, highest to lowest.
         */
        public ActionResult SelectGame()
        {
            _library = new GameLibrary();
            IEnumerable<Game> desiredGames = _library.GetGamesByStatus("wantit").OrderByDescending(game => game.Votes);
            ViewBag.DesiredGames = desiredGames;
            ViewBag.GameCount = desiredGames.Count();
            return View();
        }

        /*
         * Request to add a game to the collection of owned games. 
         * Done through a POST call to so the method can be called
         * by an AJAX function, which is done in the SelectGames.cshtml file.
         */
        [HttpPost]
        public JsonResult AddGameToCollection(int gameId)
        {
            _library = new GameLibrary();
            AddGameToCollectionResultViewModel model = new AddGameToCollectionResultViewModel();
            model.Success = _library.SetGameStatus(gameId, "gotit");
            if (model.Success)
            {
                model.Message = "Game has been added to the collection";
            }
            else
            {
                model.Message = "Unable to add game to collection";
            }
            return Json(model);
        }

        /*
         * Backend work for the Owned Games page. Gets a list of all games on the "Got It" list
         * and then orders them by their votes, highest to lowest.
         */
        public ActionResult OwnedGames()
        {
            _library = new GameLibrary();
            IEnumerable<Game> ownedGames = _library.GetGamesByStatus("gotit").OrderByDescending(game => game.Votes);
            ViewBag.OwnedGames = ownedGames;
            ViewBag.GameCount = ownedGames.Count();
            return View();
        }
    }
}
