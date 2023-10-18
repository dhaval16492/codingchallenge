namespace CodingChallenge.Common.CustomExceptions
{
    /// <summary>
    /// Represents a custom exception class that can be used for handling application-specific exceptions.
    /// </summary>
    public class CustomException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomException"/> class with no message.
        /// </summary>
        public CustomException()
        {
            // Default constructor with no message
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that describes the exception.</param>
        public CustomException(string message) : base(message)
        {
            // Constructor with a specified error message
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomException"/> class with a specified error message
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that describes the exception.</param>
        /// <param name="innerException">The inner exception that is the cause of this exception.</param>
        public CustomException(string message, Exception innerException) : base(message, innerException)
        {
            // Constructor with a specified error message and inner exception
        }
    }
}
