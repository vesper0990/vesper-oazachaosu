using System;

namespace Oazachaosu.Core
{
    public class User 
    {
        public long Id { get; set; }
        public string ApiKey { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public DateTime CreationDateTime { get; set; }
    }
}
