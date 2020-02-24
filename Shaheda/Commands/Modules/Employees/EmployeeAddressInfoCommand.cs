using Shaheda.Enums;

namespace Shaheda.Commands.Modules.Employees
{
    public class EmployeeAddressInfoCommand : CommandBase
    {
        public string AddressOne { get; set; }

        public string AddressTwo { get; set; }

        public Town Town { get; set; }

        public City City { get; set; }

        public string Email { get; set; }

        public string PostalCode { get; set; }
    }
}