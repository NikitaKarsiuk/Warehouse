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
using System.Configuration;


namespace TemplateProject.Windows
{
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();

            string ServerInfo = string.Empty;
            var list = RegistryValueDataReader.GetLocalSqlServerInstanceNames();
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var path = $"data source={Environment.MachineName};initial catalog=TradeOrganizationdb;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
            var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");

            if (connectionStringsSection.ConnectionStrings["DataContext"].ConnectionString != path)
            {
                connectionStringsSection.ConnectionStrings["DataContext"].ConnectionString = path;
                config.Save();
                System.Windows.Forms.Application.Restart();
                System.Windows.Application.Current.Shutdown();
            }
        }

        private void Page_Switch(Grid page)
        {
            List<Grid> grids = new List<Grid>()
            {
                AuthPage,
                RegPage
            };

            foreach (var grid in grids)
            {
                if (grid == page)
                    page.Visibility = Visibility.Visible;
                else
                    grid.Visibility = Visibility.Hidden;
            }
        }

        private void AuthButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    var user = db.UserInformation.FirstOrDefault(x => x.Login == AuthLoginTextBox.Text && x.Password == AuthPassPasswordBox.Password);

                    if (user != null)
                    {
                        this.Hide();
                        MainWindow window = new MainWindow(user);
                        window.Closed += new EventHandler((_s, _e) => { this.ShowDialog(); });
                        window.ShowDialog();
                    }
                    else
                    {
                        throw new ArgumentException("Логин или пароль введены неверно!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RegPageButton_Click(object sender, RoutedEventArgs e)
        {
            Page_Switch(RegPage);
        }

        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    if (db.UserInformation.Where(x => x.Login == RegLoginTextBox.Text).Count() > 0)
                        throw new ArgumentException("Данный логин уже существует");

                    if (RegPassPasswordBox.Password.Length < 6)
                        throw new ArgumentException("Ошибка. Пароль должен содержать не менее 6 символов!");

                    if (RegLoginTextBox.Text == string.Empty)
                        throw new ArgumentException("Ошибка. Вы не ввели логин!");

                    var user = new UserInformation()
                    {
                        Login = RegLoginTextBox.Text,
                        Password = RegPassPasswordBox.Password
                    };

                    db.UserInformation.Add(user);

                    db.SaveChanges();

                    MessageBox.Show("Профиль успешно создан");

                    AuthPageButton_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AuthPageButton_Click(object sender, RoutedEventArgs e)
        {
            Page_Switch(AuthPage);
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
