using Shaheda.Enums;

namespace Shaheda.Commands.Modules.Employees
{
    public class EmployeePaymentCommand : CommandBase
    {
        public PaymentFrequency PaymentFrequency { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

    }
}