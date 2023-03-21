using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarzoneFanout.Domain;

namespace WarzoneFanout.Application.Query.Queries
{
    public class GetStatsByUsernameQuery
    {
        public GetStatsByUsernameQuery(string username, GameType gameType)
        {
            Username = username;
            GameType = gameType;
        }

        public string Username { get; set; }
        public GameType GameType { get; set; }
    }
}
