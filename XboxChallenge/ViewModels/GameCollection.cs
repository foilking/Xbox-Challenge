using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XboxChallenge.Models;

namespace XboxChallenge.ViewModels
{
    public class GameCollection
    {
        public IEnumerable<Game> Games { get; set; }
    }
}