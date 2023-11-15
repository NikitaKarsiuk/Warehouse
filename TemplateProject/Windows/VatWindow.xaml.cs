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
    public partial class VatWindow : Window
    {
        private int ID { get; }
        private UserInformation User { get; set; }

        public VatWindow(UserInformation user, int ID = -1)
        {
            InitializeComponent();

            this.ID = ID;
            User = user;

            if (ID != -1)
            {
                using (DataContext db = new DataContext())
                {
                    var List = db.Vat.Find(ID);

                    PercentTextBox.Text = List.Percents.ToString();
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (PercentTextBox.Text == "" || Convert.ToInt32(PercentTextBox.Text) < 0 || Regex.IsMatch(PercentTextBox.Text, @"^[0-9]{1, 2}*$"))
                    throw new ArgumentException("Ошибка. Вы ввели некорректное значение");

                if (ID == -1)
                {
                    using (DataContext db = new DataContext())
                    {
                        db.Vat.Add(new Vat()
                        {
                            Percents = Convert.ToInt32(PercentTextBox.Text),
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
                        var List = db.Vat.Find(ID);
                        List.Percents = Convert.ToInt32(PercentTextBox.Text);

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
