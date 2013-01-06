using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XboxChallenge.ViewModels
{
    /*
     * I use these ViewModels to easily define what I want to be used in the View from the Controller.
     * Only a success flag and a message here, don't need anything fancy here. Was going to use the 
     * GameTitle and GameID variables to add the game to the game list, but since the only way
     * for me to get the game ID is to pull in the entire list of games, this idea was scrapped.
     */
    public class AddGameResultViewModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string GameTitle { get; set; }
        public string GameID { get; set; }
    }
}