using Shaheda.Enums;

namespace Shaheda.ViewModels.Employees
{
    public class EmployeeWorkViewModel
    {
        public JobTitle JobTitle { get; set; }

        public Department Department { get; set; }

        public string TimecardNo { get; set; }

        public Unit Unit { get; set; }

        public Shift Shift { get; set; }

        public Hour Hour { get; set; }
    }
}