using System;
using Shaheda.Enums;

namespace Shaheda.Commands.Modules.Employees
{
    public class CreateEmployeePersonalInfoCommand : CommandBase
    {
        public string Name { get; set; }

        public string NickName { get; set; }

        public string PersonalNumber { get; set; }

        public string Surname { get; set; }

        public DateTime? Dob { get; set; }

        public EmployeeStatus Status { get; set; }

        public string PassportNumber { get; set; }

        public Titles Title { get; set; }

        public Gender Gender { get; set; }

        public SexualOrientation SexualOrientation { get; set; }

        public int OriginId { get; set; }

        public int LanguageId { get; set; }

        public int EducationId { get; set; }

        public int SkillId { get; set; }

        public int DegreeId { get; set; }
    }
}