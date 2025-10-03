using System.Net;

namespace HR.Management.Domain.Exception
{
    public class InvalidLoginException : HrManagementException
    {
        public InvalidLoginException() : base(ErrorMessageResource.INVALID_EMAIL_OR_PASSWORD) { }

        public override int StatusCode => (int)HttpStatusCode.Unauthorized;

        public override List<string> GetErrors()
        {
            return [Message];
        }
    }
}
