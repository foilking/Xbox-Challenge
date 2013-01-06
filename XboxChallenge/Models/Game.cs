using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XboxChallenge.Models
{
    public class Game
    {
        private int _id;
        private string _ipAddress;
        private string _key;
        private string _status;
        private string _title;
        private int _votes = 0;
        private DateTime _voteDate;

        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                this._id = value;
            }
        }

        public string IPAddress
        {
            get
            {
                return _ipAddress;
            }
            set
            {
                this._ipAddress = value;
            }
        }

        public string Key
        {
            get
            {
                return _key;
            }
            set
            {
                this._key = value;
            }
        }

        public string Status
        {
            get
            {
                return _status;
            }

            set
            {
                this._status = value;
            }
        }

        public string Title
        {
            get
            {
                return _title;
            }

            set
            {
                this._title = value;
            }
        }

        public int Votes
        {
            get
            {
                return _votes;
            }
            set
            {
                this._votes = value;
            }
        }

        public DateTime VoteDate
        {
            get
            {
                return _voteDate;
            }
            set
            {
                this._voteDate = value;
            }
        }

        
    }
}