using System.Net;

namespace HR.Management.Domain.Exception
{
    public class ErrorOnValidationException(List<string> errorMessages) 
        : HrManagementException(string.Empty)
    {
        private readonly List<string> _errors = errorMessages;

        public override int StatusCode => (int)HttpStatusCode.BadRequest;

        public override List<string> GetErrors()
        {
            return _errors;
        }
    }
}
