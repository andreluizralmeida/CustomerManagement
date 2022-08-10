namespace CustomerManagement.Domain.Common;

using FluentResults;

public static class Successes
{
    public const string NotFoundMetadata = "created";
    public const string AlreadyExistsMetadata = "no.content";

    public static class General
    {
        public static Success Created(string message)
            => new Success(message).WithMetadata("type", "created");

        public static Success NoContent()
            => new Success(string.Empty).WithMetadata("type", "no.content");
    }
}