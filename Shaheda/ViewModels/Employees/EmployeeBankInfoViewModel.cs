using Shaheda.Enums;

namespace Shaheda.ViewModels.Employees
{
    public class EmployeeBankInfoViewModel
    {
        public string BankName { get; set; }

        public SortCode SortCode { get; set; }

        public string AccountNo { get; set; }

        public AccountName AccountName { get; set; }

        public string Iban { get; set; }
    }
}