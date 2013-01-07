using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace XboxChallenge.Models
{
    public class GameLibrary
    {
        private readonly string CONNSTRING = "";
        
        public bool AddGame(string gameTitle)
        {
            /*
             * Depending on how the database is setup, this will vary.
             * Using some standard stuff with Stored Procedures
             */
            using (var conn = new SqlConnection(CONNSTRING))
            using (var cmd = new SqlCommand("AddGame", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("GameTitle", gameTitle);
                cmd.Parameters.AddWithValue("Votes", 1);
                cmd.Parameters.AddWithValue("VoteDate", DateTime.Now);
                int gameId = cmd.ExecuteNonQuery();
                return gameId > 0;
            }
        }

        public IEnumerable<Game> GetAllGames()
        {
            /*
             * Depending on how the database is setup, this will vary.
             * Using some standard stuff with Stored Procedures
             */
            List<Game> games = new List<Game>();
            using (var conn = new SqlConnection(CONNSTRING))
            using (var cmd = new SqlCommand("GetAllGames", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Game game = new Game
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        IPAddress = dr["IpAddress"].ToString(),
                        Key = dr["Key"].ToString(),
                        Status = dr["Status"].ToString(),
                        Title = dr["Title"].ToString(),
                        Votes = Convert.ToInt32(dr["Votes"]),
                        VoteDate = Convert.ToDateTime(dr["VoteDate"])
                    };
                    games.Add(game);
                }
            }
            return games;
        }

        public IEnumerable<Game> GetGamesByStatus(string status)
        {
            /*
             * Depending on how the database is setup, this will vary.
             * Using some standard stuff with Stored Procedures
             */
            List<Game> games = new List<Game>();
            using (var conn = new SqlConnection(CONNSTRING))
            using (var cmd = new SqlCommand("GetGamesByStatus", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Status", status);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Game game = new Game
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        IPAddress = dr["IpAddress"].ToString(),
                        Key = dr["Key"].ToString(),
                        Status = dr["Status"].ToString(),
                        Title = dr["Title"].ToString(),
                        Votes = Convert.ToInt32(dr["Votes"]),
                        VoteDate = Convert.ToDateTime(dr["VoteDate"])
                    };
                    games.Add(game);
                }
            }
            return games;
        }

        public bool VoteForGame(int gameId)
        {
            /*
             * Depending on how the database is setup, this will vary.
             * Using some standard stuff with Stored Procedures
             */
            using (var conn = new SqlConnection(CONNSTRING))
            using (var cmd = new SqlCommand("VoteForGame", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("GameId", gameId);
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public bool CheckIfGameExists(string gameTitle)
        {
            using (var conn = new SqlConnection(CONNSTRING))
            using (var cmd = new SqlCommand("CheckIfGameExists", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Title", gameTitle);
                SqlDataReader dr = cmd.ExecuteReader();
                Game game = null;
                if (dr.Read())
                {
                    game = new Game
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        IPAddress = dr["IpAddress"].ToString(),
                        Key = dr["Key"].ToString(),
                        Status = dr["Status"].ToString(),
                        Title = dr["Title"].ToString(),
                        Votes = Convert.ToInt32(dr["Votes"]),
                        VoteDate = Convert.ToDateTime(dr["VoteDate"])
                    };
                }
                return game != null;
            }
        }

        public bool SetGameStatus(int gameId, string gameStatus)
        {
            /*
             * Depending on how the database is setup, this will vary.
             * Using some standard stuff with Stored Procedures
             */
            using (var conn = new SqlConnection(CONNSTRING))
            using (var cmd = new SqlCommand("SetGameStatus", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("GameId", gameId);
                cmd.Parameters.AddWithValue("GameStatus", gameStatus);
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
    }
}