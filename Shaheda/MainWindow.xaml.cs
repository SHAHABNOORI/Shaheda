using System.Windows;


namespace Shaheda
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        //Change View Pages from Mainwindow
        //Closing previous pages and open new pages
        private void RibbonButton_Click(object sender, RoutedEventArgs e)
        {
            SaleClient windowClient = new SaleClient();
            windowClient.Show();
        }
        private void RibbonSetting_Click(object sender, RoutedEventArgs e)
        {
            SettingAdministrative windowSettingAdministrative = new SettingAdministrative();
            windowSettingAdministrative.Show();
        }
        private void RibbonGeneral_Click(object sender, RoutedEventArgs e)
        {
            SettingGeneral windowGeneralSetting = new SettingGeneral();
            windowGeneralSetting.Show();
        }
        private void RibbonOption_Click(object sender, RoutedEventArgs e)
        {
            SettingOption windowSettingOption = new SettingOption();
        }
        private void RibbonReminder_Click(object sender, RoutedEventArgs e)
        {
            SettingReminder windowSettingReminder = new SettingReminder();
            windowSettingReminder.Show();
        }
        private void RibbonNew_Click(object sender, RoutedEventArgs e)
        {
            ReminderNew windowReminderNew = new ReminderNew();
            windowReminderNew.Show();
            
        }
        private void RibbonTodolist_Click(object sender, RoutedEventArgs e)
        {
            ReminderToDo windowReminderTodolist = new ReminderToDo();
            windowReminderTodolist.Show();
        }
        private void RibbonAdministrative_Click(object sender, RoutedEventArgs e)
        {
            AdministrativeEmpolyment windowAdministrativeEmployment = new AdministrativeEmpolyment();
            windowAdministrativeEmployment.Show();
        }
        private void RibbonSecretariat_click(object sender, RoutedEventArgs e)
        {
            AdministrativeSecretariat windowAdministrativeSecretariat = new AdministrativeSecretariat();
            windowAdministrativeSecretariat.Show();
        }
        private void RibbonPayroll_click(object sender, RoutedEventArgs e)
        {
            AdministrativePayroll windowAdministrativePayroll = new AdministrativePayroll();
            windowAdministrativePayroll.Show();
        }

        //Reset textbox in main window
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            txtAll.Text = "";
            txtName.Text = "";
            txtRef.Text = "";
        }

        private void RibbonPremission_Click(object sender, RoutedEventArgs e)
        {
            Premission prewindow = new Premission();
            prewindow.Show();
        }
    }
}
