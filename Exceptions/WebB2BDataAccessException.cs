namespace Oma.DAL.Nh.Exceptions
{
    using System;

    public class WebB2BDataAccessException : ApplicationException
    {
        public WebB2BDataAccessException()
            : base()
        {
        }
        public WebB2BDataAccessException(string message)
            : base(message)
        {
        }
        public WebB2BDataAccessException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}