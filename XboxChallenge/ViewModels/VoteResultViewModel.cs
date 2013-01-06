using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XboxChallenge.ViewModels
{
    /*
     * I use these ViewModels to easily define what I want to be used in the View from the Controller.
     * I was hoping to be able to pull in the correct amount of votes when a user submitted their vote
     * but there's no good way to pull in the vote count from one game. Only way is to pull in the 
     * entire list of games, which is far too resource intensive.
     */
    public class VoteResultViewModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int VoteCount { get; set; }
    }
}