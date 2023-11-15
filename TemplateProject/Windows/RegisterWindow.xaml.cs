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
    public partial class RegisterWindow : Window
    {
        private int ID { get; set; }
        private UserInformation User { get; set; }

        public RegisterWindow(UserInformation user, int ID = -1)
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
                    var register = db.Register.Find(ID);

                    TtnComboBox.ItemsSource = db.TTN.Where(x => x.Register.Where(y => y.TtnID == x.ID).Count() == 0 && x.UserID == User.ID || x.ID == register.TtnID && x.UserID == User.ID).ToList();
                    TtnComboBox.SelectedItem = register.TTN;
                }
                else
                {
                    TtnComboBox.ItemsSource = db.TTN.Where(x => x.Register.Where(y => y.TtnID == x.ID).Count() == 0 && x.UserID == User.ID).ToList();
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TtnComboBox.Text == "")
                    throw new ArgumentException("Ошибка. Вы не выбрали ТТН");

                var register = new Register();

                using (DataContext db = new DataContext())
                {
                    if (ID == -1)
                    {
                        register = new Register()
                        {
                            TtnID = (TtnComboBox.SelectedItem as TTN).ID,
                            UserID = User.ID
                        };

                        db.Register.Add(register);
                    }
                    else
                    {
                        register = db.Register.Find(ID);

                        register.TtnID = (TtnComboBox.SelectedItem as TTN).ID;
                        register.UserID = User.ID;
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

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            var wordApp = new Word.Application();
            wordApp.Visible = false;

            try
            {
                using (DataContext db = new DataContext())
                {
                    var ttn = db.TTN.Find((TtnComboBox.SelectedItem as TTN).ID).ProductOrder.OrderInfo;
                    var act = db.TTN.Find((TtnComboBox.SelectedItem as TTN).ID).Mismatch.First().MismatchInfo;
                    var list = ttn.Select(x => new OrderInfoData { OrderCount = x.OrderCount - act.Where(y => y.ProductID == x.ProductID).Sum(y => y.OrderCount), Product = x.Product }).ToList();
                    var wordDocument = wordApp.Documents.Open($"{Environment.CurrentDirectory}/Templates/Register.docx");
                    Word.Table table = wordDocument.Tables[1];

                    var org = db.Contractor.FirstOrDefault(x => x.ContractorType.Name == "Наша организация");

                    double amount = 0;
                    double price = 0;
                    double wholesale = 0;
                    double trading = 0;
                    double vat = 0;
                    double fullprice = 0;
                    double fullresult = 0;

                    if (org == null)
                        throw new ArgumentException("Ошибка. Вы не заполнили информацию об вашей организации!");

                    ReplaceWordStub("{OrganizationName}", org.Name, wordDocument);
                    ReplaceWordStub("{MarketType}", org.ShopType.Name, wordDocument);
                    ReplaceWordStub("{Address}", $"{org.Address.Street}, {org.Address.HouseNumber}" , wordDocument);
                    ReplaceWordStub("{Num}", ID.ToString(), wordDocument);
                    ReplaceWordStub("{Ttn}", (TtnComboBox.SelectedItem as TTN).ID.ToString(), wordDocument);
                    ReplaceWordStub("{Date}", DateTime.Now.ToShortDateString(), wordDocument);

                    for (int i = 0, count = 3; i < list.Count; i++, count++)
                    {
                        var sum = (list[i].Product.Wholesale / 100 * list[i].Product.Cost) + (list[i].Product.Trading / 100 * list[i].Product.Cost) + list[i].Product.Cost;
                        var cost = Math.Round(sum + (Convert.ToDouble(list[i].Product.Vat.Percents) / 100 * sum), 2);
                        var result = Math.Round(cost * list[i].OrderCount, 2);

                        table.Cell(count, 1).Range.Text = (i + 1).ToString();
                        table.Cell(count, 2).Range.Text = "ТТН №" + (TtnComboBox.SelectedItem as TTN).ID;
                        table.Cell(count, 3).Range.Text = list[i].Product.Name;
                        table.Cell(count, 4).Range.Text = list[i].Product.Unit.Name;
                        table.Cell(count, 5).Range.Text = list[i].OrderCount.ToString();
                        table.Cell(count, 6).Range.Text = list[i].Product.Cost.ToString();
                        table.Cell(count, 7).Range.Text = list[i].Product.Wholesale.ToString();
                        table.Cell(count, 8).Range.Text = Math.Round(list[i].Product.Wholesale / 100 * list[i].Product.Cost, 2).ToString();
                        table.Cell(count, 9).Range.Text = list[i].Product.Trading.ToString();
                        table.Cell(count, 10).Range.Text = Math.Round(list[i].Product.Trading / 100 * list[i].Product.Cost, 2).ToString();
                        table.Cell(count, 11).Range.Text = list[i].Product.Vat.Percents.ToString();
                        table.Cell(count, 12).Range.Text = Math.Round(Convert.ToDouble(list[i].Product.Vat.Percents) / 100 * sum, 2).ToString();
                        table.Cell(count, 13).Range.Text = cost.ToString();
                        table.Cell(count, 14).Range.Text = result.ToString();

                        amount += list[i].OrderCount;
                        price += list[i].Product.Cost;
                        wholesale += Math.Round(list[i].Product.Wholesale / 100 * list[i].Product.Cost, 2);
                        trading += Math.Round(list[i].Product.Trading / 100 * list[i].Product.Cost, 2);
                        vat += Math.Round(Convert.ToDouble(list[i].Product.Vat.Percents) / 100 * sum, 2);
                        fullprice += cost;
                        fullresult += result;

                        if (count <= list.Count + 1)
                                table.Rows.Add();
                    }

                    table.Rows.Add();

                    table.Cell(table.Rows.Count, 3).Range.Text = "Итого";
                    table.Cell(table.Rows.Count, 5).Range.Text = amount.ToString();
                    table.Cell(table.Rows.Count, 6).Range.Text = price.ToString();
                    table.Cell(table.Rows.Count, 8).Range.Text = wholesale.ToString();
                    table.Cell(table.Rows.Count, 10).Range.Text = trading.ToString();
                    table.Cell(table.Rows.Count, 13).Range.Text = fullprice.ToString();
                    table.Cell(table.Rows.Count, 14).Range.Text = fullresult.ToString();

                    wordDocument.SaveAs2($"{Environment.CurrentDirectory}/Documents/Register.docx");
                    wordApp.Visible = true;
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
