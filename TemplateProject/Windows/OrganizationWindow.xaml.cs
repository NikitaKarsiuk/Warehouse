using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TemplateProject.Windows
{
    public partial class OrganizationWindow : Window
    {
        private int ID { get; }
        private UserInformation User { get; set; }

        public OrganizationWindow(UserInformation user, int ID = -1)
        {
            InitializeComponent();

            this.ID = ID;
            User = user;

            using (DataContext db = new DataContext())
            {
                AddressCombobox.ItemsSource = db.Address.Where(x => x.UserID == user.ID).ToList();

                if (ID != -1)
                {
                    var List = db.Contractor.Find(ID);

                    NameTextBox.Text = List.Name;
                    BankDetailsTextBox.Text = List.BankDetails;
                    UNPTextBox.Text = List.UNP;
                    OKPOTextBox.Text = List.OKPO;
                    ContactNumberTextBox.Text = List.ContactNumber;
                    AddressCombobox.SelectedItem = List.Address;
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (NameTextBox.Text == "" || !Regex.IsMatch(NameTextBox.Text, @"^[ОАЗДИП]{2,4}\s[«][А-яЁёA-z\s№0-9]{1,40}[»]$"))
                    throw new ArgumentException("Ошибка. Вы не заполнили поле наименование");
                if (BankDetailsTextBox.Text == "" || !Regex.IsMatch(BankDetailsTextBox.Text, @"^(BY20)[A-z]{4}[0-9]{20}$"))
                    throw new ArgumentException("Ошибка. Вы не заполнили поле Р/С");
                if (UNPTextBox.Text == "" || !Regex.IsMatch(UNPTextBox.Text, @"^[0-9]{9}$"))
                    throw new ArgumentException("Ошибка. Вы не заполнили поле УНП");
                if (OKPOTextBox.Text == "" || !Regex.IsMatch(OKPOTextBox.Text, @"^[0-9]{9}$"))
                    throw new ArgumentException("Ошибка. Вы не заполнили поле ОКПО");
                if (ContactNumberTextBox.Text == "" || !Regex.IsMatch(ContactNumberTextBox.Text, @"^[+]375[(]((29)|(44)|(33)|(25)|(17))[)]\d{3}[-]\d{2}[-]\d{2}$"))
                    throw new ArgumentException("Ошибка. Вы ввели некорректный номер телефона");
                if (AddressCombobox.Text == "") 
                    throw new ArgumentException("Ошибка. Вы не выбрали адрес");


                using (DataContext db = new DataContext())
                {
                    if (ID == -1)
                    {

                        db.Contractor.Add(new Contractor()
                        {
                            Name = NameTextBox.Text,
                            BankDetails = BankDetailsTextBox.Text,
                            UNP = UNPTextBox.Text,
                            OKPO = OKPOTextBox.Text,
                            ContactNumber = ContactNumberTextBox.Text,
                            ContractorTypeID = db.ContractorType.First(x => x.Name == "Организация").ID,
                            AddressID = (AddressCombobox.SelectedItem as Address).ID,
                            UserID = User.ID
                        });
                    }
                    else
                    {
                        var List = db.Contractor.Find(ID);

                        List.Name = NameTextBox.Text;
                        List.BankDetails = BankDetailsTextBox.Text;
                        List.UNP = UNPTextBox.Text;
                        List.OKPO = OKPOTextBox.Text;
                        List.ContactNumber = ContactNumberTextBox.Text;
                        List.ContractorTypeID = db.ContractorType.First(x => x.Name == "Организация").ID;
                        List.AddressID = (AddressCombobox.SelectedItem as Address).ID;
                    }

                    db.SaveChanges();

                    this.Close();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                string pathDocument = Environment.CurrentDirectory + "\\help.chm";
                System.Diagnostics.Process.Start(pathDocument);
            }
        }
    }
}
