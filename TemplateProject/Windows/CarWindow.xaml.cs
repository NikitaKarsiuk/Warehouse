using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace TemplateProject.Windows
{
    public partial class CarWindow : Window
    {
        private int ID { get; }
        private UserInformation User { get; set; }

        public CarWindow(UserInformation user, int ID = -1)
        {
            InitializeComponent();

            this.ID = ID;
            User = user;

            using (DataContext db = new DataContext())
            {
                OrganizationComboBox.ItemsSource = db.Contractor.Where(x => x.ContractorType.Name == "Организация" && x.UserID == User.ID).ToList();

                if (ID != -1)
                {
                    var List = db.Car.Find(ID);
                    NameTextBox.Text = List.Name;
                    NumberTextBox.Text = List.Number;
                    OrganizationComboBox.SelectedItem = List.Contractor;
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (NameTextBox.Text == "" || !Regex.IsMatch(NameTextBox.Text, @"^[А-яA-zЁё0-9\s-""]*$"))
                    throw new ArgumentException("Ошибка. Вы не заполнили поле марка");
                if (NumberTextBox.Text == "" || !Regex.IsMatch(NumberTextBox.Text, @"^[A-z]{2} [0-9]{4}-[0-7]{1}$"))
                    throw new ArgumentException("Ошибка. Вы не заполнили поле номер");
                if (OrganizationComboBox.Text == "")
                    throw new ArgumentException("Ошибка. Вы не выбрали организацию");

                using (DataContext db = new DataContext())
                {
                    var car = new Car();

                    if (ID == -1)
                    {
                        car = new Car()
                        {
                            Name = NameTextBox.Text,
                            Number = NumberTextBox.Text,
                            ContractorID = (OrganizationComboBox.SelectedItem as Contractor).ID,
                            UserID = User.ID
                        };

                        db.Car.Add(car);

                    }
                    else
                    {
                        var List = db.Car.Find(ID);

                        List.Name = NameTextBox.Text;
                        List.Number = NumberTextBox.Text;
                        List.ContractorID = (OrganizationComboBox.SelectedItem as Contractor).ID;
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
