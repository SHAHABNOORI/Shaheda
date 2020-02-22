using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Shaheda.Enums;
using Shaheda.Helpers;
using Shaheda.ViewModels.Degrees;
using Shaheda.ViewModels.Educations;
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
    }
}
