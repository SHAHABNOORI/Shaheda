using System;
using Shaheda.Enums;

namespace Shaheda.Commands.Modules.Employees
{
    public class EmployeeRecruitmentCommand : CommandBase
    {
        public DateTime? DateStarted { get; set; }

        public DateTime? DateLeft { get; set; }

        public string Reason { get; set; }

        public DateTime? DatePensionStarted { get; set; }

        public string InsuarenceNumber { get; set; }

        public TypeOfEmployment TypeOfEmployment { get; set; }

        public EmployeeRecruitmentRelationShip Relationship { get; set; }
    }
}