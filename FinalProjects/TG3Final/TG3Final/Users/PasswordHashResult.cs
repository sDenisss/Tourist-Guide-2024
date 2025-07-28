using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TG.Users
{
    public class PasswordHashResult
    {
        public string Hash { get; set; }
        public string Salt { get; set; }
    }

}