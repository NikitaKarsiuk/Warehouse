using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;
using System.Collections.ObjectModel;
using TemplateProject.HelpClasses;
using System.Windows.Input;

namespace TemplateProject.Windows
{
    public partial class RequestWindow : Window
    {
        private List<OrderInfo> orderInfo_list = new List<OrderInfo>();
        private UserInformation User { get; set; }
        private int ID { get; set; }
        private enum Month
        {
            января = 1,
            февраля,
            марта,
            апреля,
            мая,
            июня,
            ююля,
            августа,
            сентября,
            октября,
            ноября,
            декабря
        }
        public RequestWindow(UserInformation user, int ID = -1)
        {
            InitializeComponent();

            this.ID = ID;
            User = user;

            ProductDataGrid.ItemsSource = new List<OrderInfo>();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (DataContext db = new DataContext())
            {
                ContractorComboBox.ItemsSource = db.Contractor.Where(x => x.ContractorType.Name == "Организация" && x.UserID == User.ID).ToList();
                EmployeeComboBox.ItemsSource = db.Employee.Where(x => x.Contractor.ContractorType.Name == "Наша организация" && x.Position.Name == "Директор" && x.UserID == User.ID).ToList();
                nameColumn.ItemsSource = db.Product.Where(x => x.UserID == User.ID).ToList();

                if (ID != -1)
                {
                    var order = db.ProductOrder.Find(ID);

                    PrintButton.Visibility = Visibility.Visible;
                    RequestButton.Visibility = Visibility.Visible;

                    EmployeeComboBox.SelectedItem = order.Employee1;
                    ContractorEmployeeComboBox.SelectedItem = order.Employee;
                    ContractorComboBox.SelectedItem = order.Contractor;
                    ProductDataGrid.ItemsSource = order.OrderInfo.Where(x => x.OrderID == order.ID).Select(x => new OrderInfoData { ID = x.ID, Product = x.Product, UnitName = x.Product.Unit.Name, TypeName = x.Product.ProductType.Name, PackedName = x.Product.PackedType.Name, OrderCount = x.OrderCount, Sum = x.OrderCount * x.Product.Cost }).ToList();
                }
                else
                {
                    ProductDataGrid.ItemsSource = new List<OrderInfoData>() { };
                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ProductDataGrid.SelectedItem == null)
                    throw new ArgumentException("Выберите строку");

                var items = ProductDataGrid.ItemsSource as List<Product>;
                var item = ProductDataGrid.SelectedItem as Product;

                using (DataContext db = new DataContext())
                {
                    var product = db.Product.Find(item.ID);

                    db.Product.Remove(product);
                    db.SaveChanges();
                }

                items.Remove(item);

                ProductDataGrid.ItemsSource = items;
                ProductDataGrid.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ContractorComboBox.Text == "")
                    throw new ArgumentException("Ошибка. Вы не выбрали поставщика");
                if (EmployeeComboBox.Text == "")
                    throw new ArgumentException("Ошибка. Вы не выбрали сотрудника");
                if (ContractorEmployeeComboBox.Text == "")
                    throw new ArgumentException("Ошибка. Вы не выбрали сотрудника от поставщика");

                var order = new ProductOrder();

                using (DataContext db = new DataContext())
                {
                    if (ID == -1)
                    {
                        order = new ProductOrder()
                        {
                            ContractorID = (ContractorComboBox.SelectedItem as Contractor).ID,
                            EmployeeID = (EmployeeComboBox.SelectedItem as Employee).ID,
                            ContractorEmployeeID = (ContractorEmployeeComboBox.SelectedItem as Employee).ID,
                            OrderDate = DateTime.Now,
                            UserID = User.ID
                        };

                        db.ProductOrder.Add(order);
                    }
                    else
                    {
                        order = db.ProductOrder.Find(ID);

                        order.ContractorID = (ContractorComboBox.SelectedItem as Contractor).ID;
                        order.EmployeeID = (EmployeeComboBox.SelectedItem as Employee).ID;
                        order.ContractorEmployeeID = (ContractorEmployeeComboBox.SelectedItem as Employee).ID;
                        order.OrderDate = DateTime.Now;
                    }

                    var list = ProductDataGrid.ItemsSource as List<OrderInfoData>;

                    if (list.Where(x => x.Product == null).Count() > 0)
                        throw new ArgumentException("Ошибка, в таблице существует пустая строка");

                    if (list.Where(x => x.OrderCount < 0).Count() > 0)
                        throw new ArgumentException("Ошибка, в строке количество введены некорректные данные");

                    if (list.Count() <= 0)
                        throw new ArgumentException("Ошибка, в таблице отсутствуют данные");

                    foreach (var obj in list)
                    {
                        if (obj.ID <= 0)
                        {
                            db.OrderInfo.Add(new OrderInfo()
                            {
                                ProductOrder = order,
                                ProductID = obj.Product.ID,
                                OrderCount = obj.OrderCount,
                            });
                        }
                        else
                        {
                            var info = db.OrderInfo.Find(obj.ID);
                            info.ProductOrder = order;
                            info.ProductID = Convert.ToInt32(obj.Product.ID);
                            info.OrderCount = Convert.ToInt32(obj.OrderCount);

                            var count = db.OrderInfo.Where(x => x.OrderID == ID && x.ProductID == obj.Product.ID).Select(x => new
                            {
                                Count =
                                (obj.OrderCount
                                - (x.Product.RealizeOrderInfo.Count == 0 ? 0 : x.Product.RealizeOrderInfo.Sum(y => y.OrderCount))
                                - (x.Product.MismatchInfo.Count == 0 ? 0 : x.Product.MismatchInfo.Sum(y => y.OrderCount)))
                            }).First();

                            if (count.Count < 0)
                            {
                                throw new ArgumentException("Ошибка, минимальное количество " + obj.Product.Name + " на складе получится " + count.Count);
                            }
                        }
                    }

                    foreach (var obj in orderInfo_list)
                    {
                        db.OrderInfo.Remove(db.OrderInfo.Find(obj.ID));
                    }

                    db.SaveChanges();
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            var wordApp = new Word.Application();
            wordApp.Visible = false;

            try
            {
                var wordDocument = wordApp.Documents.Open($"{Environment.CurrentDirectory}/Templates/Contract.docx");

                using (DataContext db = new DataContext())
                {
                    var order = db.ProductOrder.Find(ID);
                    var org = db.Contractor.FirstOrDefault(x => x.ContractorType.Name == "Наша организация");
                    var contractor = db.Contractor.First(x => x.Name == ContractorComboBox.Text);
                    var employee = db.Employee.Find((EmployeeComboBox.SelectedItem as Employee).ID);
                    var contractorEmployee = db.Employee.Find((ContractorEmployeeComboBox.SelectedItem as Employee).ID);

                    if (org == null)
                        throw new ArgumentException("Ошибка. Вы не заполнили информацию об вашей организации!");

                    ReplaceWordStub("{city}", org.Address.City, wordDocument);
                    ReplaceWordStub("{day}", order.OrderDate.Day.ToString(), wordDocument);
                    ReplaceWordStub("{month}", ((Month)order.OrderDate.Month).ToString(), wordDocument);
                    ReplaceWordStub("{year}", order.OrderDate.Year.ToString(), wordDocument);
                    ReplaceWordStub("{Day}", order.OrderDate.AddDays(15).Day.ToString(), wordDocument);
                    ReplaceWordStub("{Month}", ((Month)order.OrderDate.AddDays(15).Month).ToString(), wordDocument);
                    ReplaceWordStub("{Year}", order.OrderDate.AddDays(15).Year.ToString(), wordDocument);
                    ReplaceWordStub("{Organization}", org.Name, wordDocument);
                    ReplaceWordStub("{Employee}", employee.FIO, wordDocument);
                    ReplaceWordStub("{Contractor}", ContractorComboBox.Text, wordDocument);
                    ReplaceWordStub("{ContractorEmployee}", contractorEmployee.FIO, wordDocument);
                    ReplaceWordStub("{contractorEmployee}", contractorEmployee.FIO, wordDocument);
                    ReplaceWordStub("{contractorName}", ContractorComboBox.Text, wordDocument);
                    ReplaceWordStub("{contractorAddress}", $"г. {contractor.Address.City} {contractor.Address.Street} {contractor.Address.HouseNumber}", wordDocument);
                    ReplaceWordStub("{contractorDetails}", contractor.BankDetails, wordDocument);
                    ReplaceWordStub("{contractorUNP}", contractor.UNP, wordDocument);
                    ReplaceWordStub("{contractorNumber}", contractor.ContactNumber, wordDocument);
                    ReplaceWordStub("{Employee1}", employee.FIO, wordDocument);
                    ReplaceWordStub("{Name}", org.Name, wordDocument);
                    ReplaceWordStub("{Address}", $"г. {org.Address.City} {org.Address.Street} {org.Address.HouseNumber}", wordDocument);
                    ReplaceWordStub("{Details}", org.BankDetails, wordDocument);
                    ReplaceWordStub("{UNP}", org.UNP, wordDocument);
                    ReplaceWordStub("{Number}", org.ContactNumber, wordDocument);
                }

                wordDocument.SaveAs2($"{Environment.CurrentDirectory}/Documents/Contract.docx");
                wordApp.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ReplaceWordStub(string stubToReplace, string text, Word.Document wordDocument)
        {
            var range = wordDocument.Content;
            range.Find.ClearFormatting();
            range.Find.Execute(FindText: stubToReplace, ReplaceWith: text);
        }

        private void ProductDataGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                var list = ProductDataGrid.ItemsSource as List<OrderInfoData>;
                var info = ProductDataGrid.SelectedItem as OrderInfoData;

                using (DataContext db = new DataContext())
                {
                    var item = list.Find(x => x == info);

                    if (item != null && item.Product != null)
                    {
                        if (list.Count(x => x.Product == info.Product) > 1)
                        {
                            item.Product = null;

                            ProductDataGrid.ItemsSource = list;
                            ProductDataGrid.Items.Refresh();

                            throw new ArgumentException("Ошибка. Данный продукт уже существует в таблице");
                        }

                        item.TypeName = db.Product.First(x => x.ID == item.Product.ID).ProductType.Name;
                        item.UnitName = db.Product.First(x => x.ID == item.Product.ID).Unit.Name;
                        item.Sum = item.Product.Cost * item.OrderCount;

                        ProductDataGrid.ItemsSource = list;
                        ProductDataGrid.Items.Refresh();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RequestButton_Click(object sender, RoutedEventArgs e)
        {
            double amount = 0;
            double price = 0;
            double sum = 0;

            var wordApp = new Word.Application();
            wordApp.Visible = false;

            try
            {
                using (DataContext db = new DataContext())
                {
                    var wordDocument = wordApp.Documents.Open($"{Environment.CurrentDirectory}/Templates/Request.docx");
                    Word.Table table = wordDocument.Tables[1];

                    var org = db.Contractor.FirstOrDefault(x => x.ContractorType.Name == "Наша организация");
                    var list = db.OrderInfo.Where(x => x.OrderID == ID).ToList();
                    var order = db.ProductOrder.Find(ID);

                    if (org == null)
                        throw new ArgumentException("Ошибка. Вы не заполнили информацию об вашей организации!");

                    ReplaceWordStub("{num}", org.ID.ToString(), wordDocument);
                    ReplaceWordStub("{date}", order.OrderDate.ToShortDateString(), wordDocument);
                    ReplaceWordStub("{startDate}", order.OrderDate.ToShortDateString(), wordDocument);
                    ReplaceWordStub("{finishDate}", order.OrderDate.AddDays(15).ToShortDateString(), wordDocument);
                    ReplaceWordStub("{Date}", DateTime.Now.ToString(), wordDocument);
                    ReplaceWordStub("{OrganizationName}", org.Name, wordDocument);

                    for (int i = 0, count = 2; i < list.Count; i++, count++)
                    {
                        table.Cell(count, 1).Range.Text = (i + 1).ToString();
                        table.Cell(count, 2).Range.Text = list[i].Product.Name;
                        table.Cell(count, 3).Range.Text = list[i].Product.Unit.Name;
                        table.Cell(count, 4).Range.Text = list[i].OrderCount.ToString();
                        table.Cell(count, 5).Range.Text = list[i].Product.Cost.ToString();
                        table.Cell(count, 6).Range.Text = (list[i].Product.Cost * list[i].OrderCount).ToString();

                        amount += list[i].OrderCount;
                        price += list[i].Product.Cost;
                        sum += list[i].Product.Cost * list[i].OrderCount;

                        if (count <= list.Count)
                        {
                            table.Rows.Add();
                        }
                    }

                    table.Rows.Add();
                    table.Rows.Add();

                    table.Cell(table.Rows.Count, 2).Range.Text = "Итого";
                    table.Cell(table.Rows.Count, 4).Range.Text = amount.ToString();
                    table.Cell(table.Rows.Count, 5).Range.Text = price.ToString();
                    table.Cell(table.Rows.Count, 6).Range.Text = sum.ToString();

                    wordDocument.SaveAs2($"{Environment.CurrentDirectory}/Documents/Request.docx");
                    wordApp.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ContractorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (DataContext db = new TemplateProject.DataContext())
            {
                var contractor = ContractorComboBox.SelectedItem as Contractor;
                var employee = ContractorEmployeeComboBox.SelectedItem as Employee;
                var employees = db.Employee.Where(x => x.Contractor.ContractorType.Name == "Организация" && x.ContractorID == contractor.ID && x.Position.Name == "Директор" && x.UserID == User.ID).ToList();

                ContractorEmployeeComboBox.ItemsSource = employees;

                if (ID != -1)
                {
                    if (db.ProductOrder.Find(ID).Employee != null)
                        ContractorEmployeeComboBox.SelectedItem = db.ProductOrder.Find(ID).Employee;
                }

                if (employee != null)
                {
                    if (employees.FirstOrDefault(x => x.ID == employee.ID) != null)
                        ContractorComboBox.SelectedItem = employee;
                }
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
