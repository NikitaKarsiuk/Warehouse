using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace TemplateProject.Windows
{
    public partial class TypeProductWindow : Window
    {
        private int ID { get; }
        public TypeProductWindow(int ID = -1)
        {
            InitializeComponent();
            this.ID = ID;

            if (ID != -1)
            {
                using (DataContext db = new DataContext())
                {
                    var List = db.ProductType.Find(ID);
                    TypeProductTextBox.Text = List.Name;
                    Square200TextBox.Text = List.Square200.ToString();
                    Square400TextBox.Text = List.Square400.ToString();
                    Square650TextBox.Text = List.Square650.ToString();
                    Square800TextBox.Text = List.Square800.ToString();
                    Square1000TextBox.Text = List.Square1000.ToString();
                    Square2500TextBox.Text = List.Square2500.ToString();
                    Square4000TextBox.Text = List.Square4000.ToString();
                    Square6000TextBox.Text = List.Square6000.ToString();
                    Square8000TextBox.Text = List.Square8000.ToString();
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TypeProductTextBox.Text == "" || !Regex.IsMatch(TypeProductTextBox.Text, @"^[А-яA-zЁё]*$"))
                    throw new ArgumentException("Ошибка. Вы не заполнили поле тип продукта");

                if (ID == -1)
                {
                    using (DataContext db = new DataContext())
                    {
                        db.ProductType.Add(new ProductType()
                        {
                            Name = TypeProductTextBox.Text,
                            Square200 = Convert.ToInt32(Square200TextBox.Text),
                            Square400 = Convert.ToInt32(Square400TextBox.Text),
                            Square650 = Convert.ToInt32(Square650TextBox.Text),
                            Square800 = Convert.ToInt32(Square800TextBox.Text),
                            Square1000 = Convert.ToInt32(Square1000TextBox.Text),
                            Square2500 = Convert.ToInt32(Square2500TextBox.Text),
                            Square4000 = Convert.ToInt32(Square4000TextBox.Text),
                            Square6000 = Convert.ToInt32(Square6000TextBox.Text),
                            Square8000 = Convert.ToInt32(Square8000TextBox.Text)
                        });
                        db.SaveChanges();

                        this.Close();
                    }
                }
                else
                {
                    using (DataContext db = new DataContext())
                    {
                        var List = db.ProductType.Find(ID);
                        List.Name = TypeProductTextBox.Text;
                        List.Square200 = Convert.ToInt32(Square200TextBox.Text);
                        List.Square400 = Convert.ToInt32(Square400TextBox.Text);
                        List.Square650 = Convert.ToInt32(Square650TextBox.Text);
                        List.Square800 = Convert.ToInt32(Square800TextBox.Text);
                        List.Square1000 = Convert.ToInt32(Square1000TextBox.Text);
                        List.Square2500 = Convert.ToInt32(Square2500TextBox.Text);
                        List.Square4000 = Convert.ToInt32(Square4000TextBox.Text);
                        List.Square6000 = Convert.ToInt32(Square6000TextBox.Text);
                        List.Square8000 = Convert.ToInt32(Square8000TextBox.Text);
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
