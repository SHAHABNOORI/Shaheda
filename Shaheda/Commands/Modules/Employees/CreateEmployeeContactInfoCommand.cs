namespace Shaheda.Commands.Modules.Employees
{
    public class CreateEmployeeContactInfoCommand : CommandBase
    {
        public int EmployeeNumber { get; set; }

        public EmployeeContactInfoCommand EmployeeContactInfo { get; set; }

        public EmployeeAddressInfoCommand EmployeeAddressInfo { get; set; }

        public EmployeeEmergencyContactInfoCommand EmployeeEmergencyContactInfo { get; set; }
    }
}