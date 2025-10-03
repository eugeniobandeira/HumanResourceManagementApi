namespace HR.Management.Domain.Exception;
public abstract class HrManagementException(string message)
    : SystemException(message)
{
    public abstract int StatusCode { get; }

    public abstract List<string> GetErrors();
}
