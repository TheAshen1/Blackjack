using Dapper.Contrib.Extensions;
using System;

namespace DataAccess.DapperModels
{
    public class BaseModel
    {
        [ExplicitKey]
        public Guid Id { get; set; }
    }
}
