using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Win32;
using Shaheda.Commands.Modules.Clients;
using Shaheda.Enums;
using Shaheda.Helpers;
using Shaheda.ViewModels.Clients;
using Shaheda.ViewModels.Languages;
using Shaheda.ViewModels.Origins;

namespace Shaheda
{
    /// <summary>
    /// Interaction logic for SaleClient.xaml
    /// </summary>
    public partial class SaleClient : Window
    {
        string _imageName;
        byte[] _imgByteArr;
        private readonly IEnumerable<OriginViewModel> _origins;
        private ClientInfoViewModel _filteredClient;

        public SaleClient()
        {
            try
            {
                RequestHelper.SendRequest<List<LanguageViewModel>>("http://localhost:5000/AllLanguages");
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
            InitializeComponent();
        }

        #region Load ComboBoxes Data
        private void ComboBoxRelation_Loaded(object sender, RoutedEventArgs e)
        {
            comboBoxRelation.ItemsSource = Enum.GetValues(typeof(ClientRelation)).Cast<ClientRelation>();
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
        }

        private void ComboBoxSalesman_Loaded(object sender, RoutedEventArgs e)
        {
            comboBoxSalesman.ItemsSource = Enum.GetValues(typeof(ClientSalesman)).Cast<ClientSalesman>();
        }

        private void ComboBoxClientCategory_Loaded(object sender, RoutedEventArgs e)
        {
            comboBoxClientCategory.ItemsSource = Enum.GetValues(typeof(ClientCategory)).Cast<ClientCategory>();
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
            comboBoxStatus.ItemsSource = Enum.GetValues(typeof(ClientStatus)).Cast<ClientStatus>();
        }

        private void ComboBoxOKToContact_Loaded(object sender, RoutedEventArgs e)
        {
            comboBoxOKToContact.ItemsSource = Enum.GetValues(typeof(OkToContact)).Cast<OkToContact>();
        }

        private void ComboBoxContactType_Loaded(object sender, RoutedEventArgs e)
        {
            comboBoxContactType.ItemsSource = Enum.GetValues(typeof(ClientContactType)).Cast<ClientContactType>();
        }

        private void ComboBoxCity_Loaded(object sender, RoutedEventArgs e)
        {
            comboBoxCity.ItemsSource = Enum.GetValues(typeof(City)).Cast<City>();
        }

        private void ComboBoxTown_Loaded(object sender, RoutedEventArgs e)
        {
            comboBoxTown.ItemsSource = Enum.GetValues(typeof(Town)).Cast<Town>();
        }

        private void ComboTraphist_Loaded(object sender, RoutedEventArgs e)
        {
            comboTraphist.ItemsSource = Enum.GetValues(typeof(Therapist)).Cast<Therapist>();
        }

        private void ComboGpsName_Loaded(object sender, RoutedEventArgs e)
        {
            comboGpsName.ItemsSource = Enum.GetValues(typeof(GpsName)).Cast<GpsName>();
        }

        private void ComboReferralFor_Loaded(object sender, RoutedEventArgs e)
        {
            comboReferralFor.ItemsSource = Enum.GetValues(typeof(ReferralFor)).Cast<ReferralFor>();
        }

        private void ComboReferralBy_Loaded(object sender, RoutedEventArgs e)
        {
            comboReferralBy.ItemsSource = Enum.GetValues(typeof(ReferralBy)).Cast<ReferralBy>();
        }
        private void ComboOtherRequirements_Loaded(object sender, RoutedEventArgs e)
        {
            comboOtherRequirements.ItemsSource = Enum.GetValues(typeof(OtherReqirments)).Cast<OtherReqirments>();
        }
        #endregion

        #region Fill Form Elements

        private void SetClientPurchaseWithFilteredData()
        {
            txtBalance.Text = _filteredClient.ClientPurchase.Balance;
            txtCredit.Text = _filteredClient.ClientPurchase.Credit;
            txtDiscount.Text = _filteredClient.ClientPurchase.Discount;
            txtPurchasePaymentMethod.Text = _filteredClient.ClientPurchase.PaymentMethod;
            txtPaymentTerms.Text = _filteredClient.ClientPurchase.PaymentTerms;
            txtPricing.Text = _filteredClient.ClientPurchase.Pricing;
            txtVAT.Text = _filteredClient.ClientPurchase.Vat;
        }

        private void SetClientPaymentWithFilteredData()
        {
            txtPaymetnName.Text = _filteredClient.ClientPayment.Name;
            comboOtherRequirements.SelectedValue = _filteredClient.ClientPayment.OtherReqirments;
            comboGpsName.SelectedValue = _filteredClient.ClientPayment.GpsName;
            comboReferralFor.SelectedValue = _filteredClient.ClientPayment.ReferralFor;
            comboTraphist.SelectedValue = _filteredClient.ClientPayment.Therapist;
            comboReferralBy.SelectedValue = _filteredClient.ClientPayment.ReferralBy;
            dateOfReferralSelector.SelectedDate = _filteredClient.ClientPayment.DateOfReferral;
            txtPaymentGPsAddress.Text = _filteredClient.ClientPayment.GpsAddress;
            txtPaymentGPsNumber.Text = _filteredClient.ClientPayment.GpsNumber;
            txtPaymentReasonForReferral.Text = _filteredClient.ClientPayment.ReasonForRefrral;
            txtPaymentRefferalTel.Text = _filteredClient.ClientPayment.ReferralTel;
            dateOfReferralSelector.SelectedDate = _filteredClient.ClientPayment.DateOfReferral;
        }

        private void SetClientExtraInformationWithFilteredData()
        {
            txtExtraInformationName.Text = _filteredClient.ClientExtraInformation.Name;
            txtExtraInformationContactNumber.Text = _filteredClient.ClientExtraInformation.ContactNumber;
            txtExtraInformationNtk.Text = _filteredClient.ClientExtraInformation.Ntk;
            txtExtraInformationRelationship.Text = _filteredClient.ClientExtraInformation.RelationShip;
        }

        private void SetClientAddressWithFilteredAdress()
        {
            comboBoxTown.SelectedValue = _filteredClient.ClientAddress.Town;
            comboBoxCity.SelectedValue = _filteredClient.ClientAddress.City;
            txtAddress.Text = _filteredClient.ClientAddress.Address;
            txtBussinessAddress.Text = _filteredClient.ClientAddress.BussinesAddress;
            txtAddressInfoPostalCode.Text = _filteredClient.ClientAddress.PostalCode;
        }

        private void SetClientDeliveryAddressWithFilteredData()
        {
            txtDeliveryAddressTown.Text = _filteredClient.ClientDeliveryAddress.Town;
            txtDeliveryAddressCity.Text = _filteredClient.ClientDeliveryAddress.City;
            txtEmergencyAddress.Text = _filteredClient.ClientDeliveryAddress.Address;
            txtDeliveryPostalCode.Text = _filteredClient.ClientDeliveryAddress.PostalCode;
            txtDeliveryAddressPhoneNumber.Text = _filteredClient.ClientDeliveryAddress.PhoneNumber;
            txtDeliveryAddressName.Text = _filteredClient.ClientDeliveryAddress.Name;
        }

        private void SetClientContactWithFiteredData()
        {
            txtPhoneNumber.Text = _filteredClient.ClientContact.PhoneNumber;
            txtEmailAdrress.Text = _filteredClient.ClientContact.EmailAddress;
            txtMobileNumber.Text = _filteredClient.ClientContact.MobileNumber;
            txtHomeNumber.Text = _filteredClient.ClientContact.HomeNumber;
            comboBoxOKToContact.SelectedValue = _filteredClient.ClientContact.OkToContact;
            comboBoxContactType.SelectedValue = _filteredClient.ClientContact.ContactType;
            txtWebsite.Text = _filteredClient.ClientContact.Website;
        }

        #endregion

        private void BtnUploadPhoto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FileDialog fldlg = new OpenFileDialog();
                fldlg.InitialDirectory = Environment.SpecialFolder.MyPictures.ToString();
                fldlg.Filter = "Image File (*.jpg;*.bmp;*.gif)|*.jpg;*.bmp;*.gif";
                fldlg.ShowDialog();
                {
                    _imageName = fldlg.FileName;
                    ImageSourceConverter isc = new ImageSourceConverter();
                    image1.SetValue(Image.SourceProperty, isc.ConvertFromString(_imageName));
                }
                var fs = new FileStream(_imageName, FileMode.Open, FileAccess.Read);
                _imgByteArr = new byte[fs.Length];
                fs.Read(_imgByteArr, 0, Convert.ToInt32(fs.Length));
                fldlg = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnNewClient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var command = new CreateClientBaseInfoCommand()
                {
                    ClientCategory = (ClientCategory)comboBoxClientCategory.SelectedValue,
                    Dob = dobSelector.SelectedDate,
                    Title = (Titles)comboBoxTitle.SelectedValue,
                    Gender = (Gender)comboBoxGender.SelectedValue,
                    Name = txtName.Text,
                    NickName = txtNickname.Text,
                    Relation = (ClientRelation)comboBoxRelation.SelectedValue,
                    Salesman = (ClientSalesman)comboBoxSalesman.SelectedValue,
                    SexualOrientation = (SexualOrientation)comboBoxSexual.SelectedValue,
                    Status = (ClientStatus)comboBoxStatus.SelectedValue,
                    Surname = txtSurname.Text,
                    Photo = _imgByteArr,
                    OriginId = (int)((CustomeComboboxItem)comboOrigin.SelectedValue).Value
                };
                var result = RequestHelper.PostCommand(command, "http://localhost:5000/ClientBaseInfo");
                MessageBox.Show($"{command.Name} {command.Surname} with {result} client code successfully created", "Successfully Created");
                ClientBaseInfoUpdateMode();

            }
            catch (Exception)
            {
                MessageBox.Show("Please Fill All Data Correctly", "InCorrect Input");
            }
        }

        private void BtnFilterClicent_Click(object sender, RoutedEventArgs e)
        {
            var id = txtClientCodeFilter.Text;
            try
            {
                _filteredClient = RequestHelper.SendRequest<ClientInfoViewModel>("http://localhost:5000/GetClientInfoByClientCode?id=" + id);
            }
            catch (Exception)
            {
                MessageBox.Show("We have a problem in Fetching Client Data", "Fetching Data");
                return;
            }
            ClearForm();
            comboBoxTitle.SelectedValue = _filteredClient.Title;
            comboBoxSexual.SelectedValue = _filteredClient.SexualOrientation;
            comboBoxClientCategory.SelectedValue = _filteredClient.ClientCategory;
            comboBoxGender.SelectedValue = _filteredClient.Gender;
            comboBoxRelation.SelectedValue = _filteredClient.Relation;
            comboBoxSalesman.SelectedValue = _filteredClient.Salesman;
            comboBoxStatus.SelectedValue = _filteredClient.Status;
            txtName.Text = _filteredClient.Name;
            txtSurname.Text = _filteredClient.Surname;
            txtclientcode.Text = _filteredClient.ClientCode.ToString();
            txtNickname.Text = _filteredClient.NickName;

            comboOrigin.SelectedItem = new CustomeComboboxItem()
            {
                Text = _filteredClient.Origin.OriginName,
                Value = _filteredClient.Origin.Id
            };
            image1.Source = ByteToImageConverter.ToImage(_filteredClient.Photo);
            ClientBaseInfoUpdateMode();
            dobSelector.SelectedDate = _filteredClient.Dob;

            if (_filteredClient.ClientContact != null || _filteredClient.ClientDeliveryAddress != null ||
                _filteredClient.ClientAddress != null)
            {
                ClientContactInfoUpdatMode();
            }
            else
            {
                ClientContactInfoAddNewMode();
            }

            if (_filteredClient.ClientPurchase != null || _filteredClient.ClientPayment != null ||
                _filteredClient.ClientExtraInformation != null)
            {
                ClientPurchaceInfoUpdatMode();
            }
            else
            {
                ClientPurchaseInfoAddNewMode();
            }

            if (_filteredClient.ClientContact != null)
                SetClientContactWithFiteredData();

            if (_filteredClient.ClientDeliveryAddress != null)
                SetClientDeliveryAddressWithFilteredData();

            if (_filteredClient.ClientAddress != null)
                SetClientAddressWithFilteredAdress();

            if (_filteredClient.ClientExtraInformation != null)
            {
                SetClientExtraInformationWithFilteredData();
            }

            if (_filteredClient.ClientPayment != null)
            {
                SetClientPaymentWithFilteredData();
            }

            if (_filteredClient.ClientPurchase != null)
            {
                SetClientPurchaseWithFilteredData();
            }

        }

        private void ClientBaseInfoUpdateMode()
        {
            btnNewClient.IsEnabled = false;
            btnClearForm.IsEnabled = true;
            btnNewClient.Visibility = Visibility.Hidden;
            btnUpdateClientBaseInfo.Visibility = Visibility.Visible;
            btnUpdateClientBaseInfo.IsEnabled = true;
        }

        private void ClientPurchaseInfoAddNewMode()
        {
            btnAddClientPurchaceInfo.IsEnabled = true;
            btnUpdateClientPurchaceInfo.IsEnabled = false;
            btnAddClientPurchaceInfo.Visibility = Visibility.Visible;
            btnUpdateClientPurchaceInfo.Visibility = Visibility.Hidden;
        }

        private void ClientPurchaceInfoUpdatMode()
        {
            btnAddClientPurchaceInfo.IsEnabled = false;
            btnUpdateClientPurchaceInfo.IsEnabled = true;
            btnAddClientPurchaceInfo.Visibility = Visibility.Hidden;
            btnUpdateClientPurchaceInfo.Visibility = Visibility.Visible;
        }

        private void ClientContactInfoUpdatMode()
        {
            btnAddClientContactInfo.IsEnabled = false;
            btnUpdateClientContactInfo.IsEnabled = true;
            btnAddClientContactInfo.Visibility = Visibility.Hidden;
            btnUpdateClientContactInfo.Visibility = Visibility.Visible;
        }

        private void ClientContactInfoAddNewMode()
        {
            btnAddClientContactInfo.IsEnabled = true;
            btnUpdateClientContactInfo.IsEnabled = false;
            btnAddClientContactInfo.Visibility = Visibility.Visible;
            btnUpdateClientContactInfo.Visibility = Visibility.Hidden;
        }

        private void BtnAddClientContactInfo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_filteredClient == null)
                {
                    MessageBox.Show("Please First Filter your client", "Filter Client");
                    return;
                }
                var command = CreateClientContactInfoCommand();
                RequestHelper.PostCommand(command, "http://localhost:5000/ClientContactInfo");
                btnAddClientContactInfo.IsEnabled = false;
            }
            catch (Exception)
            {
                MessageBox.Show("Please Fill All Data Correctly", "InCorrect Input");
            }
        }

        private CreateClientContactInfoCommand CreateClientContactInfoCommand()
        {
            var command = new CreateClientContactInfoCommand()
            {
                ClientAddress = new ClientAddressCommand()
                {
                    Town = (Town)comboBoxTown.SelectedValue,
                    City = (City)comboBoxCity.SelectedValue,
                    Address = txtAddress.Text,
                    BussinesAddress = txtBussinessAddress.Text,
                    PostalCode = txtAddressInfoPostalCode.Text
                },
                ClientCode = _filteredClient.ClientCode,
                ClientContact = new ClientContactCommand()
                {
                    OkToContact = (OkToContact)comboBoxOKToContact.SelectedValue,
                    ContactType = (ClientContactType)comboBoxContactType.SelectedValue,
                    EmailAddress = txtEmailAdrress.Text,
                    HomeNumber = txtHomeNumber.Text,
                    MobileNumber = txtMobileNumber.Text,
                    PhoneNumber = txtPhoneNumber.Text,
                    Website = txtWebsite.Text
                },
                ClientDeliveryAddress = new ClientDeliveryAddressCommand()
                {
                    Town = txtDeliveryAddressTown.Text,
                    City = txtDeliveryAddressCity.Text,
                    Address = txtEmergencyAddress.Text,
                    PostalCode = txtDeliveryPostalCode.Text,
                    PhoneNumber = txtDeliveryAddressPhoneNumber.Text,
                    Name = txtDeliveryAddressName.Text
                }
            };
            return command;
        }

        private void BtnClearForm_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
            _filteredClient = null;
        }

        private void ClearForm()
        {
            comboBoxTitle.SelectedValue = null;
            comboBoxSexual.SelectedValue = null;
            comboBoxClientCategory.SelectedValue = null;
            comboBoxGender.SelectedValue = null;
            comboBoxRelation.SelectedValue = null;
            comboBoxSalesman.SelectedValue = null;
            comboBoxStatus.SelectedValue = null;
            dobSelector.SelectedDate = null;
            txtName.Text = "";
            txtSurname.Text = "";
            txtclientcode.Text = "";
            txtNickname.Text = "";
            comboOrigin.SelectedItem = null;
            image1.Source = (ImageSource)FindResource("blueuser");
            ClientBaseInfoAddNewMode();
            comboBoxTown.SelectedValue = null;
            comboBoxCity.SelectedValue = null;
            txtAddress.Text = "";
            txtBussinessAddress.Text = "";
            txtAddressInfoPostalCode.Text = "";
            txtDeliveryAddressTown.Text = "";
            txtDeliveryAddressCity.Text = "";
            txtEmergencyAddress.Text = "";
            txtDeliveryPostalCode.Text = "";
            txtDeliveryAddressPhoneNumber.Text = "";
            txtDeliveryAddressName.Text = "";
            txtPhoneNumber.Text = "";
            txtEmailAdrress.Text = "";
            txtMobileNumber.Text = "";
            txtHomeNumber.Text = "";
            comboBoxOKToContact.SelectedValue = null;
            comboBoxContactType.SelectedValue = null;
            txtWebsite.Text = "";
            txtExtraInformationName.Text = "";
            txtExtraInformationContactNumber.Text = "";
            txtExtraInformationNtk.Text = "";
            txtExtraInformationRelationship.Text = "";
            txtBalance.Text = "";
            txtCredit.Text = "";
            txtDiscount.Text = "";
            txtPurchasePaymentMethod.Text = "";
            txtPaymentTerms.Text = "";
            txtPricing.Text = "";
            txtVAT.Text = "";
            txtPaymetnName.Text = "";
            comboOtherRequirements.SelectedValue = null;
            comboGpsName.SelectedValue = null;
            comboReferralFor.SelectedValue = null;
            comboTraphist.SelectedValue = null;
            comboReferralBy.SelectedValue = null;
            dateOfReferralSelector.SelectedDate = null;
            txtPaymentGPsAddress.Text = "";
            txtPaymentGPsNumber.Text = "";
            txtPaymentReasonForReferral.Text = "";
            txtPaymentRefferalTel.Text = "";
            ClientPurchaseInfoAddNewMode();
            ClientContactInfoAddNewMode();
        }

        private void ClientBaseInfoAddNewMode()
        {
            btnNewClient.IsEnabled = true;
            btnNewClient.Visibility = Visibility.Visible;
            btnClearForm.IsEnabled = false;
            btnUpdateClientBaseInfo.Visibility = Visibility.Hidden;
            btnUpdateClientBaseInfo.IsEnabled = false;
        }

        private void BtnAddClientPurchaceInfo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_filteredClient == null)
                {
                    MessageBox.Show("Please First Filter your client", "Filter Client");
                    return;
                }
                var command = CreateClientPurchaceInformationCommand();
                RequestHelper.PostCommand(command, "http://localhost:5000/ClientPurchaceInfo");
                btnAddClientContactInfo.IsEnabled = false;
            }
            catch (Exception)
            {
                MessageBox.Show("Please Fill All Data Correctly", "InCorrect Input");
            }
        }

        private CreateClientPurchaceInformationCommand CreateClientPurchaceInformationCommand()
        {
            var command = new CreateClientPurchaceInformationCommand()
            {
                ClientCode = _filteredClient.ClientCode,
                ClientExtraInformation = new ClientExtraInformationCommand()
                {
                    Name = txtExtraInformationName.Text,
                    ContactNumber = txtExtraInformationContactNumber.Text,
                    Ntk = txtExtraInformationNtk.Text,
                    RelationShip = txtExtraInformationRelationship.Text
                },
                ClientPayment = new ClientPaymentCommand()
                {
                    Name = txtPaymetnName.Text,
                    OtherReqirments = (OtherReqirments)comboOtherRequirements.SelectedValue,
                    GpsName = (GpsName)comboGpsName.SelectedValue,
                    ReferralFor = (ReferralFor)comboReferralFor.SelectedValue,
                    Therapist = (Therapist)comboTraphist.SelectedValue,
                    ReferralBy = (ReferralBy)comboReferralBy.SelectedValue,
                    DateOfReferral = dateOfReferralSelector.SelectedDate,
                    GpsAddress = txtPaymentGPsAddress.Text,
                    GpsNumber = txtPaymentGPsNumber.Text,
                    ReasonForRefrral = txtPaymentReasonForReferral.Text,
                    ReferralTel = txtPaymentRefferalTel.Text
                },
                ClientPurchaceInformation = new ClientPurchaceInformationCommand()
                {
                    Balance = txtBalance.Text,
                    Credit = txtCredit.Text,
                    Discount = txtDiscount.Text,
                    PaymentMethod = txtPurchasePaymentMethod.Text,
                    PaymentTerms = txtPaymentTerms.Text,
                    Pricing = txtPricing.Text,
                    Vat = txtVAT.Text
                }
            };
            return command;
        }

        /// <summary>
        /// ویرایش اطلاعات پایه کلاینت
        /// Update Client Base Info
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnUpdateClientBaseInfo_Click(object sender, RoutedEventArgs e)
        {
            if (_filteredClient == null)
            {
                MessageBox.Show("Please First Filter your client", "Filter Client");
                return;
            }

            try
            {
                var command = new UpdateClientBaseInfoCommand()
                {
                    ClientCode = _filteredClient.ClientCode,
                    ClientCategory = (ClientCategory)comboBoxClientCategory.SelectedValue,
                    Dob = dobSelector.SelectedDate,
                    Title = (Titles)comboBoxTitle.SelectedValue,
                    Gender = (Gender)comboBoxGender.SelectedValue,
                    Name = txtName.Text,
                    NickName = txtNickname.Text,
                    Relation = (ClientRelation)comboBoxRelation.SelectedValue,
                    Salesman = (ClientSalesman)comboBoxSalesman.SelectedValue,
                    SexualOrientation = (SexualOrientation)comboBoxSexual.SelectedValue,
                    Status = (ClientStatus)comboBoxStatus.SelectedValue,
                    Surname = txtSurname.Text,
                    Photo = _imgByteArr,
                    OriginId = (int)((CustomeComboboxItem)comboOrigin.SelectedValue).Value
                };
                var result = RequestHelper.PutCommand(command, "http://localhost:5000/ClientBaseInfo");
                MessageBox.Show($"{command.Name} {command.Surname} with {result} client code successfully created", "Successfully Created");
                ClientBaseInfoUpdateMode();

            }
            catch (Exception)
            {
                MessageBox.Show("Please Fill All Data Correctly", "InCorrect Input");
            }
        }

        private void BtnUpdateClientPurchaseInfo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnUpdateClientContatcInfo_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
