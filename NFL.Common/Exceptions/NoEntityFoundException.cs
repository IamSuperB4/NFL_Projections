using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFL.Common.Exceptions
{
    public class NoEntityFoundException : NflException
    {
        public NoEntityFoundException(string msg) : base(msg)
        {

        }
    }
}
