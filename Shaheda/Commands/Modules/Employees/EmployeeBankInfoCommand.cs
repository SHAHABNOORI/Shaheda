using Shaheda.Enums;

namespace Shaheda.Commands.Modules.Employees
{
    public class EmployeeBankInfoCommand : CommandBase
    {
        public string BankName { get; set; }

        public SortCode SortCode { get; set; }

        public string AccountNo { get; set; }

        public AccountName AccountName { get; set; }

        public string Iban { get; set; }
    }
}