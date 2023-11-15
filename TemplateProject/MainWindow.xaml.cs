using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Configuration;
using TemplateProject.Windows;
using Word = Microsoft.Office.Interop.Word;

namespace TemplateProject
{
    public partial class MainWindow : System.Windows.Window
    {
        private int ID { get; }
        private UserInformation User { get; set; }

        public void EmpoyeeDataGridView()
        {
            //using (DataContext db = new DataContext())
            //{
            //    if (ID != -1)
            //    {
            //        var contractor = db.Contractor.Find(ID);

            //        nameTextBox.Text = contractor.Name;
            //        typeComboBox.Text = contractor.ProductType.Name;
            //        phoneTextBox.Text = contractor.Phone;
            //        addressTextBox.Text = contractor.ContactAddress;

            //        foreach (var item in db.ContractorInfo.AsNoTracking().Where(x => x.ContractorID == contractor.ID).ToList())
            //        {
            //            data.Add(new InfoData()
            //            {
            //                ID = item.ID,
            //                Name = item.Product.Name,
            //                Cost = item.Cost,
            //                Count = item.ProductCount,
            //                Sum = item.Cost * item.ProductCount
            //            });
            //        }

            //        foreach (var item in data)
            //            nameComboBox.Items.Remove(item.Name);
            //    }

            //    EmployeeDataGridView.ItemsSource = data;
            //    contractorInfoDataGrid.Columns[1].Header = "Название";
            //    contractorInfoDataGrid.Columns[2].Header = "Количество";
            //    contractorInfoDataGrid.Columns[3].Header = "Цена";
            //    contractorInfoDataGrid.Columns[4].Header = "Стоимость";

            //    MainWindow.Column_Wrap(contractorInfoDataGrid);
            //    isLoaded = true;
            //}
        }

        public MainWindow(UserInformation user)
        {
            InitializeComponent();

            User = user;
        }

        private void EmployeeAddButton_Click(object sender, RoutedEventArgs e)
        {
            EmployeeWindow window = new EmployeeWindow(User);
            window.Closed += new EventHandler((_s, _e) =>
            {
                DirectoryTabItemFill("Сотрудники");
            });
            window.Show();
        }
        private void OrganizationAddButton_Click(object sender, RoutedEventArgs e)
        {
            OrganizationWindow window = new OrganizationWindow(User);
            window.Closed += new EventHandler((_s, _e) => 
            {
                DirectoryTabItemFill("Поставщики"); 
            });
            window.Show();
        }

        private void TypeProductAddButton_Click(object sender, RoutedEventArgs e)
        {
            TypeProductWindow window = new TypeProductWindow();
            window.Closed += new EventHandler((_s, _e) =>
            {
                DirectoryTabItemFill("Тип продукта");
            });
            window.Show();
        }

        private void ProductAddButton_Click(object sender, RoutedEventArgs e)
        {
            ProductWindow window = new ProductWindow(User);
            window.Closed += new EventHandler((_s, _e) =>
            {
                DirectoryTabItemFill("Товары");
            });
            window.Show();
        }

        private void PositionAddButton_Click(object sender, RoutedEventArgs e)
        {
            PositionWindow window = new PositionWindow(User);
            window.Closed += new EventHandler((_s, _e) =>
            {
                DirectoryTabItemFill("Должность");
            });
            window.Show();
        }

        private void VatAddButton_Click(object sender, RoutedEventArgs e)
        {
            VatWindow window = new VatWindow(User);
            window.Closed += new EventHandler((_s, _e) =>
            {
                DirectoryTabItemFill("НДС");
            });
            window.Show();
        }
        private void UnitAddButton_Click(object sender, RoutedEventArgs e)
        {
            UnitWindow window = new UnitWindow();
            window.Closed += new EventHandler((_s, _e) =>
            {
                DirectoryTabItemFill("Ед. измерения");
            });
            window.Show();
        }
        private void AddressAddButton_Click(object sender, RoutedEventArgs e)
        {
            AddressWindow window = new AddressWindow(User);
            window.Closed += new EventHandler((_s, _e) =>
            {
                DirectoryTabItemFill("Адрес");
            });
            window.Show();
        }
        private void CarAddButton_Click(object sender, RoutedEventArgs e)
        {
            CarWindow window = new CarWindow(User);
            window.Closed += new EventHandler((_s, _e) =>
            {
                DirectoryTabItemFill("Машины");
            });
            window.Show();
        }
        private void TrailerAddButton_Click(object sender, RoutedEventArgs e)
        {
            TrailerWindow window = new TrailerWindow(User);
            window.Closed += new EventHandler((_s, _e) =>
            {
                DirectoryTabItemFill("Прицепы");
            });
            window.Show();
        }
        private void TnProductAddButton_Click(object sender, RoutedEventArgs e)
        {
            TnWindow window = new TnWindow();
            window.Show();
        }

        private void TtnProductAddButton_Click(object sender, RoutedEventArgs e)
        {
            TtnWindow window = new TtnWindow(User);
            window.Closed += new EventHandler((_s, _e) =>
            {
                var tabControl = SecondaryTabControl2.Items;

                foreach (var obj in tabControl)
                {
                    var item = obj as TabItem;

                    if (item.IsSelected)
                        DocumentsTabItemFill(item.Header.ToString());
                }
            });
            window.Show();
        }

        private void ApplicationProductAddButton_Click(object sender, RoutedEventArgs e)
        {
            RequestWindow window = new RequestWindow(User);
            window.Closed += new EventHandler((_s, _e) =>
            {
                using (DataContext db = new DataContext())
                {
                    var items = db.ProductOrder.Where(x => x.UserID == User.ID).ToList();
                    orderDataGrid.ItemsSource = items;
                }
            });
            window.Show();
        }
        private void EmployeeDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (employeeDataGrid.SelectedItem == null)
                    throw new ArgumentException("Выберите строку");

                var items = employeeDataGrid.ItemsSource as List<Employee>;
                var item = employeeDataGrid.SelectedItem as Employee;

                using (DataContext db = new DataContext())
                {
                    var employee = db.Employee.Find(item.ID);

                    db.Employee.Remove(employee);
                    db.SaveChanges();
                }

                items.Remove(item);

                employeeDataGrid.ItemsSource = items;
                employeeDataGrid.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OrganizationDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (contractorDataGrid.SelectedItem == null)
                    throw new ArgumentException("Выберите строку");
                var items = contractorDataGrid.ItemsSource as List<Contractor>;
                var item = contractorDataGrid.SelectedItem as Contractor;

                using (DataContext db = new DataContext())
                {
                    var contractor = db.Contractor.Find(item.ID);

                    db.Contractor.Remove(contractor);
                    db.SaveChanges();
                }

                items.Remove(item);

                contractorDataGrid.ItemsSource = items;
                contractorDataGrid.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TypeProductDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (productTypeDataGrid.SelectedItem == null)
                    throw new ArgumentException("Выберите строку");

                var items = productTypeDataGrid.ItemsSource as List<ProductType>;
                var item = productTypeDataGrid.SelectedItem as ProductType;

                using (DataContext db = new DataContext())
                {
                    var productType = db.ProductType.Find(item.ID);

                    db.ProductType.Remove(productType);
                    db.SaveChanges();
                }

                items.Remove(item);

                productTypeDataGrid.ItemsSource = items;
                productTypeDataGrid.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void VatDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (vatDataGrid.SelectedItem == null)
                    throw new ArgumentException("Выберите строку");

                var items = vatDataGrid.ItemsSource as List<Vat>;
                var item = vatDataGrid.SelectedItem as Vat;

                using (DataContext db = new DataContext())
                {
                    var vat = db.Vat.Find(item.ID);

                    db.Vat.Remove(vat);
                    db.SaveChanges();
                }

                items.Remove(item);

                vatDataGrid.ItemsSource = items;
                vatDataGrid.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void UnitDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (unitDataGrid.SelectedItem == null)
                    throw new ArgumentException("Выберите строку");

                var items = unitDataGrid.ItemsSource as List<Unit>;
                var item = unitDataGrid.SelectedItem as Unit;

                using (DataContext db = new DataContext())
                {
                    var unit = db.Unit.Find(item.ID);

                    db.Unit.Remove(unit);
                    db.SaveChanges();
                }

                items.Remove(item);

                unitDataGrid.ItemsSource = items;
                unitDataGrid.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void AddressDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (addressDataGrid.SelectedItem == null)
                    throw new ArgumentException("Выберите строку");

                var items = addressDataGrid.ItemsSource as List<Address>;
                var item = addressDataGrid.SelectedItem as Address;

                using (DataContext db = new DataContext())
                {
                    var address = db.Address.Find(item.ID);

                    db.Address.Remove(address);
                    db.SaveChanges();
                }

                items.Remove(item);

                addressDataGrid.ItemsSource = items;
                addressDataGrid.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void CarDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (carDataGrid.SelectedItem == null)
                    throw new ArgumentException("Выберите строку");

                var items = carDataGrid.ItemsSource as List<Car>;
                var item = carDataGrid.SelectedItem as Car;

                using (DataContext db = new DataContext())
                {
                    var car = db.Car.Find(item.ID);

                    db.Car.Remove(car);
                    db.SaveChanges();
                }

                items.Remove(item);

                carDataGrid.ItemsSource = items;
                carDataGrid.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void TrailerDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (trailerDataGrid.SelectedItem == null)
                    throw new ArgumentException("Выберите строку");

                var items = trailerDataGrid.ItemsSource as List<Trailer>;
                var item = trailerDataGrid.SelectedItem as Trailer;

                using (DataContext db = new DataContext())
                {
                    var trailer = db.Trailer.Find(item.ID);

                    db.Trailer.Remove(trailer);
                    db.SaveChanges();
                }

                items.Remove(item);

                trailerDataGrid.ItemsSource = items;
                trailerDataGrid.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
       
        private void ProductDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (productDataGrid.SelectedItem == null)
                    throw new ArgumentException("Выберите строку");

                var items = productDataGrid.ItemsSource as List<Product>;
                var item = productDataGrid.SelectedItem as Product;

                using (DataContext db = new DataContext())
                {
                    var product = db.Product.Find(item.ID);

                    db.Product.Remove(product);
                    db.SaveChanges();
                }

                items.Remove(item);

                productDataGrid.ItemsSource = items;
                productDataGrid.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void EmployeeChangeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (employeeDataGrid.SelectedItem == null)
                    throw new ArgumentException("Выберите строку");

                var items = employeeDataGrid.ItemsSource as List<Employee>;
                var item = employeeDataGrid.SelectedItem as Employee;

                EmployeeWindow window = new EmployeeWindow(User, item.ID);
                window.Closed += new EventHandler((_s, _e) =>
                {
                    DirectoryTabItemFill("Сотрудники");
                });
                window.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OrganizationChangeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (contractorDataGrid.SelectedItem == null)
                    throw new ArgumentException("Выберите строку");

                var items = contractorDataGrid.ItemsSource as List<Contractor>;
                var item = contractorDataGrid.SelectedItem as Contractor;

                OrganizationWindow window = new OrganizationWindow(User, item.ID);

                window.Closed += new EventHandler((_s, _e) =>
                {
                    DirectoryTabItemFill("Поставщики");
                });
                window.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void VatChangeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (vatDataGrid.SelectedItem == null)
                    throw new ArgumentException("Выберите строку");
                var items = vatDataGrid.ItemsSource as List<Vat>;
                var item = vatDataGrid.SelectedItem as Vat;

                VatWindow window = new VatWindow(User, item.ID);
                window.Closed += new EventHandler((_s, _e) =>
                {
                    DirectoryTabItemFill("НДС");
                });
                window.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void UnitChangeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (unitDataGrid.SelectedItem == null)
                    throw new ArgumentException("Выберите строку");

                var items = unitDataGrid.ItemsSource as List<Unit>;
                var item = unitDataGrid.SelectedItem as Unit;

                UnitWindow window = new UnitWindow(item.ID);
                window.Closed += new EventHandler((_s, _e) =>
                {
                    DirectoryTabItemFill("Ед. измерения");
                });
                window.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void AddressChangeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (addressDataGrid.SelectedItem == null)
                    throw new ArgumentException("Выберите строку");

                var items = addressDataGrid.ItemsSource as List<Address>;
                var item = addressDataGrid.SelectedItem as Address;

                AddressWindow window = new AddressWindow(User, item.ID);
                window.Closed += new EventHandler((_s, _e) =>
                {
                    DirectoryTabItemFill("Адрес");
                });
                window.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void CarChangeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (carDataGrid.SelectedItem == null)
                    throw new ArgumentException("Выберите строку");
                var items = carDataGrid.ItemsSource as List<Car>;
                var item = carDataGrid.SelectedItem as Car;

                CarWindow window = new CarWindow(User, item.ID);
                window.Closed += new EventHandler((_s, _e) =>
                {
                    DirectoryTabItemFill("Машины");
                });
                window.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void TrailerChangeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (trailerDataGrid.SelectedItem == null)
                    throw new ArgumentException("Выберите строку");

                var items = trailerDataGrid.ItemsSource as List<Trailer>;
                var item = trailerDataGrid.SelectedItem as Trailer;

                TrailerWindow window = new TrailerWindow(User, item.ID);
                window.Closed += new EventHandler((_s, _e) =>
                {
                    DirectoryTabItemFill("Прицепы");
                });
                window.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void TypeProductChangeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (productTypeDataGrid.SelectedItem == null)
                    throw new ArgumentException("Выберите строку");

                var items = productTypeDataGrid.ItemsSource as List<ProductType>;
                var item = productTypeDataGrid.SelectedItem as ProductType;

                TypeProductWindow window = new TypeProductWindow(item.ID);
                window.Closed += new EventHandler((_s, _e) =>
                {
                    DirectoryTabItemFill("Тип продукта");
                });
                window.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ProductChangeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (productDataGrid.SelectedItem == null)
                    throw new ArgumentException("Выберите строку");

                var items = productDataGrid.ItemsSource as List<Product>;
                var item = productDataGrid.SelectedItem as Product;

                ProductWindow window = new ProductWindow(User, item.ID);
                window.Closed += new EventHandler((_s, _e) =>
                {
                    DirectoryTabItemFill("Товары");
                });
                window.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void DirectoryTabItemFill(string header)
        {
            using (DataContext db = new DataContext())
            {
                if (header == "Сотрудники")
                {
                    var items = db.Employee.Where(x => x.UserID == User.ID).ToList();

                    employeeDataGrid.ItemsSource = items;
                }
                else if (header == "Поставщики")
                {
                    var items = db.Contractor.Where(x => x.ContractorType.Name == "Организация" && x.UserID == User.ID).ToList();
                    contractorDataGrid.ItemsSource = items;
                }
                else if (header == "Тип продукта")
                {
                    var items = db.ProductType.ToList();

                    productTypeDataGrid.ItemsSource = items;
                }
                else if (header == "Товары")
                {
                    var items = db.Product.Where(x => x.UserID == User.ID).ToList();

                    productDataGrid.ItemsSource = items;
                }
                else if (header == "НДС")
                {
                    var items = db.Vat.Where(x => x.UserID == User.ID).ToList();

                    vatDataGrid.ItemsSource = items;
                }
                else if (header == "Ед. измерения")
                {
                    var items = db.Unit.ToList();

                    unitDataGrid.ItemsSource = items;
                }
                else if (header == "Адрес")
                {
                    var items = db.Address.Where(x => x.UserID == User.ID).ToList();

                    addressDataGrid.ItemsSource = items;
                }
                else if (header == "Машины")
                {
                    var items = db.Car.Where(x => x.UserID == User.ID).ToList();

                    carDataGrid.ItemsSource = items;
                }
                else if (header == "Прицепы")
                {
                    var items = db.Trailer.Where(x => x.UserID == User.ID).ToList();

                    trailerDataGrid.ItemsSource = items;
                }
                else if(header == "Склад")
                {
                    var items = db.Product.Where(x => x.UserID == User.ID).Select(x => new { Name = x.Name, UnitID = x.Unit.ID, TypeID = x.TypeID, Count = (x.OrderInfo.Count == 0 ? 0 : x.OrderInfo.Sum(y => y.OrderCount)) - (x.RealizeOrderInfo.Count == 0 ? 0 : x.RealizeOrderInfo.Sum(y => y.OrderCount)) - (x.MismatchInfo.Count == 0 ? 0 : x.MismatchInfo.Sum(y => y.OrderCount))}).ToList();
                    storeDataGrid.ItemsSource = items;
                }
            }
        }

        private void DirectoryTabControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var tabControl = SecondaryTabControl1.Items;

            foreach (var obj in tabControl)
            {
                var item = obj as TabItem;

                if (item.IsSelected)
                    DirectoryTabItemFill(item.Header.ToString());
            }
        }

        private void DirectoryTabItem_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var header = (sender as TabItem).Header.ToString();

            DirectoryTabItemFill(header);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (DataContext db = new DataContext())
            {
                AddressCombobox.ItemsSource = db.Address.Where(x => x.UserID == User.ID).ToList();
                ShopTypeComboBox.ItemsSource = db.ShopType.ToList();
                var org = db.Contractor.Where(x => x.UserID == User.ID).FirstOrDefault(x => x.ContractorType.Name == "Наша организация");

                if (org != null)
                {
                    NameTextBox.Text = org.Name;
                    SquareTextBox.Text = org.Square.ToString();
                    BankDetailsTextBox.Text = org.BankDetails;
                    UNPTextBox.Text = org.UNP;
                    OKPOTextBox.Text = org.OKPO;
                    ContactNumberTextBox.Text = org.ContactNumber;
                    AddressCombobox.SelectedItem = org.Address;
                    ShopTypeComboBox.SelectedItem = org.ShopType;
                }

                if (db.Position.Count() != 4)
                {
                    db.Position.RemoveRange(db.Position.ToList());

                    db.Position.AddRange(new List<Position>() {new Position()
                    {
                        Name = "Водитель"
                    },
                    new Position()
                    {
                        Name = "Директор"
                    },
                    
                    new Position()
                    {
                        Name = "Бухгалтер"
                    },
                    new Position()
                    {
                        Name = "Приемщик"
                    }});

                    db.SaveChanges();
                }

                if (db.Unit.Count() != 5)
                {
                    db.Unit.RemoveRange(db.Unit.ToList());

                    db.Unit.AddRange(new List<Unit>() {new Unit()
                    {
                        Name = "кг",
                    },
                    new Unit()
                    {
                        Name = "л"
                    },
                    new Unit()
                    {
                        Name = "упаковка"
                    },
                    new Unit()
                    {
                        Name = "шт"
                    },
                    new Unit()
                    {
                        Name = "банка"
                    }});

                    db.SaveChanges();
                }

                if (db.PackedType.Count() != 2)
                {
                    db.PackedType.RemoveRange(db.PackedType.ToList());

                    db.PackedType.AddRange(new List<PackedType>() {new PackedType()
                    {
                        Name = "Фасованный",
                    },
                    new PackedType()
                    {
                        Name = "Нефасованный"
                    }});

                    db.SaveChanges();
                }

                if (db.ContractorType.Count() != 2)
                {
                    db.ContractorType.RemoveRange(db.ContractorType.ToList());

                    db.ContractorType.AddRange(new List<ContractorType>() { new ContractorType()
                    {
                        Name = "Организация"
                    },
                    new ContractorType()
                    {
                        Name = "Наша организация"
                    } });
                    db.SaveChanges();
                }

                if (db.ShopType.Count() != 4)
                {
                    db.ShopType.RemoveRange(db.ShopType.ToList());

                    db.ShopType.AddRange(new List<ShopType>() { new ShopType()
                    {
                        Name = "Гастроном",
                        MinSquare = 200,
                        MaxSquare = 999
                    },
                    new ShopType()
                    {
                        Name = "Универсам",
                        MinSquare = 400,
                        MaxSquare = 2499
                    },
                    new ShopType()
                    {
                        Name = "Супермаркет",
                        MinSquare = 650,
                        MaxSquare = 3999
                    },
                    new ShopType()
                    {
                        Name = "Гипермаркет",
                        MinSquare = 4000,
                        MaxSquare = 50000
                    },});

                    db.ProductType.RemoveRange(db.ProductType.ToList());

                    using (StreamReader sr = new StreamReader($@"{Environment.CurrentDirectory}/productType.txt"))
                    {
                        db.Database.ExecuteSqlCommand(sr.ReadToEnd());

                        db.SaveChanges();
                    }

                    db.SaveChanges();
                }
            }
        }

        private void DocumentsTabItemFill(string header)
        {
            using (DataContext db = new DataContext())
            {
                if (header == "TH")
                {
                    //var items = db.RealizeOrder.ToList();

                    //tnDataGrid.ItemsSource = items;
                }
                else if (header == "TTH")
                {
                    var items = db.TTN.ToList();

                    ttnDataGrid.ItemsSource = items;
                }
            }
        }

        private void DocumentsTabControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var tabControl = SecondaryTabControl2.Items;

            foreach (var obj in tabControl)
            {
                var item = obj as TabItem;

                if (item.IsSelected)
                    DocumentsTabItemFill(item.Header.ToString());
            }
        }

        private void DocumentsTabItem_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                string path = $"{Environment.CurrentDirectory}/Documents";
                string[] list = Directory.GetFiles(path);

                var items = list.Where(x => Path.GetFileName(x).Substring(0, 2) != "~$").Select(x => Path.GetFileName(x)).ToList();

                documentsDataGrid.ItemsSource = items;
            }
            catch{ }
        }

        private void OrderTabItem_MouseUp(object sender, MouseButtonEventArgs e)
        {
            using (DataContext db = new DataContext())
            {
                var items = db.ProductOrder.Where(x => x.UserID == User.ID).ToList();

                orderDataGrid.ItemsSource = items;
            }
        }

        private void ApplicationChangeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (orderDataGrid.SelectedItem == null)
                    throw new ArgumentException("Выберите строку");
                var item = orderDataGrid.SelectedItem as ProductOrder;

                RequestWindow window = new RequestWindow(User, item.ID);
                window.Closed += new EventHandler((_s, _e) =>
                {
                    using (DataContext db = new DataContext())
                    {
                        var items = db.ProductOrder.Where(x => x.UserID == User.ID).ToList();
                        orderDataGrid.ItemsSource = items;
                    }
                });
                window.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ApplicationDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (orderDataGrid.SelectedItem == null)
                    throw new ArgumentException("Выберите строку");

                var items = orderDataGrid.ItemsSource as List<ProductOrder>;
                var item = orderDataGrid.SelectedItem as ProductOrder;

                using (DataContext db = new DataContext())
                {
                    var order = db.ProductOrder.Find(item.ID);

                    db.ProductOrder.Remove(order);

                    db.SaveChanges();
                }

                items.Remove(item);

                orderDataGrid.ItemsSource = items;
                orderDataGrid.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SaveOrganizationButton_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (NameTextBox.Text == "" || !Regex.IsMatch(NameTextBox.Text, @"^[ОАЗДУИП]{2,4}\s[«][А-яЁёA-z\s]{1,40}[»]$"))
                    throw new ArgumentException("Ошибка. Вы ввели некорректное значение поля наименование");
                if (BankDetailsTextBox.Text == "" || !Regex.IsMatch(BankDetailsTextBox.Text, @"^(BY20)[A-z]{4}[0-9]{20}$"))
                    throw new ArgumentException("Ошибка. Вы ввели некорректное значение поля Р/С");
                if (UNPTextBox.Text == "" || !Regex.IsMatch(UNPTextBox.Text, @"^[0-9]{9}$"))
                    throw new ArgumentException("Ошибка. Вы ввели некорректное значение поля УНП");
                if (ContactNumberTextBox.Text == "" || !Regex.IsMatch(ContactNumberTextBox.Text, @"^[+]375[(]((29)|(44)|(33)|(25)|(17))[)]\d{3}[-]\d{2}[-]\d{2}$"))
                    throw new ArgumentException("Ошибка. Вы ввели некорректный номер телефона");
                if (OKPOTextBox.Text == "" || !Regex.IsMatch(OKPOTextBox.Text, @"^[0-9]{9}$"))
                    throw new ArgumentException("Ошибка. Вы ввели некорректное значение поля ОКПО");
                if (AddressCombobox.Text == "")
                    throw new ArgumentException("Ошибка. Вы ввели некорректное значение поля адрес");
                if (SquareTextBox.Text == "" || !Regex.IsMatch(SquareTextBox.Text, @"^[0-9]*$"))
                    throw new ArgumentException("Ошибка. Вы ввели некорректное значение поля площади");
                if (ShopTypeComboBox.Text == "")
                    throw new ArgumentException("Ошибка. Вы ввели некорректное значение поля тип магазина");

                using (DataContext db = new DataContext())
                {
                    var org = db.Contractor.Where(x => x.UserID == User.ID).FirstOrDefault(x => x.ContractorType.Name == "Наша организация");

                    var square = Convert.ToInt32(SquareTextBox.Text);
                    var shoptypeid = (ShopTypeComboBox.SelectedItem as ShopType).ID;
                    var shoptype = db.ShopType.First(x => x.ID == shoptypeid);

                    if (org == null)
                        org = new Contractor();

                    if (square >= shoptype.MinSquare && square <= shoptype.MaxSquare)
                    {
                        org.Name = NameTextBox.Text;
                        org.Square = Convert.ToInt32(SquareTextBox.Text);
                        org.BankDetails = BankDetailsTextBox.Text;
                        org.UNP = UNPTextBox.Text;
                        org.OKPO = OKPOTextBox.Text;
                        org.ContactNumber = ContactNumberTextBox.Text;
                        org.AddressID = (AddressCombobox.SelectedItem as Address).ID;
                        org.ContractorTypeID = db.ContractorType.First(x => x.Name == "Наша организация").ID;
                        org.ShopTypeID = (ShopTypeComboBox.SelectedItem as ShopType).ID;
                        org.UserID = User.ID;
                    }
                    else
                    {
                        throw new ArgumentException("Ошибка, невозможно выбрать данный тип организации");
                    }

                    if (org.ID == 0)
                        db.Contractor.Add(org);

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OrganizationTabItem_MouseUp(object sender, MouseButtonEventArgs e)
        {
            using (DataContext db = new DataContext())
            {
                AddressCombobox.ItemsSource = db.Address.Where(x => x.UserID == User.ID).ToList();
                ShopTypeComboBox.ItemsSource = db.ShopType.ToList();
                var org = db.Contractor.Where(x => x.UserID == User.ID).FirstOrDefault(x => x.ContractorType.Name == "Наша организация");
                
                if (org != null)
                {
                    NameTextBox.Text = org.Name;
                    SquareTextBox.Text = org.Square.ToString();
                    BankDetailsTextBox.Text = org.BankDetails;
                    UNPTextBox.Text = org.UNP;
                    ContactNumberTextBox.Text = org.ContactNumber;
                    OKPOTextBox.Text = org.OKPO;
                    AddressCombobox.SelectedItem = org.Address;
                    ShopTypeComboBox.SelectedItem = org.ShopType;
                }
            }
        }

        private void RealizeOrderAddButton_Click(object sender, RoutedEventArgs e)
        {
            RealizeOrderWindow window = new RealizeOrderWindow(User);
            window.Closed += new EventHandler((_s, _e) =>
            {
                using (DataContext db = new DataContext())
                {
                    var items = db.RealizeOrder.Where(x => x.UserID == User.ID).ToList();
                    realizeOrderDataGrid.ItemsSource = items;
                }
            });
            window.ShowDialog();
        }

        private void RealizeOrderDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (realizeOrderDataGrid.SelectedItem == null)
                    throw new ArgumentException("Выберите строку");

                var items = realizeOrderDataGrid.ItemsSource as List<RealizeOrder>;
                var item = realizeOrderDataGrid.SelectedItem as RealizeOrder;

                using (DataContext db = new DataContext())
                {
                    var order = db.RealizeOrder.Find(item.ID);

                    db.RealizeOrder.Remove(order);

                    db.SaveChanges();
                }

                items.Remove(item);

                realizeOrderDataGrid.ItemsSource = items;
                realizeOrderDataGrid.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RealizeOrderChangeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (realizeOrderDataGrid.SelectedItem == null)
                    throw new ArgumentException("Выберите строку");

                var item = realizeOrderDataGrid.SelectedItem as RealizeOrder;

                RealizeOrderWindow window = new RealizeOrderWindow(User, item.ID);
                window.Closed += new EventHandler((_s, _e) =>
                {
                    using (DataContext db = new DataContext())
                    {
                        var items = db.RealizeOrder.Where(x => x.UserID == User.ID).ToList();
                        realizeOrderDataGrid.ItemsSource = items;
                    }
                });
                window.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RealizeOrderTabItem_MouseUp(object sender, MouseButtonEventArgs e)
        {
            using (DataContext db = new DataContext())
            {
                var items = db.RealizeOrder.Where(x => x.UserID == User.ID).ToList();

                realizeOrderDataGrid.ItemsSource = items;
            }
        }

        private void MismatchTabItem_MouseUp(object sender, MouseButtonEventArgs e)
        {
            using (DataContext db = new DataContext())
            {
                var items = db.Mismatch.Where(x => x.UserID == User.ID).ToList();

                mismatchDataGrid.ItemsSource = items;
            }
        }

        private void MismatchAddButton_Click(object sender, RoutedEventArgs e)
        {
            MismatchWindow window = new MismatchWindow(User);
            window.Closed += new EventHandler((_s, _e) =>
            {
                using (DataContext db = new DataContext())
                {
                    var items = db.Mismatch.Where(x => x.UserID == User.ID).ToList();
                    mismatchDataGrid.ItemsSource = items;
                }
            });
            window.ShowDialog();
        }

        private void MismatchDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (mismatchDataGrid.SelectedItem == null)
                    throw new ArgumentException("Выберите строку");

                var items = mismatchDataGrid.ItemsSource as List<Mismatch>;
                var item = mismatchDataGrid.SelectedItem as Mismatch;

                using (DataContext db = new DataContext())
                {
                    var order = db.Mismatch.Find(item.ID);

                    db.Mismatch.Remove(order);

                    db.SaveChanges();
                }

                items.Remove(item);

                mismatchDataGrid.ItemsSource = items;
                mismatchDataGrid.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MismatchChangeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (mismatchDataGrid.SelectedItem == null)
                    throw new ArgumentException("Выберите строку");
                var item = mismatchDataGrid.SelectedItem as Mismatch;

                MismatchWindow window = new MismatchWindow(User, item.ID);
                window.Closed += new EventHandler((_s, _e) =>
                {
                    using (DataContext db = new DataContext())
                    {
                        var items = db.Mismatch.Where(x => x.UserID == User.ID).ToList();
                        mismatchDataGrid.ItemsSource = items;
                    }
                });
                window.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RegisterAddButton_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow window = new RegisterWindow(User);
            window.Closed += new EventHandler((_s, _e) =>
            {
                using (DataContext db = new DataContext())
                {
                    var items = db.Register.Where(x => x.UserID == User.ID).ToList();
                    registerDataGrid.ItemsSource = items;
                }
            });
            window.ShowDialog();
        }

        private void RegisterDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (registerDataGrid.SelectedItem == null)
                    throw new ArgumentException("Выберите строку");

                var items = registerDataGrid.ItemsSource as List<Register>;
                var item = registerDataGrid.SelectedItem as Register;

                using (DataContext db = new DataContext())
                {
                    var register = db.Register.Find(item.ID);

                    db.Register.Remove(register);

                    db.SaveChanges();
                }

                items.Remove(item);

                registerDataGrid.ItemsSource = items;
                registerDataGrid.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RegisterChangeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (registerDataGrid.SelectedItem == null)
                    throw new ArgumentException("Выберите строку");

                var item = registerDataGrid.SelectedItem as Register;

                RegisterWindow window = new RegisterWindow(User, item.ID);
                window.Closed += new EventHandler((_s, _e) =>
                {
                    using (DataContext db = new DataContext())
                    {
                        var items = db.Register.Where(x => x.UserID == User.ID).ToList();
                        registerDataGrid.ItemsSource = items;
                    }
                });
                window.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RegisterTabItem_MouseUp(object sender, MouseButtonEventArgs e)
        {
            using (DataContext db = new DataContext())
            {
                var items = db.Register.Where(x => x.UserID == User.ID).ToList();

                registerDataGrid.ItemsSource = items;
            }
        }

        private void TtnProductChangeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ttnDataGrid.SelectedItem == null)
                    throw new ArgumentException("Выберите строку");

                var item = ttnDataGrid.SelectedItem as TTN;

                TtnWindow window = new TtnWindow(User, item.ID);
                window.Closed += new EventHandler((_s, _e) =>
                {
                    var tabControl = SecondaryTabControl2.Items;

                    foreach (var obj in tabControl)
                    {
                        var _item = obj as TabItem;

                        if (_item.IsSelected)
                            DocumentsTabItemFill(_item.Header.ToString());
                    }
                });
                window.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TtnProductDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ttnDataGrid.SelectedItem == null)
                    throw new ArgumentException("Выберите строку");

                var items = ttnDataGrid.ItemsSource as List<TTN>;
                var item = ttnDataGrid.SelectedItem as TTN;

                using (DataContext db = new DataContext())
                {
                    var register = db.TTN.Find(item.ID);

                    db.TTN.Remove(register);

                    db.SaveChanges();
                }

                items.Remove(item);

                ttnDataGrid.ItemsSource = items;
                ttnDataGrid.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void UpdateDocumentsDataGrid()
        {
            string path = $"{Environment.CurrentDirectory}/Documents";
            string[] list = Directory.GetFiles(path);

            var items = list.Where(x => Path.GetFileName(x).Substring(0, 2) != "~$").Select(x => Path.GetFileName(x)).ToList();

            documentsDataGrid.ItemsSource = items;
        }

        private void DocumentAddButton_Click(object sender, RoutedEventArgs e)
        {
            string path = $"{Environment.CurrentDirectory}/Documents";

            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Documents (*.docx;*.rtf;*.xlsx;*.xls)|*.docx;*.rtf;*.xlsx;*.xls";
            if (file.ShowDialog() == true)
            {
                File.Copy(file.FileName, Path.Combine(path, Path.GetFileName(file.FileName)), true);
            }
            UpdateDocumentsDataGrid();
        }

        private void DocumentDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var item = documentsDataGrid.SelectedItem;
                if (item != null)
                {
                    File.Delete($"{Environment.CurrentDirectory}/Documents/{item}");
                    UpdateDocumentsDataGrid();
                }
            }
            catch
            {
                throw new ArgumentException("Выберите строку");
            }
        }

        private void DocumentOpenButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var item = documentsDataGrid.SelectedItem;
                if (item != null)
                {
                    System.Diagnostics.Process.Start($"{Environment.CurrentDirectory}/Documents/{item}");
                }
            }
            catch
            {
                throw new ArgumentException("Выберите строку");
            }
        }

        private void AssListOpenButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start($"{Environment.CurrentDirectory}/Templates/Ассортиментный перечень.pdf");
            }
            catch { }
        }

        private void ReplaceWordStub(string stubToReplace, string text, Word.Document wordDocument)
        {
            var range = wordDocument.Content;
            range.Find.ClearFormatting();
            range.Find.Execute(FindText: stubToReplace, ReplaceWith: text);
        }

        private void AssortmentListTabItem_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var wordApp = new Word.Application();
            wordApp.Visible = false;

            try
            {
                using (DataContext db = new DataContext())
                {
                    var wordDocument = wordApp.Documents.Open($"{Environment.CurrentDirectory}/Templates/AssortList.docx");
                    Word.Table table = wordDocument.Tables[1];

                    var org = db.Contractor.Where(x => x.UserID == User.ID).FirstOrDefault(x => x.ContractorType.Name == "Наша организация");
                    var list = db.ProductType.Where(x => x.Product.Count > 0).ToList();

                    if (org == null)
                        throw new ArgumentException("Ошибка. Вы не заполнили информацию об вашей организации!");

                    ReplaceWordStub("{name}", org.Name, wordDocument);
                    ReplaceWordStub("{address}", $"{org.Address.City} {org.Address.Street} {org.Address.HouseNumber}", wordDocument);
                    ReplaceWordStub("{square}", org.Square.ToString(), wordDocument);

                    for (int i = 0, count = 2; i < list.Count; i++, count++)
                    {
                        table.Cell(count, 1).Range.Text = (i + 1).ToString();
                        table.Cell(count, 2).Range.Text = list[i].Name;

                        if (org.Square >= 200 && org.Square < 400)
                            table.Cell(count, 3).Range.Text = list[i].Square200.ToString();
                        else if (org.Square >= 400 && org.Square < 650)
                            table.Cell(count, 3).Range.Text = list[i].Square400.ToString();
                        else if (org.Square >= 650 && org.Square < 800)
                            table.Cell(count, 3).Range.Text = list[i].Square650.ToString();
                        else if (org.Square >= 800 && org.Square < 1000)
                            table.Cell(count, 3).Range.Text = list[i].Square800.ToString();
                        else if (org.Square >= 1000 && org.Square < 2500)
                            table.Cell(count, 3).Range.Text = list[i].Square1000.ToString();
                        else if (org.Square >= 2500 && org.Square < 4000)
                            table.Cell(count, 3).Range.Text = list[i].Square2500.ToString();
                        else if (org.Square >= 4000 && org.Square < 6000)
                            table.Cell(count, 3).Range.Text = list[i].Square4000.ToString();
                        else if (org.Square >= 6000 && org.Square < 8000)
                            table.Cell(count, 3).Range.Text = list[i].Square6000.ToString();
                        else if (org.Square >= 8000)
                            table.Cell(count, 3).Range.Text = list[i].Square8000.ToString();

                        if (count <= list.Count)
                            table.Rows.Add();
                    }

                    wordDocument.SaveAs2($"{Environment.CurrentDirectory}/Documents/AssortList.docx");
                    wordApp.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void StoreTabItem_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var header = (sender as TabItem).Header.ToString();

            DirectoryTabItemFill(header);
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
