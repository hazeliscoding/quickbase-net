using System;
using QuickbaseNet.Errors;

namespace QuickbaseNet
{
    public class QuickbaseResult
    {
        protected internal QuickbaseResult(bool isSuccess, QuickbaseError quickbaseError)
        {
            if (isSuccess && quickbaseError != QuickbaseError.None ||
                !isSuccess && quickbaseError == QuickbaseError.None)
            {
                throw new ArgumentException("Invalid error", nameof(quickbaseError));
            }

            IsSuccess = isSuccess;
            QuickbaseError = quickbaseError;
        }

        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;

        public QuickbaseError QuickbaseError { get; }

        public static QuickbaseResult Success() => new QuickbaseResult(true, QuickbaseError.None);

        public static QuickbaseResult<TValue> Success<TValue>(TValue value) => new QuickbaseResult<TValue>(value, true, QuickbaseError.None);

        public static QuickbaseResult Failure(QuickbaseError quickbaseError) => new QuickbaseResult(false, quickbaseError);

        public static QuickbaseResult<TValue> Failure<TValue>(QuickbaseError quickbaseError) => new QuickbaseResult<TValue>(default, false, quickbaseError);
    }

    public class QuickbaseResult<TValue> : QuickbaseResult
    {
        private readonly TValue _value;

        protected internal QuickbaseResult(TValue value, bool isSuccess, QuickbaseError quickbaseError)
            : base(isSuccess, quickbaseError)
        {
            _value = value;
        }

        public TValue Value => IsSuccess
            ? _value
            : throw new InvalidOperationException("The value of a failure result can't be accessed.");

        public static implicit operator QuickbaseResult<TValue>(TValue value) =>
            !(value == null) ? Success(value) : Failure<TValue>(QuickbaseError.NullValue);
    }

}