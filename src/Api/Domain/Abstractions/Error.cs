namespace DogShelterService.Api.Domain.Abstractions
{
    public class Error
    {

        public const string NotFoundCode = "NOT_FOUND";
        public const string BadRequestCode = "BAD_REQUEST";

        public string Code { get; private set; }
        public string Message { get; private set; }

        public Error(string code, string message)
        {
            Code = code;
            Message = message;
        }

    }
}
