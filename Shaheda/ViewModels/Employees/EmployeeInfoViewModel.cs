using System;
using Shaheda.Enums;
using Shaheda.ViewModels.Degrees;
using Shaheda.ViewModels.Educations;
using Shaheda.ViewModels.Languages;
using Shaheda.ViewModels.Origins;
using Shaheda.ViewModels.Skills;

namespace Shaheda.ViewModels.Employees
{
    public class EmployeeInfoViewModel
    {
        public int EmployeeNumber { get; set; }

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

        public  OriginViewModel Origin { get; set; }

        public  LanguageViewModel Language { get; set; }

        public  EducationViewModel Education { get; set; }

        public SkillViewModel EmployeeSkill { get; set; }

        public  DegreeViewModel EmployeeDegree { get; set; }

        public EmployeeAddressInfoViewModel EmployeeAddressInfo { get; set; }

        public EmployeeContactInfoViewModel EmployeeContactInfo { get; set; }

        public EmployeeEmergencyContactInfoViewModel EmployeeEmergencyContactInfo { get; set; }

        public EmployeeRecruitmentViewModel EmployeeRecruitment { get; set; }

        public EmployeePaymentViewModel EmployeePayment { get; set; }

        public EmployeeBankInfoViewModel EmployeeBankInfo { get; set; }

        public EmployeeWorkViewModel EmployeeWork { get; set; }
    }
}