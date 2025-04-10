namespace WebAPICommonPitfalls.Common.Utilities
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public T? Value { get; }
        public string? Error { get; }

        private Result(bool isSuccess, T? value, string? error) => (IsSuccess, Value, Error) = (isSuccess, value, error);

        public static Result<T> Success(T value) => new Result<T>(true, value, null);

        public static Result<T> Failure(string error) => new Result<T>(false, default(T), error);
    }
}