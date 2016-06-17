namespace Oma.DAL.Nh.Exceptions
{
    using System;

    public class DataAccessException : ApplicationException
    {
        public DataAccessException()
            : base()
        {
        }
        public DataAccessException(string message)
            : base(message)
        {
        }
        public DataAccessException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
