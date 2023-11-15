using System.Windows;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace TemplateProject.Windows
{
    public partial class EmployeeWindow
    {
        private int ID { get; }
        private UserInformation User { get; set; }

        public EmployeeWindow(UserInformation user,int ID = -1)
        {
            InitializeComponent();

            this.ID = ID;
            User = user;

            using (DataContext db = new DataContext())
            {

                PositionComboBox.ItemsSource = db.Position.ToList();
                OrganizationComboBox.ItemsSource = db.Contractor.Where(x => x.UserID == User.ID).ToList();

                if (ID != -1)
                {
                    var List = db.Employee.Find(ID);
                    FioTextBox.Text = List.FIO;
                    PositionComboBox.Text = List.Position.Name;
                    OrganizationComboBox.Text = List.Contractor.Name;
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (FioTextBox.Text == "" || !Regex.IsMatch(FioTextBox.Text, @"^([А-яЁёA-z]*\s){2}[А-яЁёA-z]*$"))
                    throw new ArgumentException("Ошибка. Вы не заполнили поле фио");
                if (PositionComboBox.Text == "")
                    throw new ArgumentException("Ошибка. Вы не выбрали должность");
                if (OrganizationComboBox.Text == "")
                    throw new ArgumentException("Ошибка. Вы не выбрали организацию");

                using (DataContext db = new DataContext())
                {
                    if (PositionComboBox.Text == "Директор")
                    {
                        var position = db.Position.First(x => x.Name == PositionComboBox.Text && x.Name == "Директор");
                        var contractor = db.Contractor.First(x => x.Name == OrganizationComboBox.Text && x.UserID == User.ID);

                        if (db.Employee.Where(x => x.PositionID == position.ID && x.ID != ID && x.ContractorID == contractor.ID).Count() > 0)
                            throw new ArgumentException("Ошибка. В организации не может быть больше 1 директора!");
                    }

                    if (ID == -1)
                    {
                        db.Employee.Add(new Employee()
                        {
                            FIO = FioTextBox.Text,
                            PositionID = (PositionComboBox.SelectedItem as Position).ID,
                            ContractorID = (OrganizationComboBox.SelectedItem as Contractor).ID,
                            UserID = User.ID
                        });
                        db.SaveChanges();

                        this.Close();
                    }
                    else
                    {
                        var List = db.Employee.Find(ID);
                        List.FIO = FioTextBox.Text;
                        List.PositionID = (PositionComboBox.SelectedItem as Position).ID;
                        List.ContractorID = (OrganizationComboBox.SelectedItem as Contractor).ID;
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
