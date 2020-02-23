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
            ClientEmployeePersonalInfoNewMode();
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
    }
}
