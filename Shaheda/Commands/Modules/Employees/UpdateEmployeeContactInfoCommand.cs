namespace Shaheda.Commands.Modules.Employees
{
    public class UpdateEmployeeContactInfoCommand : CommandBase
    {
        public int EmployeeNumber { get; set; }

        public EmployeeContactInfoCommand EmployeeContactInfo { get; set; }

        public EmployeeAddressInfoCommand EmployeeAddressInfo { get; set; }

        public EmployeeEmergencyContactInfoCommand EmployeeEmergencyContactInfo { get; set; }
    }
}