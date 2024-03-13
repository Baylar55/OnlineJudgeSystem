using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoCode.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string key, string objectName) : base
            ($"Queried object {objectName} was not found, Key: {key}") { }
    }
}
