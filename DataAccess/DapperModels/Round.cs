using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DapperModels
{
    public class Round : BaseModel
    {
        public Guid GameId { get; set; }
        public string Deck { get; set; }

    }
}
