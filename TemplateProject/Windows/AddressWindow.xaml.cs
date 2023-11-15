using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TemplateProject.Windows
{
    public partial class AddressWindow : Window
    {
        private int ID { get; }
        private UserInformation User { get; set; }

        public AddressWindow(UserInformation user, int ID = -1)
        {
            InitializeComponent();

            this.ID = ID;
            User = user;

            if (ID != -1)
            {
                using (DataContext db = new DataContext())
                {
                    var List = db.Address.Find(ID);

                    CityTextBox.Text = List.City;
                    StreetTextBox.Text = List.Street;
                    HouseNumberTextBox.Text = List.HouseNumber.ToString();
                }
            }
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (CityTextBox.Text == "" || !Regex.IsMatch(CityTextBox.Text, @"^[А-яA-zЁё]*$"))
                    throw new ArgumentException("Ошибка. Вы не заполнили поле город");
                if (StreetTextBox.Text == "" || !Regex.IsMatch( StreetTextBox.Text, @"^[А-яA-zЁё\-\s]*$"))
                    throw new ArgumentException("Ошибка. Вы не заполнили поле улица");
                if (HouseNumberTextBox.Text == "" || Convert.ToInt32(HouseNumberTextBox.Text) <= 0 || Regex.IsMatch(HouseNumberTextBox.Text, @"^[0-9]{4}$"))
                    throw new ArgumentException("Ошибка. Вы не заполнили поле дом");

                if (ID == -1)
                {
                    using (DataContext db = new DataContext())
                    {
                        db.Address.Add(new Address()
                        {
                            City = CityTextBox.Text,
                            Street = StreetTextBox.Text,
                            HouseNumber = Convert.ToInt32(HouseNumberTextBox.Text),
                            UserID = User.ID
                        });
                        db.SaveChanges();

                        this.Close();
                    }
                }
                else
                {
                    using (DataContext db = new DataContext())
                    {
                        var List = db.Address.Find(ID);
                        List.City = CityTextBox.Text;
                        List.Street = StreetTextBox.Text;
                        List.HouseNumber = Convert.ToInt32(HouseNumberTextBox.Text);
                        db.SaveChanges();

                        this.Close();
                    }
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
