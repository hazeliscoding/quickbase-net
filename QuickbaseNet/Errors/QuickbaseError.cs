namespace QuickbaseNet.Errors
{
    /// <summary>
    /// Represents an error in the QuickBase API.
    /// </summary>
    public class QuickbaseError
    {
        /// <summary>
        /// Represents no error.
        /// </summary>
        public static readonly QuickbaseError None = new QuickbaseError(string.Empty, string.Empty, string.Empty, QuickbaseErrorType.Failure);

        /// <summary>
        /// Represents an error indicating a null value was provided.
        /// </summary>
        public static readonly QuickbaseError NullValue = new QuickbaseError("Error.NullValue", "Null value was provided", string.Empty, QuickbaseErrorType.Failure);

        /// <summary>
        /// Initializes a new instance of the QuickbaseError class.
        /// </summary>
        /// <param name="code">The error code.</param>
        /// <param name="message">The error message.</param>
        /// <param name="description">The error description.</param>
        /// <param name="quickbaseErrorType">The type of the error.</param>
        public QuickbaseError(string code, string message, string description, QuickbaseErrorType quickbaseErrorType)
        {
            Code = code;
            Message = message;
            Description = description;
            Type = quickbaseErrorType;
        }

        /// <summary>
        /// Gets the error code.
        /// </summary>
        public string Code { get; private set; }

        /// <summary>
        /// Gets the error message.
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Gets the error description.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Gets the type of the error.
        /// </summary>
        public QuickbaseErrorType Type { get; private set; }

        /// <summary>
        /// Creates a new QuickbaseError representing a not found error.
        /// </summary>
        /// <param name="code">The error code.</param>
        /// <param name="message">The error message.</param>
        /// <param name="description">The error description.</param>
        /// <returns>A QuickbaseError representing a not found error.</returns>
        public static QuickbaseError NotFound(string code, string message, string description) =>
            new QuickbaseError(code, message, description, QuickbaseErrorType.NotFound);

        /// <summary>
        /// Creates a new QuickbaseError representing a failure error.
        /// </summary>
        /// <param name="code">The error code.</param>
        /// <param name="message">The error message.</param>
        /// <param name="description">The error description.</param>
        /// <returns>A QuickbaseError representing a failure error.</returns>
        public static QuickbaseError Failure(string code, string message, string description) =>
            new QuickbaseError(code, message, description, QuickbaseErrorType.Failure);

        /// <summary>
        /// Creates a new QuickbaseError representing a client error.
        /// </summary>
        /// <param name="code">The error code.</param>
        /// <param name="message">The error message.</param>
        /// <param name="description">The error description.</param>
        /// <returns>A QuickbaseError representing a client error.</returns>
        public static QuickbaseError ClientError(string code, string message, string description) =>
            new QuickbaseError(code, message, description, QuickbaseErrorType.ClientError);

        /// <summary>
        /// Creates a new QuickbaseError representing a server error.
        /// </summary>
        /// <param name="code">The error code.</param>
        /// <param name="message">The error message.</param>
        /// <param name="description">The error description.</param>
        /// <returns>A QuickbaseError representing a server error.</returns>
        public static QuickbaseError ServerError(string code, string message, string description) =>
            new QuickbaseError(code, message, description, QuickbaseErrorType.ServerError);
    }
}
