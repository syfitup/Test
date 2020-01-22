using System;

namespace SYF.Infrastructure.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException()
            : base("Object not found")
        {
        }

        public NotFoundException(string message)
            : base(message)
        {
        }
    }
}
