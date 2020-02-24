using Shaheda.Enums;

namespace Shaheda.Commands.Modules.Employees
{
    public class EmployeeEmergencyContactInfoCommand : CommandBase
    {
        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public EmployeeRelation Relation { get; set; }
    }
}