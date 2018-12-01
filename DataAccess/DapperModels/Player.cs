﻿using System;

namespace DataAccess.DapperModels
{
    public class Player : BaseModel
    {
        public string Name { get; set; }
        public bool IsBot { get; set; }
        public Guid GameId { get; set; }
        public int Chips { get; set; }
    }
}
