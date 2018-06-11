using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
        public string ContactName { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

    }
}
