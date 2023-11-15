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
    public partial class MismatchWindow : Window
    {
        private List<OrderInfo> orderInfo_list = new List<OrderInfo>();
        private UserInformation User { get; set; }
        private bool isLoaded { get; set; }
        private int ID { get; set; }
        private enum Month
        {
            Января = 1,
            Февраля,
            Марта,
            Апреля,
            Мая,
            Июня,
            Июля,
            Августа,
            Сентября,
            Октября,
            Ноября,
            Декабря
        }
        public MismatchWindow(UserInformation user, int ID = -1)
        {
            InitializeComponent();

            this.ID = ID;
            User = user;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (DataContext db = new DataContext())
            {
                if (ID != -1)
                {
                    var order = db.Mismatch.Find(ID);
                    nameColumn.ItemsSource = order.TTN.ProductOrder.OrderInfo.Select(x => x.Product).ToList();
                    TtnComboBox.ItemsSource = db.TTN.Where(x => x.Mismatch.Where(y => y.TtnID == x.ID).Count() == 0 && x.UserID == User.ID || x.ID == order.TtnID && x.UserID == User.ID).ToList();
                    TtnComboBox.SelectedItem = order.TTN;
                    ProductDataGrid.ItemsSource = order.MismatchInfo.Where(x => x.MismatchID == order.ID).Select(x => new OrderInfoData { ID = x.ID, Product = x.Product, TypeName = x.Product.ProductType.Name, UnitName = x.Product.Unit.Name, OrderCount = x.OrderCount, Sum = x.OrderCount * x.Product.Cost }).ToList();
                }
                else
                {
                    TtnComboBox.ItemsSource = db.TTN.Where(x => x.Mismatch.Where(y => y.TtnID == x.ID).Count() == 0 && x.UserID == User.ID).ToList();                    
                    ProductDataGrid.ItemsSource = new List<OrderInfoData>() { };
                }

                isLoaded = true;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                if (TtnComboBox.Text == "")
                    throw new ArgumentException("Ошибка. Вы не выбрали ТТН");

                MessageBox.Show(ProductDataGrid.Items.Count.ToString());

                var order = new Mismatch();

                using (DataContext db = new DataContext())
                {
                    if (ID == -1)
                    {
                        order = new Mismatch()
                        {
                            TtnID = (TtnComboBox.SelectedItem as TTN).ID,
                            Date = DateTime.Now,
                            UserID = User.ID
                        };

                        db.Mismatch.Add(order);
                    }
                    else
                    {
                        order = db.Mismatch.Find(ID);
                        order.TtnID = (TtnComboBox.SelectedItem as TTN).ID;
                        order.Date = DateTime.Now;
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
                        var orderInfo = db.TTN.Find((TtnComboBox.SelectedItem as TTN).ID).ProductOrder.OrderInfo.Where(x => x.ProductID == obj.Product.ID);
                        var count = 0.0;

                        db.MismatchInfo.Add(new MismatchInfo()
                        {
                            Mismatch = order,
                            ProductID = obj.Product.ID,
                            OrderCount = obj.OrderCount,
                        });

                        if (orderInfo.Count() > 0)
                        {
                            count = orderInfo.Sum(x => x.OrderCount);
                        }
                        if (count - obj.OrderCount < 0)
                        {
                            throw new ArgumentException("Ошибка, на складе: " + count + " " + obj.Product.Name);
                        }
                    }

                    db.MismatchInfo.RemoveRange(db.MismatchInfo.Where(x => x.MismatchID == ID).ToList());

                    db.SaveChanges();
                }

                this.Close();
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

        public void InsertRow(int rowNum, Excel.Worksheet _workSheet)
        {
            Excel.Range cellRange = (Excel.Range)_workSheet.Cells[rowNum, 1];
            Excel.Range rowRange = cellRange.EntireRow;
            rowRange.Insert(Excel.XlInsertShiftDirection.xlShiftDown, false);
            Excel.Range _excelCells = (Excel.Range)_workSheet.get_Range($"C{rowNum}", $"H{rowNum}").Cells;
            _excelCells.Merge(Type.Missing);
            _excelCells = (Excel.Range)_workSheet.get_Range($"I{rowNum}", $"K{rowNum}").Cells;
            _excelCells.Merge(Type.Missing);
            _excelCells = (Excel.Range)_workSheet.get_Range($"L{rowNum}", $"N{rowNum}").Cells;
            _excelCells.Merge(Type.Missing);
            _excelCells = (Excel.Range)_workSheet.get_Range($"O{rowNum}", $"Q{rowNum}").Cells;
            _excelCells.Merge(Type.Missing);
            _excelCells = (Excel.Range)_workSheet.get_Range($"R{rowNum}", $"U{rowNum}").Cells;
            _excelCells.Merge(Type.Missing);
            _excelCells = (Excel.Range)_workSheet.get_Range($"V{rowNum}", $"Y{rowNum}").Cells;
            _excelCells.Merge(Type.Missing);
            _excelCells = (Excel.Range)_workSheet.get_Range($"Z{rowNum}", $"AC{rowNum}").Cells;
            _excelCells.Merge(Type.Missing);
            _excelCells = (Excel.Range)_workSheet.get_Range($"AD{rowNum}", $"AG{rowNum}").Cells;
            _excelCells.Merge(Type.Missing);
            _excelCells = (Excel.Range)_workSheet.get_Range($"AH{rowNum}", $"AK{rowNum}").Cells;
            _excelCells.Merge(Type.Missing);
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
            catch(Exception ex)
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
                using (DataContext db = new DataContext())
                {
                    var act = db.TTN.Find((TtnComboBox.SelectedItem as TTN).ID).Mismatch.First().MismatchInfo.ToList();
                    var wordDocument = wordApp.Documents.Open($"{Environment.CurrentDirectory}/Templates/Act.docx");
                    Word.Table table = wordDocument.Tables[1];

                    var order = db.Contractor.First(x => x.ContractorType.Name == "Наша организация");

                    ReplaceWordStub("{ShopType}", order.ShopType.Name , wordDocument);
                    ReplaceWordStub("{Name}", order.Name, wordDocument);
                    ReplaceWordStub("{Fio}", db.Employee.First(x => x.Position.Name == "Директор" && x.ContractorID == order.ID).FIO , wordDocument);
                    ReplaceWordStub("{Date}", DateTime.Now.ToShortDateString(), wordDocument);
                    ReplaceWordStub("{TtnNum}", (TtnComboBox.SelectedItem as TTN).ID.ToString(), wordDocument);
                    ReplaceWordStub("{ActNum}", act.First().MismatchID.ToString(), wordDocument);

                    for (int i = 0, count = 2; i < act.Count; i++, count++)
                    {
                        table.Cell(count, 1).Range.Text = (i + 1).ToString();
                        table.Cell(count, 2).Range.Text = act[i].Product.Name;
                        table.Cell(count, 3).Range.Text = act[i].Product.Unit.Name;
                        table.Cell(count, 4).Range.Text = act[i].OrderCount.ToString();
                        table.Cell(count, 5).Range.Text = act[i].Product.Cost.ToString();
                        table.Cell(count, 6).Range.Text = (act[i].OrderCount * act[i].Product.Cost).ToString();

                        if (count <= act.Count)
                            table.Rows.Add();
                    }

                    wordDocument.SaveAs2($"{Environment.CurrentDirectory}/Documents/Act.docx");
                    wordApp.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TtnComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (isLoaded)
            {
                using (DataContext db = new DataContext())
                {
                    nameColumn.ItemsSource = db.TTN.Find((TtnComboBox.SelectedItem as TTN).ID).ProductOrder.OrderInfo.Select(x => x.Product).ToList();

                    ProductDataGrid.ItemsSource = new List<OrderInfoData>() { };
                    ProductDataGrid.Items.Refresh();
                }
            }
        }

        private void ProductDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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
