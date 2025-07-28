using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TG.Users
{
    public interface IEnterSystem
    {
        public byte[] Combine(string password, byte[] salt);

    }
}