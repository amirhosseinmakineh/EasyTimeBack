using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EasyTime.Utilities.Convertor
{
    public record Error(string Code, string Message)
    {
        public static Error None = new(string.Empty, string.Empty);
        public static Error NullValue = new("Error.NullValue", "Um valor nulo foi fornecido.");
    }

    public class Result<T>
    {
        protected Result(bool isSuccess, Error error)
        {
            switch (isSuccess)
            {
                case true when error != Error.None:
                    throw new InvalidOperationException();

                case false when error == Error.None:
                    throw new InvalidOperationException();

                default:
                    IsSuccess = isSuccess;
                    Error = error;
                    break;
            }
        }

        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public Error Error { get; }

        public static Result<T> Success() => new(true, Error.None);
        public static Result<T> Failure(Error error) => new(false, error);
        public static Result<T> Failure(string message, Error? error) => new(false, error);

        public static Result<T> Success<T>(T value,string message) => new(true, Error.None);
        public static Result<T> Failure<T>(T value,Error error) => new(false, error);

        public static Result<T> Create<T>(T? value,string message) =>
            value is not null ? Success(value,message) : Failure<T>(value,Error.NullValue);
    }
}
