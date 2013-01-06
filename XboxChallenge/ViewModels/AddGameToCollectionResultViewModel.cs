using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XboxChallenge.ViewModels
{
    /*
     * I use these ViewModels to easily define what I want to be used in the View from the Controller.
     * Only a success flag and a message here, don't need anything fancy here.
     */
    public class AddGameToCollectionResultViewModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}