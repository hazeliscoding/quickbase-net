namespace QuickbaseNet.Errors
{
    /// <summary>
    /// Represents the type of QuickBase API error.
    /// </summary>
    public enum QuickbaseErrorType
    {
        /// <summary>
        /// Indicates a general failure error.
        /// </summary>
        Failure = 0,

        /// <summary>
        /// Indicates a not found error.
        /// </summary>
        NotFound = 1,

        /// <summary>
        /// Indicates a client error.
        /// </summary>
        ClientError = 3,

        /// <summary>
        /// Indicates a server error.
        /// </summary>
        ServerError = 4
    }
}