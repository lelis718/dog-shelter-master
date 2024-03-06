namespace DogShelterService.Api.Domain.Abstractions
{
    public class Result
    {
        public bool IsSuccess { get; private set; }
        public Error ErrorMessage { get; private set; }
        public Result(bool success)
        {
            this.IsSuccess = success;
        }
        public Result(bool success, Error error)
        {
            this.IsSuccess = success;
            this.ErrorMessage = error;
        }

        public static Result Success()
        {
            return new Result(true);
        }
        public static Result Error(string code, string message)
        {
            return new Result(false, new Error(code, message));
        }

        public static Result<T> Success<T>(T value)
        {
            return new Result<T>(value);
        }
        public static Result<T> Error<T>(string code, string message)
        {
            return new Result<T>(new Error(code, message));
        }

    }

    public class Result<T> : Result
    {
        public T Value { get; private set; }
        public Result(T value) : base(true)
        {
            this.Value = value;
        }
        public Result(Error error) : base(false, error)
        {
            this.Value = default;
        }



    }
}
