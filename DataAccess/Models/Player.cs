using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Player : BaseModel
    {
        public string Name { get; set; }

        public virtual ICollection<Round_Player> Round_Player { get; set; }

    }
}
