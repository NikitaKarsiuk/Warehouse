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
    public partial class RealizeOrderWindow : Window
    {
        private int ID { get; set; }
        private UserInformation User { get; set; }
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

        public RealizeOrderWindow(UserInformation user, int ID = -1)
        {
            InitializeComponent();

            this.ID = ID;
            User = user;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (DataContext db = new DataContext())
            {
                nameColumn.ItemsSource = db.Product.Where(x => x.UserID == User.ID).ToList();

                if (ID != -1)
                {
                    var order = db.RealizeOrder.Find(ID);

                    PrintPriceTagButton.Visibility = Visibility.Visible;

                    ProductDataGrid.ItemsSource = order.RealizeOrderInfo.Where(x => x.OrderID == order.ID).Select(x => new OrderInfoData { ID = x.ID, Product = x.Product, UnitName = x.Product.Unit.Name, TypeName = x.Product.ProductType.Name, PackedName = x.Product.PackedType.Name, OrderCount = x.OrderCount, Sum = x.OrderCount * x.Product.Cost }).ToList();
                }
                else
                {
                    ProductDataGrid.ItemsSource = new List<OrderInfoData>() {  };
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
                var order = new RealizeOrder();

                using (DataContext db = new DataContext())
                {
                    if (ID == -1)
                    {
                        order = new RealizeOrder()
                        {
                            OrderDate = DateTime.Now,
                            UserID = User.ID
                        };

                        db.RealizeOrder.Add(order);
                    }
                    else
                    {
                        order = db.RealizeOrder.Find(ID);

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
                        var items = db.Product.Where(x => x.ID == obj.Product.ID && x.UserID == User.ID).Select(x => new { Name = x.Name, UnitID = x.Unit.ID, TypeID = x.TypeID, Count = (x.OrderInfo.Count == 0 ? 0 : x.OrderInfo.Sum(y => y.OrderCount)) - (x.RealizeOrderInfo.Where(y => y.ID != obj.ID).Count() == 0 ? 0 : x.RealizeOrderInfo.Where(y => y.ID != obj.ID).Sum(y => y.OrderCount)) - (x.MismatchInfo.Count == 0 ? 0 : x.MismatchInfo.Sum(y => y.OrderCount)) }).First();

                        if (items.Count - obj.OrderCount < 0)
                        {
                            throw new ArgumentException("Ошибка, на складе: " + items.Count + " " + obj.Product.Name);
                        }
                        if (obj.ID <= 0)
                        {
                            db.RealizeOrderInfo.Add(new RealizeOrderInfo()
                            {
                                RealizeOrder = order,
                                ProductID = obj.Product.ID,
                                OrderCount = obj.OrderCount,
                            });
                        }
                        else
                        {
                            var info = db.RealizeOrderInfo.Find(obj.ID);
                            info.RealizeOrder = order;
                            info.ProductID = Convert.ToInt32(obj.Product.ID);
                            info.OrderCount = obj.OrderCount;
                        }
                    }

                    db.SaveChanges();
                    
                }
                this.Close();
            }
            catch(Exception ex)
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

                        var type = db.Product.FirstOrDefault(x => x.ID == item.Product.ID).ProductType;
                        var unit = db.Product.FirstOrDefault(x => x.ID == item.Product.ID).Unit;
                        var packedname = db.Product.FirstOrDefault(x => x.ID == item.Product.ID).PackedType;

                        if (type != null)
                        {

                            item.TypeName = type.Name;
                            item.UnitName = unit.Name;
                            item.PackedName = packedname.Name;

                            var vat = double.Parse(db.Product.First(x => x.ID == item.Product.ID).Vat.Percents.ToString());
                            var sum = (item.Product.Wholesale / 100 * item.Product.Cost) + (item.Product.Trading / 100 * item.Product.Cost) + item.Product.Cost;
                            var cost = Math.Round(sum + (vat / 100 * sum), 2);

                            item.Sum = cost * item.OrderCount;

                            ProductDataGrid.ItemsSource = list;
                            ProductDataGrid.Items.Refresh();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PrintPriceTag_Click(object sender, RoutedEventArgs e)
        {
            var item = ProductDataGrid.SelectedItem as OrderInfoData;
            var wordApp = new Word.Application();
            wordApp.Visible = false;

            try
            {
                if (item != null)
                {
                    if (item.PackedName == "Нефасованный")
                    {
                        using (DataContext db = new DataContext())
                        {
                            var wordDocument = wordApp.Documents.Open($"{Environment.CurrentDirectory}/Templates/PriceTagN.docx");
                            var vat = double.Parse(db.Product.First(x => x.ID == item.Product.ID).Vat.Percents.ToString());
                            var sum = (item.Product.Wholesale / 100 * item.Product.Cost) + (item.Product.Trading / 100 * item.Product.Cost) + item.Product.Cost;
                            var cost = Math.Round(sum + (vat / 100 * sum), 2);
                            var org = db.Contractor.Where(x => x.UserID == User.ID).FirstOrDefault(x => x.ContractorType.Name == "Наша организация");
                            var unit = db.Unit.First(x => x.ID == item.Product.UnitID).Name;

                            if (org == null)
                                throw new ArgumentException("Ошибка. Вы не заполнили информацию об вашей организации!");

                            ReplaceWordStub("{Date}", DateTime.Now.ToString(), wordDocument);
                            ReplaceWordStub("{U}", unit, wordDocument);
                            ReplaceWordStub("{U1}", unit, wordDocument);
                            ReplaceWordStub("{ProductName}", item.Product.Name, wordDocument);
                            ReplaceWordStub("{OrganizationName}", org.Name, wordDocument);
                            ReplaceWordStub("{Cost}", cost.ToString(), wordDocument);
                            ReplaceWordStub("{Count}", item.OrderCount.ToString(), wordDocument);
                            ReplaceWordStub("{Sum}", Math.Round(cost * item.OrderCount, 2).ToString(), wordDocument);
                            ReplaceWordStub("{ProductStructure}", item.Product.Structure, wordDocument);

                            wordDocument.SaveAs2($"{Environment.CurrentDirectory}/Documents/PriceTag.docx");
                            wordApp.Visible = true;
                        }
                    }
                    else if (item.PackedName == "Фасованный")
                    {
                        using (DataContext db = new DataContext())
                        {
                            var wordDocument = wordApp.Documents.Open($"{Environment.CurrentDirectory}/Templates/PriceTagF.docx");
                            var vat = double.Parse(db.Product.First(x => x.ID == item.Product.ID).Vat.Percents.ToString());
                            var sum = (item.Product.Wholesale / 100 * item.Product.Cost) + (item.Product.Trading / 100 * item.Product.Cost) + item.Product.Cost;
                            var cost = Math.Round(sum + (vat / 100 * sum), 2);
                            var org = db.Contractor.Where(x => x.UserID == User.ID).FirstOrDefault(x => x.ContractorType.Name == "Наша организация");
                            var unit = db.Unit.First(x => x.ID == item.Product.UnitID).Name;

                            if (org == null)
                                throw new ArgumentException("Ошибка. Вы не заполнили информацию об вашей организации!");

                            ReplaceWordStub("{Date}", DateTime.Now.ToString(), wordDocument);
                            ReplaceWordStub("{U}", unit, wordDocument);
                            ReplaceWordStub("{ProductName}", item.Product.Name, wordDocument);
                            ReplaceWordStub("{OrganizationName}", org.Name, wordDocument);
                            ReplaceWordStub("{Cost}", cost.ToString(), wordDocument);
                            ReplaceWordStub("{Count}", item.OrderCount.ToString(), wordDocument);
                            ReplaceWordStub("{Sum}", Math.Round(cost * item.OrderCount, 2).ToString(), wordDocument);
                            ReplaceWordStub("{ProductStructure}", item.Product.Structure, wordDocument);

                            wordDocument.SaveAs2($"{Environment.CurrentDirectory}/Documents/PriceTag.docx");
                            wordApp.Visible = true;
                        }
                    }
                }
                else
                {
                    throw new ArgumentException("Ошибка. Вы не выбрали продукт");
                }
            }
            catch (Exception ex)
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
