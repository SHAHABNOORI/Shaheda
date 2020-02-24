using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Shaheda.Commands;
using Shaheda.Commands.Modules.Employees;
using Shaheda.Enums;
using Shaheda.Helpers;
using Shaheda.ViewModels.Degrees;
using Shaheda.ViewModels.Educations;
using Shaheda.ViewModels.Employees;
using Shaheda.ViewModels.Languages;
using Shaheda.ViewModels.Origins;
using Shaheda.ViewModels.Skills;

namespace Shaheda
{
    /// <summary>
    /// Interaction logic for AdministrativeEmpolyment.xaml
    /// </summary>
    public partial class AdministrativeEmpolyment : Window
    {
        private readonly IEnumerable<OriginViewModel> _origins;
        private readonly IEnumerable<LanguageViewModel> _languages;
        private readonly IEnumerable<DegreeViewModel> _degrees;
        private readonly IEnumerable<EducationViewModel> _educations;
        private readonly IEnumerable<SkillViewModel> _skills;
        private EmployeeInfoViewModel _filteredEmployee;

        public AdministrativeEmpolyment()
        {
            try
            {
                _languages = RequestHelper.SendRequest<List<LanguageViewModel>>("http://localhost:5000/AllLanguages");
            }
            catch (Exception)
            {
                MessageBox.Show("We have a problem in Fetching List Of Languages", "Fetching Data");
            }

            try
            {
                _origins = RequestHelper.SendRequest<List<OriginViewModel>>("http://localhost:5000/AllOrigins");
            }

            catch (Exception)
            {
                MessageBox.Show("We have a problem in Fetching List Of Origins", "Fetching Data");
            }

            try
            {
                _degrees = RequestHelper.SendRequest<List<DegreeViewModel>>("http://localhost:5000/AllDegrees");
            }

            catch (Exception)
            {
                MessageBox.Show("We have a problem in Fetching List Of Degrees", "Fetching Data");
            }

            try
            {
                _skills = RequestHelper.SendRequest<List<SkillViewModel>>("http://localhost:5000/AllSkills");
            }

            catch (Exception)
            {
                MessageBox.Show("We have a problem in Fetching List Of Degrees", "Fetching Data");
            }

            try
            {
                _educations = RequestHelper.SendRequest<List<EducationViewModel>>("http://localhost:5000/AllEducations");
            }

            catch (Exception)
            {
                MessageBox.Show("We have a problem in Fetching List Of Degrees", "Fetching Data");
            }
            InitializeComponent();

        }

        private void ComboBoxTitle_Loaded(object sender, RoutedEventArgs e)
        {
            comboBoxTitle.ItemsSource = Enum.GetValues(typeof(Titles)).Cast<Titles>();
        }

        private void ComboBoxGender_Loaded(object sender, RoutedEventArgs e)
        {
            comboBoxGender.ItemsSource = Enum.GetValues(typeof(Gender)).Cast<Gender>();
        }

        private void ComboBoxStatus_Loaded(object sender, RoutedEventArgs e)
        {
            comboBoxStatus.ItemsSource = Enum.GetValues(typeof(EmployeeStatus)).Cast<EmployeeStatus>();
        }

        private void ComboBoxSexual_Loaded(object sender, RoutedEventArgs e)
        {
            comboBoxSexual.ItemsSource = Enum.GetValues(typeof(SexualOrientation)).Cast<SexualOrientation>();
        }
        private void ComboOrigin_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var origin in _origins)
            {
                comboOrigin.Items.Add(new CustomeComboboxItem()
                {
                    Value = origin.Id,
                    Text = origin.OriginName
                });
            }
            if (comboOrigin.Items.Count > 0)
                comboOrigin.SelectedIndex = 0;
        }

        private void ComboLanguage_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var language in _languages)
            {
                comboBoxLanguage.Items.Add(new CustomeComboboxItem()
                {
                    Value = language.Id,
                    Text = language.LanguageName
                });
            }
            if (comboBoxLanguage.Items.Count > 0)
                comboBoxLanguage.SelectedIndex = 0;
        }

        private void ComboDegree_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var degree in _degrees)
            {
                comboBoxDegree.Items.Add(new CustomeComboboxItem()
                {
                    Value = degree.Id,
                    Text = degree.DegreeName
                });
            }
            if (comboBoxDegree.Items.Count > 0)
                comboBoxDegree.SelectedIndex = 0;
        }

        private void ComboSkill_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var skill in _skills)
            {
                comboBoxSkills.Items.Add(new CustomeComboboxItem()
                {
                    Value = skill.Id,
                    Text = skill.SkillName
                });
            }
            if (comboBoxSkills.Items.Count > 0)
                comboBoxSkills.SelectedIndex = 0;
        }

        private void ComboEducation_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var education in _educations)
            {
                comboBoxEducation.Items.Add(new CustomeComboboxItem()
                {
                    Value = education.Id,
                    Text = education.EducationName
                });
            }
            if (comboBoxEducation.Items.Count > 0)
                comboBoxEducation.SelectedIndex = 0;
        }

        /// <summary>
        ///  ثبت اطلاعات پرسنلی کارمند
        /// Employee Personal Info Create 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddEmployeePersonalInfo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var command = MakeEmployeePersonalCreateCommand();
                var result = RequestHelper.PostCommand(command, "http://localhost:5000/EmployeePersonalInfo");
                MessageBox.Show($"{command.Name} {command.Surname} with {result} employee number successfully created", "Successfully Created");
                ClientEmployeePersonalInfoUpdateMode();
            }
            catch (Exception)
            {
                MessageBox.Show("Please Fill All Data Correctly", "InCorrect Input");
            }
        }

        private CreateEmployeePersonalInfoCommand MakeEmployeePersonalCreateCommand()
        {
            var command = new CreateEmployeePersonalInfoCommand()
            {
                DegreeId = (int)((CustomeComboboxItem)comboBoxDegree.SelectedValue).Value,
                Dob = employeeDobSelector.SelectedDate,
                EducationId = (int)((CustomeComboboxItem)comboBoxEducation.SelectedValue).Value,
                SexualOrientation = (SexualOrientation)comboBoxSexual.SelectedValue,
                Status = (EmployeeStatus)comboBoxStatus.SelectedValue,
                Title = (Titles)comboBoxTitle.SelectedValue,
                Gender = (Gender)comboBoxGender.SelectedValue,
                Surname = txtSurname.Text,
                LanguageId = (int)((CustomeComboboxItem)comboBoxLanguage.SelectedValue).Value,
                Name = txtName.Text,
                NickName = txtNickname.Text,
                OriginId = (int)((CustomeComboboxItem)comboOrigin.SelectedValue).Value,
                PassportNumber = txtPassportNumber.Text,
                PersonalNumber = txtPersonalNumber.Text,
                SkillId = (int)((CustomeComboboxItem)comboBoxSkills.SelectedValue).Value
            };
            return command;
        }

        private void ClientEmployeePersonalInfoUpdateMode()
        {
            btnAddEmployeePersonalInfo.Visibility = Visibility.Hidden;
            btnAddEmployeePersonalInfo.IsEnabled = false;
            btnUpdateEmployeePersonalInfo.Visibility = Visibility.Visible;
            btnUpdateEmployeePersonalInfo.IsEnabled = true;
        }

        private void ClientEmployeePersonalInfoNewMode()
        {
            btnAddEmployeePersonalInfo.Visibility = Visibility.Visible;
            btnAddEmployeePersonalInfo.IsEnabled = true;
            btnUpdateEmployeePersonalInfo.Visibility = Visibility.Hidden;
            btnUpdateEmployeePersonalInfo.IsEnabled = false;
        }

        /// <summary>
        /// فیلتر کردن کارمند مورد نظر با استفاده از شناسه کارمند
        /// Filter Employee by Employee Number
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFilterEmployee_Click(object sender, RoutedEventArgs e)
        {
            var id = txtEmployeeNumberFilter.Text;
            try
            {
                _filteredEmployee = RequestHelper.SendRequest<EmployeeInfoViewModel>("http://localhost:5000/GetEmployeeInfoByEmployeeNumber?id=" + id);
            }
            catch (Exception)
            {
                MessageBox.Show("We have a problem in Fetching Employee Data", "Fetching Data");
                return;
            }
            BindPersonalInfoToForm();
            ClientEmployeePersonalInfoUpdateMode();

            if (_filteredEmployee.EmployeeRecruitment != null)
            {
                ClientEmployeeRecruitmentInfoUpdateMode();
            }

            if (_filteredEmployee.EmployeeContactInfo != null)
            {
                ClientEmployeeContactInfoUpdateMode();
            }

        }

        private void BindPersonalInfoToForm()
        {
            ClearForm();
            comboBoxTitle.SelectedValue = _filteredEmployee.Title;
            comboBoxSexual.SelectedValue = _filteredEmployee.SexualOrientation;
            comboBoxGender.SelectedValue = _filteredEmployee.Gender;
            comboBoxStatus.SelectedValue = _filteredEmployee.Status;
            txtName.Text = _filteredEmployee.Name;
            txtSurname.Text = _filteredEmployee.Surname;
            txtNickname.Text = _filteredEmployee.NickName;
            txtPassportNumber.Text = _filteredEmployee.PassportNumber;
            txtPersonalNumber.Text = _filteredEmployee.PersonalNumber;
            comboOrigin.SelectedItem = GetComboItem(comboOrigin, _filteredEmployee.Origin.Id);
            comboBoxDegree.SelectedItem = GetComboItem(comboBoxDegree, _filteredEmployee.EmployeeDegree.Id);
            employeeDobSelector.SelectedDate = _filteredEmployee.Dob;
            comboBoxEducation.SelectedItem = GetComboItem(comboBoxEducation, _filteredEmployee.Education.Id);
            comboBoxLanguage.SelectedItem = GetComboItem(comboBoxLanguage, _filteredEmployee.Language.Id);
            comboBoxSkills.SelectedItem = GetComboItem(comboBoxSkills, _filteredEmployee.EmployeeSkill.Id);
            if (_filteredEmployee.EmployeeAddressInfo != null)
            {
                txtEmployeeAddressInfoAddreessOne.Text = _filteredEmployee.EmployeeAddressInfo.AddressOne;
                comboBoxEmployeeAddressInfoCity.SelectedValue = _filteredEmployee.EmployeeAddressInfo.City;
                txtEmployeeAddressInfoAddreessTwo.Text = _filteredEmployee.EmployeeAddressInfo.AddressTwo;
                txtEmployeeAddressInfoEmail.Text = _filteredEmployee.EmployeeAddressInfo.Email;
                txtEmployeeAddressInfoPostalCode.Text = _filteredEmployee.EmployeeAddressInfo.PostalCode;
                comboBoxEmployeeAddressInfoTown.SelectedValue = _filteredEmployee.EmployeeAddressInfo.Town;
            }

            if (_filteredEmployee.EmployeeContactInfo != null)
            {
                txtEmployeeContactInfoPostalCode.Text = _filteredEmployee.EmployeeContactInfo.PostalCode;
                comboBoxEmployeeContactInfoContactType.SelectedValue = _filteredEmployee.EmployeeContactInfo.ContactType;
                txtEmployeeContactInfoMobileNumber.Text = _filteredEmployee.EmployeeContactInfo.MobileNumber;
                comboBoxEmployeeContactInfoOKToContact.SelectedValue = _filteredEmployee.EmployeeContactInfo.OkToContact;
                txtEmployeeContactInfoPhoneNumber.Text = _filteredEmployee.EmployeeContactInfo.PhoneNumber;
            }

            if (_filteredEmployee.EmployeeEmergencyContactInfo != null)
            {
                txtEmergencyContactPhoneNumber.Text = _filteredEmployee.EmployeeEmergencyContactInfo.PhoneNumber;
                txtEmergencyContactName.Text = _filteredEmployee.EmployeeEmergencyContactInfo.Name;
                comboboxEmergencyContactRelationship.SelectedValue = _filteredEmployee.EmployeeEmergencyContactInfo.Relation;
            }

            if (_filteredEmployee.EmployeeBankInfo != null)
            {
                comboBoxSortCode.SelectedValue = _filteredEmployee.EmployeeBankInfo.SortCode;
                comboBoxAccountName.SelectedValue = _filteredEmployee.EmployeeBankInfo.AccountName;
                txtAccountNo.Text = _filteredEmployee.EmployeeBankInfo.AccountNo;
                txtBankBame.Text = _filteredEmployee.EmployeeBankInfo.BankName;
                txtIBAN.Text = _filteredEmployee.EmployeeBankInfo.Iban;
            }

            if (_filteredEmployee.EmployeePayment != null)
            {
                comboBoxPaymentMethod.SelectedValue = _filteredEmployee.EmployeePayment.PaymentMethod;
                comboBoxPayFrequency.SelectedValue = _filteredEmployee.EmployeePayment.PaymentFrequency;
            }

            if (_filteredEmployee.EmployeeRecruitment != null)
            {
                dateLeftSelector.SelectedDate = _filteredEmployee.EmployeeRecruitment.DateLeft;
                datePensionSelector.SelectedDate = _filteredEmployee.EmployeeRecruitment.DatePensionStarted;
                dateStartedSelector.SelectedDate = _filteredEmployee.EmployeeRecruitment.DateStarted;
                txtInsuranceNuber.Text = _filteredEmployee.EmployeeRecruitment.InsuarenceNumber;
                txtReaseon.Text = _filteredEmployee.EmployeeRecruitment.Reason;
                comboBoxReruitmentRelaionship.SelectedValue = _filteredEmployee.EmployeeRecruitment.Relationship;
                comboBoxTypeOfEmpolyment.SelectedValue = _filteredEmployee.EmployeeRecruitment.TypeOfEmployment;
            }

            if (_filteredEmployee.EmployeeWork != null)
            {
                comboBoxShift.SelectedValue = _filteredEmployee.EmployeeWork.Shift;
                comboBoxUnit.SelectedValue = _filteredEmployee.EmployeeWork.Unit;
                comboBoxDepartment.SelectedValue = _filteredEmployee.EmployeeWork.Department;
                comboBoxJobTitle.SelectedValue = _filteredEmployee.EmployeeWork.JobTitle;
                comboBoxHours.SelectedValue = _filteredEmployee.EmployeeWork.Hour;
                txtTimecardNo.Text = _filteredEmployee.EmployeeWork.TimecardNo;
            }

        }

        private void ClearForm()
        {
            comboBoxTitle.SelectedValue = "";
            comboBoxSexual.SelectedValue = null;
            comboBoxGender.SelectedValue = null;
            comboBoxStatus.SelectedValue = null;
            txtName.Text = "";
            txtSurname.Text = "";
            txtNickname.Text = "";
            txtPassportNumber.Text = "";
            txtPersonalNumber.Text = "";
            comboOrigin.SelectedItem = null;
            comboBoxDegree.SelectedItem = null;
            employeeDobSelector.SelectedDate = null;
            comboBoxEducation.SelectedItem = null;
            comboBoxLanguage.SelectedItem = null;
            comboBoxSkills.SelectedItem = null;
            txtEmployeeAddressInfoAddreessOne.Text = "";
            comboBoxEmployeeAddressInfoCity.SelectedValue = null;
            txtEmployeeAddressInfoAddreessTwo.Text = "";
            txtEmployeeAddressInfoEmail.Text = "";
            txtEmployeeAddressInfoPostalCode.Text = "";
            comboBoxEmployeeAddressInfoTown.SelectedValue = null;
            txtEmployeeContactInfoPostalCode.Text = _filteredEmployee.EmployeeContactInfo.PostalCode;
            comboBoxEmployeeContactInfoContactType.SelectedValue = null;
            txtEmployeeContactInfoMobileNumber.Text = _filteredEmployee.EmployeeContactInfo.MobileNumber;
            comboBoxEmployeeContactInfoOKToContact.SelectedValue = null;
            txtEmployeeContactInfoPhoneNumber.Text = "";
            txtEmergencyContactPhoneNumber.Text = "";
            txtEmergencyContactName.Text = "";
            comboboxEmergencyContactRelationship.SelectedValue = null;
            comboBoxSortCode.SelectedValue = null;
            comboBoxAccountName.SelectedValue = null;
            txtAccountNo.Text = "";
            txtBankBame.Text = "";
            txtIBAN.Text = "";
            comboBoxPaymentMethod.SelectedValue = null;
            comboBoxPayFrequency.SelectedValue = null;
            dateLeftSelector.SelectedDate = null;
            datePensionSelector.SelectedDate = null;
            dateStartedSelector.SelectedDate = null;
            txtInsuranceNuber.Text = "";
            txtReaseon.Text = "";
            comboBoxReruitmentRelaionship.SelectedValue = null;
            comboBoxTypeOfEmpolyment.SelectedValue = null;
            comboBoxShift.SelectedValue = null;
            comboBoxUnit.SelectedValue = null;
            comboBoxDepartment.SelectedValue = null;
            comboBoxJobTitle.SelectedValue = null;
            comboBoxHours.SelectedValue = null;
            txtTimecardNo.Text = "";

            ClientEmployeePersonalInfoNewMode();
            ClientEmployeeRecruitmentInfoNewMode();
            ClientEmployeeContactInfoNewMode();
        }

        private void BtnClearForm_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
        }

        private CustomeComboboxItem GetComboItem<T>(ComboBox comboBox, T value)
        {
            if (comboBox?.Items == null)
                return null;

            foreach (var comboItem in comboBox.Items)
            {
                if (!(comboItem is CustomeComboboxItem item && item.Value is T itemValue))
                    return null;

                if (itemValue.Equals(value))
                    return item;
            }

            return null;
        }

        private void BtnUpdateEmployeePersonalInfo_Click(object sender, RoutedEventArgs e)
        {
            if (CheckForFilteredEmployee()) return;
            try
            {
                var command = MakeEmployeePersonalInfoUpdateCommand();
                var result = RequestHelper.PutCommand(command, "http://localhost:5000/EmployeePersonalInfo");
                MessageBox.Show("Employee Personal Info Successfully Has Been Updated", "Updated");
            }
            catch (Exception)
            {
                MessageBox.Show("Please Fill All Data Correctly", "InCorrect Input");
            }
        }

        private CommandBase MakeEmployeePersonalInfoUpdateCommand()
        {
            var command = new UpdateEmployeePersonalInfoCommand()
            {
                EmployeeNumber = _filteredEmployee.EmployeeNumber,
                DegreeId = (int)((CustomeComboboxItem)comboBoxDegree.SelectedValue).Value,
                Dob = employeeDobSelector.SelectedDate,
                EducationId = (int)((CustomeComboboxItem)comboBoxEducation.SelectedValue).Value,
                SexualOrientation = (SexualOrientation)comboBoxSexual.SelectedValue,
                Status = (EmployeeStatus)comboBoxStatus.SelectedValue,
                Title = (Titles)comboBoxTitle.SelectedValue,
                Gender = (Gender)comboBoxGender.SelectedValue,
                Surname = txtSurname.Text,
                LanguageId = (int)((CustomeComboboxItem)comboBoxLanguage.SelectedValue).Value,
                Name = txtName.Text,
                NickName = txtNickname.Text,
                OriginId = (int)((CustomeComboboxItem)comboOrigin.SelectedValue).Value,
                PassportNumber = txtPassportNumber.Text,
                PersonalNumber = txtPersonalNumber.Text,
                SkillId = (int)((CustomeComboboxItem)comboBoxSkills.SelectedValue).Value
            };
            return command;
        }

        private bool CheckForFilteredEmployee()
        {
            if (_filteredEmployee != null) return false;
            MessageBox.Show("Please First Filter your employee", "Filter Employee");
            return true;
        }

        private void BtnAddEmployeeContactInfo_Click(object sender, RoutedEventArgs e)
        {
            if (CheckForFilteredEmployee()) return;
            try
            {
                var command = new CreateEmployeeContactInfoCommand()
                {
                    EmployeeNumber = _filteredEmployee.EmployeeNumber,
                    EmployeeAddressInfo = new EmployeeAddressInfoCommand()
                    {
                        AddressOne = txtEmployeeAddressInfoAddreessOne.Text,
                        City = (City)comboBoxEmployeeAddressInfoCity.SelectedValue,
                        AddressTwo = txtEmployeeAddressInfoAddreessTwo.Text,
                        Email = txtEmployeeAddressInfoEmail.Text,
                        PostalCode = txtEmployeeAddressInfoPostalCode.Text,
                        Town = (Town)comboBoxEmployeeAddressInfoTown.SelectedValue
                    },
                    EmployeeContactInfo = new EmployeeContactInfoCommand()
                    {
                        PostalCode = txtEmployeeContactInfoPostalCode.Text,
                        ContactType = (EmployeeContactType)comboBoxEmployeeContactInfoContactType.SelectedValue,
                        MobileNumber = txtEmployeeContactInfoMobileNumber.Text,
                        OkToContact = (OkToContact)comboBoxEmployeeContactInfoOKToContact.SelectedValue,
                        PhoneNumber = txtEmployeeContactInfoPhoneNumber.Text
                    },
                    EmployeeEmergencyContactInfo = new EmployeeEmergencyContactInfoCommand()
                    {
                        PhoneNumber = txtEmergencyContactPhoneNumber.Text,
                        Name = txtEmergencyContactName.Text,
                        Relation = (EmployeeRelation)comboboxEmergencyContactRelationship.SelectedValue
                    }
                };
                var result = RequestHelper.PostCommand(command, "http://localhost:5000/EmployeeContactInfo");
                MessageBox.Show("Employee Contact Info Successfully Has Been Updated", "Successfully Updated");
                ClientEmployeeContactInfoUpdateMode();
            }
            catch (Exception)
            {
                MessageBox.Show("Please Fill All Data Correctly", "InCorrect Input");
            }
        }

        private void BtnUpdateEmployeeContactInfo_Click(object sender, RoutedEventArgs e)
        {
            if (CheckForFilteredEmployee()) return;
            try
            {
                var command = new UpdateEmployeeContactInfoCommand()
                {
                    EmployeeNumber = _filteredEmployee.EmployeeNumber,
                    EmployeeAddressInfo = new EmployeeAddressInfoCommand()
                    {
                        AddressOne = txtEmployeeAddressInfoAddreessOne.Text,
                        City = (City)comboBoxEmployeeAddressInfoCity.SelectedValue,
                        AddressTwo = txtEmployeeAddressInfoAddreessTwo.Text,
                        Email = txtEmployeeAddressInfoEmail.Text,
                        PostalCode = txtEmployeeAddressInfoPostalCode.Text,
                        Town = (Town)comboBoxEmployeeAddressInfoTown.SelectedValue
                    },
                    EmployeeContactInfo = new EmployeeContactInfoCommand()
                    {
                        PostalCode = txtEmployeeContactInfoPostalCode.Text,
                        ContactType = (EmployeeContactType)comboBoxEmployeeContactInfoContactType.SelectedValue,
                        MobileNumber = txtEmployeeContactInfoMobileNumber.Text,
                        OkToContact = (OkToContact)comboBoxEmployeeContactInfoOKToContact.SelectedValue,
                        PhoneNumber = txtEmployeeContactInfoPhoneNumber.Text
                    },
                    EmployeeEmergencyContactInfo = new EmployeeEmergencyContactInfoCommand()
                    {
                        PhoneNumber = txtEmergencyContactPhoneNumber.Text,
                        Name = txtEmergencyContactName.Text,
                        Relation = (EmployeeRelation)comboboxEmergencyContactRelationship.SelectedValue
                    }
                };
                var result = RequestHelper.PutCommand(command, "http://localhost:5000/EmployeeContactInfo");
                MessageBox.Show("Employee Contact Info Successfully Has Been Updated", "Successfully Updated");
                ClientEmployeeContactInfoUpdateMode();
            }
            catch (Exception)
            {
                MessageBox.Show("Please Fill All Data Correctly", "InCorrect Input");
            }
        }

        private void ClientEmployeeContactInfoUpdateMode()
        {
            btnUpdateEmployeeContactInfo.IsEnabled = true;
            btnUpdateEmployeeContactInfo.Visibility = Visibility.Visible;
            btnAddEmployeeConatctInfo.IsEnabled = false;
            btnAddEmployeeConatctInfo.Visibility = Visibility.Hidden;
        }

        private void ClientEmployeeContactInfoNewMode()
        {
            btnUpdateEmployeeContactInfo.IsEnabled = false;
            btnUpdateEmployeeContactInfo.Visibility = Visibility.Hidden;
            btnAddEmployeeConatctInfo.IsEnabled = true;
            btnAddEmployeeConatctInfo.Visibility = Visibility.Visible;
        }

        private void ComboBoxEmployeeContactInfoContactType_Loaded(object sender, RoutedEventArgs e)
        {
            comboBoxEmployeeContactInfoContactType.ItemsSource = Enum.GetValues(typeof(EmployeeContactType)).Cast<EmployeeContactType>();
        }

        private void ComboBoxEmployeeAddressInfoCity_Loaded(object sender, RoutedEventArgs e)
        {
            comboBoxEmployeeAddressInfoCity.ItemsSource = Enum.GetValues(typeof(City)).Cast<City>();
        }

        private void ComboBoxEmployeeAddressInfoTown_Loaded(object sender, RoutedEventArgs e)
        {
            comboBoxEmployeeAddressInfoTown.ItemsSource = Enum.GetValues(typeof(Town)).Cast<Town>();
        }

        private void ComboBoxEmployeeContactInfoOKToContact_Loaded(object sender, RoutedEventArgs e)
        {
            comboBoxEmployeeContactInfoOKToContact.ItemsSource = Enum.GetValues(typeof(OkToContact)).Cast<OkToContact>();
        }

        private void ComboboxEmergencyContactRelationship_Loaded(object sender, RoutedEventArgs e)
        {
            comboboxEmergencyContactRelationship.ItemsSource = Enum.GetValues(typeof(EmployeeRelation)).Cast<EmployeeRelation>();
        }

        private void ComboBoxTypeOfEmpolyment_Loaded(object sender, RoutedEventArgs e)
        {
            comboBoxTypeOfEmpolyment.ItemsSource = Enum.GetValues(typeof(TypeOfEmployment)).Cast<TypeOfEmployment>();
        }

        private void ComboBoxReruitmentRelaionship_Loaded(object sender, RoutedEventArgs e)
        {
            comboBoxReruitmentRelaionship.ItemsSource = Enum.GetValues(typeof(EmployeeRecruitmentRelationShip)).Cast<EmployeeRecruitmentRelationShip>();
        }

        private void ComboBoxJobTitle_Loaded(object sender, RoutedEventArgs e)
        {
            comboBoxJobTitle.ItemsSource = Enum.GetValues(typeof(JobTitle)).Cast<JobTitle>();
        }

        private void ComboBoxDepartment_Loaded(object sender, RoutedEventArgs e)
        {
            comboBoxDepartment.ItemsSource = Enum.GetValues(typeof(Department)).Cast<Department>();
        }

        private void ComboBoxShift_Loaded(object sender, RoutedEventArgs e)
        {
            comboBoxShift.ItemsSource = Enum.GetValues(typeof(Shift)).Cast<Shift>();
        }

        private void ComboBoxHours_Loaded(object sender, RoutedEventArgs e)
        {
            comboBoxHours.ItemsSource = Enum.GetValues(typeof(Hour)).Cast<Hour>();
        }

        private void ComboBoxAccountName_Loaded(object sender, RoutedEventArgs e)
        {
            comboBoxAccountName.ItemsSource = Enum.GetValues(typeof(AccountName)).Cast<AccountName>();
        }

        private void ComboBoxUnit_Loaded(object sender, RoutedEventArgs e)
        {
            comboBoxUnit.ItemsSource = Enum.GetValues(typeof(Unit)).Cast<Unit>();
        }

        private void ComboBoxSortCode_Loaded(object sender, RoutedEventArgs e)
        {
            comboBoxSortCode.ItemsSource = Enum.GetValues(typeof(SortCode)).Cast<SortCode>();
        }

        private void ComboBoxPayFrequency_Loaded(object sender, RoutedEventArgs e)
        {
            comboBoxPayFrequency.ItemsSource = Enum.GetValues(typeof(PaymentFrequency)).Cast<PaymentFrequency>();
        }

        private void ComboBoxPaymentMethod_Loaded(object sender, RoutedEventArgs e)
        {
            comboBoxPaymentMethod.ItemsSource = Enum.GetValues(typeof(PaymentMethod)).Cast<PaymentMethod>();
        }

        private void BtnAddEmployeeRecruitmentInfo_Click(object sender, RoutedEventArgs e)
        {
            if (CheckForFilteredEmployee()) return;
            try
            {
                var command = new CreateEmployeeRecruitmentInfoCommand()
                {
                    EmployeeNumber = _filteredEmployee.EmployeeNumber,
                    EmployeeBankInfo = new EmployeeBankInfoCommand()
                    {
                        SortCode = (SortCode)comboBoxSortCode.SelectedValue,
                        AccountName = (AccountName)comboBoxAccountName.SelectedValue,
                        AccountNo = txtAccountNo.Text,
                        BankName = txtBankBame.Text,
                        Iban = txtIBAN.Text
                    },
                    EmployeePayment = new EmployeePaymentCommand()
                    {
                        PaymentMethod = (PaymentMethod)comboBoxPaymentMethod.SelectedValue,
                        PaymentFrequency = (PaymentFrequency)comboBoxPayFrequency.SelectedValue
                    },
                    EmployeeRecruitment = new EmployeeRecruitmentCommand()
                    {
                        DateLeft = dateLeftSelector.SelectedDate,
                        DatePensionStarted = datePensionSelector.SelectedDate,
                        DateStarted = dateStartedSelector.SelectedDate,
                        InsuarenceNumber = txtInsuranceNuber.Text,
                        Reason = txtReaseon.Text,
                        Relationship = (EmployeeRecruitmentRelationShip)comboBoxReruitmentRelaionship.SelectedValue,
                        TypeOfEmployment = (TypeOfEmployment)comboBoxTypeOfEmpolyment.SelectedValue
                    },
                    EmployeeWork = new EmployeeWorkCommand()
                    {
                        Shift = (Shift)comboBoxShift.SelectedValue,
                        Unit = (Unit)comboBoxUnit.SelectedValue,
                        Department = (Department)comboBoxDepartment.SelectedValue,
                        JobTitle = (JobTitle)comboBoxJobTitle.SelectedValue,
                        Hour = (Hour)comboBoxHours.SelectedValue,
                        TimecardNo = txtTimecardNo.Text
                    }
                };
                var result = RequestHelper.PostCommand(command, "http://localhost:5000/EmployeeRecruitmentInfo");
                MessageBox.Show("Employee Recruitment Info Successfully Has Been Updated", "Successfully Updated");
                ClientEmployeeRecruitmentInfoUpdateMode();
            }
            catch (Exception)
            {
                MessageBox.Show("Please Fill All Data Correctly", "InCorrect Input");
            }
        }

        private void ClientEmployeeRecruitmentInfoUpdateMode()
        {
            btnUpdateEmployeeRecruitmentInfo.IsEnabled = true;
            btnUpdateEmployeeRecruitmentInfo.Visibility = Visibility.Visible;
            btnAddEmployeeRecruitmentInfo.IsEnabled = false;
            btnAddEmployeeRecruitmentInfo.Visibility = Visibility.Hidden;
        }

        private void ClientEmployeeRecruitmentInfoNewMode()
        {
            btnUpdateEmployeeRecruitmentInfo.IsEnabled = false;
            btnUpdateEmployeeRecruitmentInfo.Visibility = Visibility.Hidden;
            btnAddEmployeeRecruitmentInfo.IsEnabled = true;
            btnAddEmployeeRecruitmentInfo.Visibility = Visibility.Visible;
        }

        private void BtnUpdateEmployeeRecruitmentInfo_Click(object sender, RoutedEventArgs e)
        {
            if (CheckForFilteredEmployee()) return;
            try
            {
                var command = new CreateEmployeeRecruitmentInfoCommand()
                {
                    EmployeeNumber = _filteredEmployee.EmployeeNumber,
                    EmployeeBankInfo = new EmployeeBankInfoCommand()
                    {
                        SortCode = (SortCode)comboBoxSortCode.SelectedValue,
                        AccountName = (AccountName)comboBoxAccountName.SelectedValue,
                        AccountNo = txtAccountNo.Text,
                        BankName = txtBankBame.Text,
                        Iban = txtIBAN.Text
                    },
                    EmployeePayment = new EmployeePaymentCommand()
                    {
                        PaymentMethod = (PaymentMethod)comboBoxPaymentMethod.SelectedValue,
                        PaymentFrequency = (PaymentFrequency)comboBoxPayFrequency.SelectedValue
                    },
                    EmployeeRecruitment = new EmployeeRecruitmentCommand()
                    {
                        DateLeft = dateLeftSelector.SelectedDate,
                        DatePensionStarted = datePensionSelector.SelectedDate,
                        DateStarted = dateStartedSelector.SelectedDate,
                        InsuarenceNumber = txtInsuranceNuber.Text,
                        Reason = txtReaseon.Text,
                        Relationship = (EmployeeRecruitmentRelationShip)comboBoxReruitmentRelaionship.SelectedValue,
                        TypeOfEmployment = (TypeOfEmployment)comboBoxTypeOfEmpolyment.SelectedValue
                    },
                    EmployeeWork = new EmployeeWorkCommand()
                    {
                        Shift = (Shift)comboBoxShift.SelectedValue,
                        Unit = (Unit)comboBoxUnit.SelectedValue,
                        Department = (Department)comboBoxDepartment.SelectedValue,
                        JobTitle = (JobTitle)comboBoxJobTitle.SelectedValue,
                        Hour = (Hour)comboBoxHours.SelectionBoxItem,
                        TimecardNo = txtTimecardNo.Text
                    }
                };
                var result = RequestHelper.PutCommand(command, "http://localhost:5000/EmployeeRecruitmentInfo");
                MessageBox.Show("Employee Recruitment Info Successfully Has Been Updated", "Successfully Updated");
                ClientEmployeeContactInfoUpdateMode();
            }
            catch (Exception)
            {
                MessageBox.Show("Please Fill All Data Correctly", "InCorrect Input");
            }
        }
    }
}
