namespace CustomerManagement.Domain.Common;

using FluentResults;

public static class Errors
{
    public const string NotFoundMetadata = "not.found";
    public const string AlreadyExistsMetadata = "already.exists";

    public static class General
    {
        public static Error NotFound(string message)
            => new Error(message).WithMetadata("type", NotFoundMetadata);

        public static Error AlreadyExists(string message)
            => new Error(message).WithMetadata("type", AlreadyExistsMetadata);
    }
}