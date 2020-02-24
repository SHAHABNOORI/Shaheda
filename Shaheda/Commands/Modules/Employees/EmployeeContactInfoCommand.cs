using Shaheda.Enums;

namespace Shaheda.Commands.Modules.Employees
{
    public class EmployeeContactInfoCommand : CommandBase
    {
        public EmployeeContactType ContactType { get; set; }
        
        public string PostalCode { get; set; }

        public string MobileNumber { get; set; }

        public string PhoneNumber { get; set; }

        public OkToContact OkToContact { get; set; }
    }
}