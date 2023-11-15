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
    public partial class UnitWindow : Window
    {
        private int ID { get; }
        public UnitWindow(int ID = -1)
        {
            InitializeComponent();
            this.ID = ID;

            if (ID != -1)
            {
                using (DataContext db = new DataContext())
                {
                    var List = db.Unit.Find(ID);

                    NameTextBox.Text = List.Name;
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (NameTextBox.Text == "" || !Regex.IsMatch(NameTextBox.Text, @"^[А-яA-zЁё]*$"))
                    throw new ArgumentException("Ошибка. Вы не заполнили поле единица измерения");

                if (ID == -1)
                {
                    using (DataContext db = new DataContext())
                    {
                        db.Unit.Add(new Unit()
                        {
                            Name = NameTextBox.Text,
                        });
                        db.SaveChanges();

                        this.Close();
                    }
                }
                else
                {
                    using (DataContext db = new DataContext())
                    {
                        var List = db.Unit.Find(ID);
                        List.Name = NameTextBox.Text;
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
