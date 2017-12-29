using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class User 
    {
        public long Id { get; set; }
        public string ApiKey { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
