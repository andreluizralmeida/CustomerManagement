namespace CustomerManagement.Api.Extensions;

public class ApplicationErrorCollection
{
    public List<ApplicationError> Errors
    {
        get;
        set;
    }

    public ApplicationErrorCollection()
    {
        Errors = new List<ApplicationError>();
    }

    public ApplicationErrorCollection(params ApplicationError[] errors)
    {
        Errors = errors.ToList();
    }
}