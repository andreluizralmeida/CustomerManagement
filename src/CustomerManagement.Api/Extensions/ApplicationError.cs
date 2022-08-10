namespace CustomerManagement.Api.Extensions;

public class ApplicationError
{
    public ApplicationError()
    {
    }

    public ApplicationError(string code, string message)
        : this()
    {
        Code = code;
        Message = message;
    }

    public ApplicationError(string code, string message, string developerMessage)
        : this(code, message)
    {
        DeveloperMessage = developerMessage;
    }

    public ApplicationError(string code, string message, string developerMessage, Exception exception)
        : this(code, message, developerMessage)
    {
        Exception = exception;
    }

    public string Code { get; set; }

    public string Message { get; set; }

    public string DeveloperMessage { get; set; }

    public Exception Exception { get; set; }

    public string MoreInformation { get; set; }
}