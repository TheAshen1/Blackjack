using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DapperModels
{
    public class RoundPlayer : BaseModel
    {
        public Guid PlayerId { get; set; }
        public Guid RoundId { get; set; }
        public int Bet { get; set; }
        public string Cards { get; set; }

    }
}
