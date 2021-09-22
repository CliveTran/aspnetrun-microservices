using System;

namespace order.application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name) : base($"{name} was not found.") {  }
    }
}
