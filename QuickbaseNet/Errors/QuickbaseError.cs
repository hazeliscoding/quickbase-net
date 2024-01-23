namespace QuickbaseNet.Errors
{
    public class QuickbaseError
    {
        public static readonly QuickbaseError None = new QuickbaseError(string.Empty, string.Empty, string.Empty, QuickbaseErrorType.Failure);
        public static readonly QuickbaseError NullValue = new QuickbaseError("Error.NullValue", "Null value was provided", string.Empty, QuickbaseErrorType.Failure);

        public QuickbaseError(string code, string message, string description, QuickbaseErrorType quickbaseErrorType)
        {
            Code = code;
            Message = message;
            Description = description;
            Type = quickbaseErrorType;
        }

        public string Code { get; private set; }

        public string Message { get; private set; }

        public string Description { get; private set; }

        public QuickbaseErrorType Type { get; private set; }

        public static QuickbaseError NotFound(string code, string message, string description) =>
            new QuickbaseError(code, message, description, QuickbaseErrorType.NotFound);

        public static QuickbaseError Failure(string code, string message, string description) =>
            new QuickbaseError(code, message, description, QuickbaseErrorType.Failure);

        public static QuickbaseError ClientError(string code, string message, string description) =>
            new QuickbaseError(code, message, description, QuickbaseErrorType.ClientError);

        public static QuickbaseError ServerError(string code, string message, string description) =>
            new QuickbaseError(code, message, description, QuickbaseErrorType.ServerError);
    }
}