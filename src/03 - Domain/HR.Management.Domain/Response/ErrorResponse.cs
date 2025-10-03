namespace HR.Management.Domain.Response
{
    public class ErrorResponse
    {
        public List<string> ErrorMessage { get; set; }
        public ErrorResponse(string errorMessage)
        {
            ErrorMessage = [errorMessage];
        }

        public ErrorResponse(List<string> errorMessage)
        {
            ErrorMessage = errorMessage ?? [];
        }
    }
}
