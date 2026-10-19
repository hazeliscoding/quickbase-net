using System;
using QuickbaseNet.Errors;

namespace QuickbaseNet
{
    /// <summary>
    /// Represents the outcome of a Quickbase operation.
    /// </summary>
    public class QuickbaseResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QuickbaseResult"/> class.
        /// </summary>
        /// <param name="isSuccess">Whether the operation succeeded.</param>
        /// <param name="quickbaseError">The error associated with a failed operation, or <see cref="QuickbaseError.None"/> on success.</param>
        /// <exception cref="ArgumentException">The success state and error value are inconsistent.</exception>
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

        /// <summary>
        /// Gets a value indicating whether the operation succeeded.
        /// </summary>
        public bool IsSuccess { get; }

        /// <summary>
        /// Gets a value indicating whether the operation failed.
        /// </summary>
        public bool IsFailure => !IsSuccess;

        /// <summary>
        /// Gets the error associated with the operation, or <see cref="QuickbaseError.None"/> when it succeeded.
        /// </summary>
        public QuickbaseError QuickbaseError { get; }

        /// <summary>
        /// Creates a successful result without a value.
        /// </summary>
        /// <returns>A successful result.</returns>
        public static QuickbaseResult Success() => new QuickbaseResult(true, QuickbaseError.None);

        /// <summary>
        /// Creates a successful result containing the specified value.
        /// </summary>
        /// <typeparam name="TValue">The type of the result value.</typeparam>
        /// <param name="value">The value to include in the result.</param>
        /// <returns>A successful result containing <paramref name="value"/>.</returns>
        public static QuickbaseResult<TValue> Success<TValue>(TValue value) => new QuickbaseResult<TValue>(value, true, QuickbaseError.None);

        /// <summary>
        /// Creates a failed result without a value.
        /// </summary>
        /// <param name="quickbaseError">The error that caused the operation to fail.</param>
        /// <returns>A failed result containing <paramref name="quickbaseError"/>.</returns>
        public static QuickbaseResult Failure(QuickbaseError quickbaseError) => new QuickbaseResult(false, quickbaseError);

        /// <summary>
        /// Creates a failed result with the specified value type.
        /// </summary>
        /// <typeparam name="TValue">The type of value the result would contain on success.</typeparam>
        /// <param name="quickbaseError">The error that caused the operation to fail.</param>
        /// <returns>A failed result containing <paramref name="quickbaseError"/>.</returns>
        public static QuickbaseResult<TValue> Failure<TValue>(QuickbaseError quickbaseError) => new QuickbaseResult<TValue>(default, false, quickbaseError);
    }

    /// <summary>
    /// Represents the outcome of a Quickbase operation that returns a value.
    /// </summary>
    /// <typeparam name="TValue">The type of value returned by a successful operation.</typeparam>
    public class QuickbaseResult<TValue> : QuickbaseResult
    {
        private readonly TValue _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuickbaseResult{TValue}"/> class.
        /// </summary>
        /// <param name="value">The value returned by a successful operation.</param>
        /// <param name="isSuccess">Whether the operation succeeded.</param>
        /// <param name="quickbaseError">The error associated with a failed operation, or <see cref="QuickbaseError.None"/> on success.</param>
        /// <exception cref="ArgumentException">The success state and error value are inconsistent.</exception>
        protected internal QuickbaseResult(TValue value, bool isSuccess, QuickbaseError quickbaseError)
            : base(isSuccess, quickbaseError)
        {
            _value = value;
        }

        /// <summary>
        /// Gets the value returned by a successful operation.
        /// </summary>
        /// <exception cref="InvalidOperationException">The result represents a failed operation.</exception>
        public TValue Value => IsSuccess
            ? _value
            : throw new InvalidOperationException("The value of a failure result can't be accessed.");

        /// <summary>
        /// Converts a value to a successful result, or a null value to a failed result.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A successful result containing <paramref name="value"/>, or a failed result with <see cref="QuickbaseError.NullValue"/> when <paramref name="value"/> is null.</returns>
        public static implicit operator QuickbaseResult<TValue>(TValue value) =>
            !(value == null) ? Success(value) : Failure<TValue>(QuickbaseError.NullValue);
    }

}
