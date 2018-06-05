﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace DataAccess.Models
{
    public class Game : BaseModel
    {
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }

        //public ICollection<Round> Rounds { get; set; }
    }
}
