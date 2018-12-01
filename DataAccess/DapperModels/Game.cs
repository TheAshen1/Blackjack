using System;

namespace DataAccess.DapperModels
{
    public class Game : BaseModel
    {
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
    }
}
