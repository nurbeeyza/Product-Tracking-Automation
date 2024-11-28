using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using Application = System.Windows.Forms.Application;
using ExcelApplication = Microsoft.Office.Interop.Excel.Application;

namespace Product_Tracking_Automation
{
    public partial class Form2 : Form
    {
        public UserController _userController;
        public BrandController _brandController;
        public ProductController _productController;
        public StockController _stockController;
        public CompanyController _companyController;
        public SupplierController _supplierController;
        public ExcellFunctions _excelfunctions;
        public PdfFunctions _pdfFunctions;
        

        private string _currentBrandName;


        public Form2()
        {

            InitializeComponent();
            _userController = new UserController(DBConnections.DbConnectionString);
            _brandController = new BrandController(DBConnections.DbConnectionString);
            _productController = new ProductController(DBConnections.DbConnectionString);
            _stockController = new StockController(DBConnections.DbConnectionString);
            _companyController = new CompanyController(DBConnections.DbConnectionString);
            _supplierController = new SupplierController(DBConnections.DbConnectionString);
            _excelfunctions = new ExcellFunctions();
            _pdfFunctions=new PdfFunctions();




            SetupDataGridView();


            //panel1.BackColor = Color.FromArgb(181, 50, 50)



        }
        private void SetupDataGridView()
        {
            // Initialize DataGridView1
            InitializeDataGridView(TC__Active_DGV, "Deactivate", "Pasif et");
            // Initialize DataGridView2
            InitializeDataGridView(TC_Passive_DGV, "Activate", "Aktif et");
            // Initialize DataGridView3
            InitializeDataGridView(TC_Approve_DGV, "Approve", "Onayla ");
            InitializeDataGridView(Brand_DGV, "Approve", "Onayla ");




        }

        private void InitializeDataGridView(DataGridView dataGridView, string btn_name, string btn_text)
        {
            dataGridView.Columns.Clear();
            dataGridView.Rows.Clear();

            if (dataGridView == Brand_DGV)
            {
                dataGridView.Columns.Add("Id", "Id");
                dataGridView.Columns.Add("Name", "Name");


                DataGridViewButtonColumn deleteColumn = new DataGridViewButtonColumn
                {
                    Name = "Delete",
                    Text = "Sil",
                    UseColumnTextForButtonValue = true,
                    DefaultCellStyle = new DataGridViewCellStyle { ForeColor = Color.Red }
                };
                dataGridView.Columns.Add(deleteColumn);

                // Add Update button column
                DataGridViewButtonColumn updateColumn = new DataGridViewButtonColumn
                {
                    Name = "Update",
                    Text = "Güncelle",
                    UseColumnTextForButtonValue = true,
                    DefaultCellStyle = new DataGridViewCellStyle { ForeColor = Color.Blue }
                };
                dataGridView.Columns.Add(updateColumn);

                dataGridView.Columns["Name"].MinimumWidth = 150;
                dataGridView.Columns["Delete"].MinimumWidth = 100;
                dataGridView.Columns["Update"].MinimumWidth = 100;

                dataGridView.Columns["Name"].FillWeight = 2;
                dataGridView.Columns["Delete"].FillWeight = 1;
                dataGridView.Columns["Update"].FillWeight = 1;
                dataGridView.Columns["Id"].Visible = false;
            }
            else
            {
                dataGridView.Columns.Add("Name", "Name");
                dataGridView.Columns.Add("Surname", "Surname");
                dataGridView.Columns.Add("UserName", "Username");

                DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn
                {
                    Name = btn_name,
                    Text = btn_text,
                    UseColumnTextForButtonValue = true,
                    DefaultCellStyle = new DataGridViewCellStyle { ForeColor = Color.Red }
                };

                dataGridView.Columns.Add(buttonColumn);

                dataGridView.Columns["Name"].MinimumWidth = 150;
                dataGridView.Columns["Surname"].MinimumWidth = 150;
                dataGridView.Columns["UserName"].MinimumWidth = 150;
                dataGridView.Columns[btn_name].MinimumWidth = 150;

                dataGridView.Columns["Name"].FillWeight = 2;
                dataGridView.Columns["Surname"].FillWeight = 2;
                dataGridView.Columns["UserName"].FillWeight = 2;
                dataGridView.Columns[btn_name].FillWeight = 1;
            }
        }



        private void Menu_Users_btn_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = UserPage;
        }
        private void Menu_Supplier_btn_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = SupplierPage;
        }
        private void TC_btn_ActiveUsers_Click(object sender, EventArgs e)
        {



            TC_Users.SelectedTab = TC_Users_TP_Active;
        }


        private void TC_btn_PassiveUsers_Click(object sender, EventArgs e)
        {
            TC_Users.SelectedTab = TC_Users_TP_Passive;
        }


        private void TC_btn_WaitingApprovalUsers_Click(object sender, EventArgs e)
        {

            TC_Users.SelectedTab = TC_Users_TP_Waiting;
        }
        private void Active_searchtext_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                var activeUsers = _userController.GetActiveUsers(Active_searchtext.Text);


                TC__Active_DGV.Rows.Clear();

                foreach (var user in activeUsers)
                {
                    TC__Active_DGV.Rows.Add(user.Name, user.Surname, user.UserName);
                }


                TC_Users.SelectedTab = TC_Users_TP_Active;
            }
        }

        private void Passive_searchbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var inactiveUsers = _userController.GetInactiveUsers(Passive_searchbox.Text);

                TC_Passive_DGV.Rows.Clear();

                foreach (var user in inactiveUsers)
                {
                    TC_Passive_DGV.Rows.Add(user.Name, user.Surname, user.UserName);
                }


                TC_Users.SelectedTab = TC_Users_TP_Passive;
            }
        }

        private void Approve_searchbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var waitingApproveUsers = _userController.GetWaitingApproveUsers(Approve_searchbox.Text);

                TC_Approve_DGV.Rows.Clear();

                foreach (var user in waitingApproveUsers)
                {
                    TC_Approve_DGV.Rows.Add(user.Name, user.Surname, user.UserName);
                }


                TC_Users.SelectedTab = TC_Users_TP_Waiting;
            }

        }

        private void TC__Active_DGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }



        private void Menu_Exit_btn_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Restart();
        }

        private void active_search_btn_Click(object sender, EventArgs e)
        {
            string name = ActiveUsers_TBox_Name.Text;
            string surname = ActiveUsers_TBox_Surname.Text;
            string username = ActiveUsers_TBox_Username.Text;

            User nUser = new User
            {
                UserName = username,
                Name = name,
                Surname = surname
            };


            List<User> testactive = _userController.GetActiveUsers(nUser);
            TC__Active_DGV.Rows.Clear();

            foreach (var usr in testactive)
            {
                TC__Active_DGV.Rows.Add(usr.Name, usr.Surname, usr.UserName);
            }
        }
        private void TC__Active_DGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0 && e.ColumnIndex == TC__Active_DGV.Columns["deactivate"].Index)
            {

                var row = TC__Active_DGV.Rows[e.RowIndex];
                string username = row.Cells["UserName"].Value.ToString();


                _userController.DeactivateUser(username);


                TC__Active_DGV.Rows.RemoveAt(e.RowIndex);

                MessageBox.Show($"Kullanıcı {username} başarıyla pasif hale getirildi.");
            }
        }

        private void passive_search_btn_Click(object sender, EventArgs e)
        {
            string name = PassiveUsers_TBox_Name.Text;
            string surname = PassiveUsers_TBox_Surname.Text;
            string username = PassiveUsers_TBox_Username.Text;

            User pUser = new User
            {
                UserName = PassiveUsers_TBox_Username.Text,
                Name = PassiveUsers_TBox_Name.Text,
                Surname = PassiveUsers_TBox_Surname.Text
            };


            List<User> testpassive = _userController.GetInaciveUsers(pUser);
            TC_Passive_DGV.Rows.Clear();

            foreach (var usr in testpassive)
            {
                TC_Passive_DGV.Rows.Add(usr.Name, usr.Surname, usr.UserName);
            }
        }

        private void TC_Passive_DGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == TC_Passive_DGV.Columns["activate"].Index && e.RowIndex >= 0)
            {

                string username = TC_Passive_DGV.Rows[e.RowIndex].Cells["UserName"].Value.ToString();


                _userController.ActivateUser(username);


            }
        }

        private void waiting_search_btn_Click(object sender, EventArgs e)
        {
            string name = WaitingUsers_TBox_Name.Text;
            string surname = WaitingUsers_TBox_Surname.Text;
            string username = WaitingUsers_TBox_Username.Text;

            User pUser = new User
            {
                UserName = WaitingUsers_TBox_Username.Text,
                Name = WaitingUsers_TBox_Name.Text,
                Surname = WaitingUsers_TBox_Surname.Text
            };


            List<User> testpassive = _userController.GetWaitingApproveUsers(pUser);
            TC_Approve_DGV.Rows.Clear();

            foreach (var usr in testpassive)
            {
                TC_Approve_DGV.Rows.Add(usr.Name, usr.Surname, usr.UserName);
            }
        }

        private void TC_Approve_DGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == TC_Approve_DGV.Columns["approve"].Index && e.RowIndex >= 0)
            {

                string username = TC_Approve_DGV.Rows[e.RowIndex].Cells["UserName"].Value.ToString();


                _userController.ApproveUser(username);


                TC_Approve_DGV.Rows.RemoveAt(e.RowIndex);
            }
        }


        private void Menu_Brand_btn_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = BrandPage;
        }

        private void btn_brand_add_Click(object sender, EventArgs e)
        {
            // Get the brand name from the text box
            string brandName = Brand_TextBox.Text.Trim();

            // Check if the brand name is not empty
            if (string.IsNullOrEmpty(brandName))
            {
                MessageBox.Show("Lütfen bir marka adı girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Add the brand to the database
            try
            {
                // Assuming you have a method in BrandController to add a brand to the database
                _brandController.AddBrand(new Brand { Name = brandName });

                // Add the brand to the DataGridView
                Brand_DGV.Rows.Add(brandName);

                // Clear the text box
                Brand_TextBox.Clear();

                MessageBox.Show($"Marka '{brandName}' başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Marka eklenirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Brand_searchbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var getbrand = _brandController.GetBrand(Brand_searchbox.Text);


                Brand_DGV.Rows.Clear();



                foreach (var brand in getbrand)
                {
                    int RowIndex = Brand_DGV.Rows.Add(brand.Id, brand.Name);
                    /*DataGridViewButtonCell Ubutton= new DataGridViewButtonCell();
                    Ubutton.Value = "Güncelle";
                    Brand_DGV.Rows[RowIndex].Cells["Update"] = Ubutton;
                    DataGridViewButtonCell Dbutton = new DataGridViewButtonCell();
                    Dbutton.Value = "Sil";
                    Brand_DGV.Rows[RowIndex].Cells["Delete"] = Dbutton;

                    */
                }
                //Brand_DGV.Columns[0].Visible = false;
            }
        }

        private void Brand_DGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Satır seçili mi kontrol et
            {
                var row = Brand_DGV.Rows[e.RowIndex];
                string brandName = row.Cells["Name"].Value.ToString();
                Guid GId = (Guid)row.Cells["Id"].Value;

                if (e.ColumnIndex == Brand_DGV.Columns["Delete"].Index)
                {
                    // Silme işlemi
                    DialogResult confirmResult = MessageBox.Show($"Marka '{brandName}' silinecek. Devam etmek istiyor musunuz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (confirmResult == DialogResult.Yes)
                    {
                        try
                        {
                            _brandController.DeleteBrand(new Brand { Name = brandName, Id = GId });
                            Brand_DGV.Rows.RemoveAt(e.RowIndex); // DataGridView'dan satırı kaldır
                            MessageBox.Show($"Marka '{brandName}' başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Marka silinirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else if (e.ColumnIndex == Brand_DGV.Columns["Update"].Index)
                {
                    // Güncelleme işlemi
                    string newBrandName = row.Cells["Name"].Value.ToString();


                    if (_currentBrandName == null)
                    {
                        _currentBrandName = brandName;
                    }

                    // Eğer yeni marka adı boş değilse ve eski adı ile farklıysa güncelleme yaptır
                    if (!string.IsNullOrEmpty(newBrandName))
                    {
                        if (newBrandName != _currentBrandName)
                        {
                            try
                            {
                                _brandController.UpdateBrand(new Brand { Name = newBrandName, Id = GId });
                                MessageBox.Show($"Marka '{_currentBrandName}' başarıyla '{newBrandName}' olarak güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                // Güncellenen adı eski adı olarak güncelle
                                _currentBrandName = newBrandName;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Marka güncellenirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Marka isminden değişiklik olmadığından dolayı güncelleme işlemi yapılmıyor.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Marka adı boş olamaz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }
        private void Menu_Product_btn_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = ProductPage;

        }
        private void BTN_add_product_Click(object sender, EventArgs e)
        {
            // Get the selected brand ID from the ComboBox
            Guid selectedBrandId;
            if (!Guid.TryParse(cmbx_brandıd.SelectedValue.ToString(), out selectedBrandId))
            {
                MessageBox.Show("Geçerli bir marka seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Get the product details from the form controls
            string serial = txt_serial.Text.Trim();
            string name = txt_productname.Text.Trim();
            string description = txt_product_description.Text.Trim();
            decimal lenght = NUD_product_lenght.Value;
            decimal width = NUD_product_width.Value;
            decimal depth = NUD_product_depth.Value;
            decimal weight = NUD_product_weight.Value;
            decimal volume = NUD_product_volume.Value;

            // Check if required product fields are not empty
            if (string.IsNullOrEmpty(serial) || string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Lütfen ürünün seri numarasını ve adını girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Create a new Product object with the values from the form controls
            Product product = new Product
            {
                Serial = serial,
                Name = name,
                Description = description,
                Lenght = lenght,
                Width = width,
                Depth = depth,
                Weight = weight,
                Volume = volume,
                BrandId = selectedBrandId
            };

            // Try to add the product to the database
            try
            {
                _productController.AddProduct(product);

                // Optionally, show a confirmation message or refresh the UI
                MessageBox.Show("Ürün başarılı bir şekilde eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Clear the form controls
                txt_serial.Clear();
                txt_productname.Clear();
                txt_product_description.Clear();
                NUD_product_lenght.Value = 0;
                NUD_product_width.Value = 0;
                NUD_product_depth.Value = 0;
                NUD_product_weight.Value = 0;
                NUD_product_volume.Value = 0;
                cmbx_brandıd.SelectedIndex = -1; // Optionally clear the ComboBox selection
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ürün eklenirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbx_brandıd_TextChanged(object sender, EventArgs e)
        {
            string searchText = cmbx_brandıd.Text;

            var brand = _brandController.GetBrand(searchText);

            cmbx_brandıd.DataSource = brand;
            cmbx_brandıd.DisplayMember = "Name";
            cmbx_brandıd.ValueMember = "Id";
            /* cmbx_brandıd.Items.Clear();
             foreach (var brand in brands)
             {
                 cmbx_brandıd.Items.Add(brand.Name);
             }
            */


        }

        private void btn_productsearch_Click(object sender, EventArgs e)
        {


            string serial = txtProductSerial.Text;
            string name = txtProductName.Text;
            string description = txtProductDescription.Text;

            decimal minLenght = string.IsNullOrEmpty(MinLenght.Text) ? 0 : Convert.ToDecimal(MinLenght.Text);
            decimal maxLenght = string.IsNullOrEmpty(MaxLenght.Text) ? decimal.MaxValue : Convert.ToDecimal(MaxLenght.Text);
            decimal minWidth = string.IsNullOrEmpty(MinWidth.Text) ? 0 : Convert.ToDecimal(MinWidth.Text);
            decimal maxWidth = string.IsNullOrEmpty(MaxWidth.Text) ? decimal.MaxValue : Convert.ToDecimal(MaxWidth.Text);
            decimal minDepth = string.IsNullOrEmpty(MinDepth.Text) ? 0 : Convert.ToDecimal(MinDepth.Text);
            decimal? maxDepth = string.IsNullOrEmpty(MaxDepth.Text) ? (decimal?)null : Convert.ToDecimal(MaxDepth.Text);
            decimal minWeight = string.IsNullOrEmpty(MinWeight.Text) ? 0 : Convert.ToDecimal(MinWeight.Text);
            decimal maxWeight = string.IsNullOrEmpty(MaxWeight.Text) ? decimal.MaxValue : Convert.ToDecimal(MaxWeight.Text);
            decimal minVolume = string.IsNullOrEmpty(MinVolume.Text) ? 0 : Convert.ToDecimal(MinVolume.Text);
            decimal maxVolume = string.IsNullOrEmpty(MaxVolume.Text) ? decimal.MaxValue : Convert.ToDecimal(MaxVolume.Text);
            string brandName = txtBrandName.Text;

            
            var productFilter = new ProductFilter
            {
                Serial = serial,
                Name = name,
                Description = description,
                MinLenght = minLenght,
                MaxLenght = maxLenght,
                MinWidth = minWidth,
                MaxWidth = maxWidth,
                MinDepth = minDepth,
                MaxDepth = maxDepth,
                MinWeight = minWeight,
                MaxWeight = maxWeight,
                MinVolume = minVolume,
                MaxVolume = maxVolume,
                BrandName = brandName
            };


            List<Product> filteredProducts = _productController.GetProduct(productFilter);


            dgvProducts.Rows.Clear();

            MessageBox.Show(filteredProducts.Count.ToString());
            foreach (var product in filteredProducts)
            {
                dgvProducts.Rows.Add(
                    product.BrandName,
                    product.Serial,
                    product.Name,
                    product.Description,
                    product.Lenght,
                    product.Width,
                    product.Depth,
                    product.Weight,
                    product.Volume,
                    product.Id,
                    product.BrandId


                );

            }


        }
        private void Menu_Stock_btn_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = StockPage;
        }

        private void cmbx_prductId_TextChanged(object sender, EventArgs e)
        {
            string searchText = cmbx_prductId.Text;

            var product = _productController.GetProduct(searchText);

            cmbx_prductId.DataSource = product;
            cmbx_prductId.DisplayMember = "Name";
            cmbx_prductId.ValueMember = "Id";
        }

        private void btn_add_stock_Click(object sender, EventArgs e)
        {
            Guid selectedProductId;
            if (!Guid.TryParse(cmbx_prductId.SelectedValue.ToString(), out selectedProductId))
            {
                MessageBox.Show("Geçerli bir ürün seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string stockCode = txt_stockcode.Text.Trim();
            string stockUnit = cmbx_stockunit.SelectedItem?.ToString();


            string stockCountText = txt_stockcount.Text.Trim();
            float stockCount;
            if (string.IsNullOrEmpty(stockCode) || string.IsNullOrEmpty(stockUnit) || !float.TryParse(stockCountText, out stockCount))
            {
                MessageBox.Show("Lütfen stok kodunu, stok miktarını ve birimini doğru şekilde girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            Stock stock = new Stock
            {
                StockCode = stockCode,
                Count = stockCount,
                Unit = stockUnit,
                ProductId = selectedProductId
            };


            try
            {
                _stockController.AddStock(stock);

                MessageBox.Show("Stok başarılı bir şekilde eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);


                txt_stockcode.Clear();
                txt_stockcount.Clear(); // Clear the stock count input
                cmbx_stockunit.SelectedIndex = -1; // Clear the ComboBox selection
                cmbx_prductId.SelectedIndex = -1; // Clear the ComboBox selection
            }
            catch (Exception ex)
            {
                MessageBox.Show("Stok eklenirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void stock_searchtext_KeyDown(object sender, KeyEventArgs e)
        {
            // Enter tuşuna basıldığında işlemi gerçekleştirin
            if (e.KeyCode == Keys.Enter)
            {
                // Arama terimini al
                string searchText = stock_searchtext.Text.Trim();

                // Arama terimini kontrol et
                if (string.IsNullOrEmpty(searchText))
                {
                    MessageBox.Show("Lütfen arama terimini girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // StockController üzerinden stokları al
                var stocks = _stockController.GetStock(searchText);

                // DataGridView'yi temizle
                dgv_stock.Rows.Clear();

                // Stokları DataGridView'ye ekle
                foreach (var stock in stocks)
                {
                    dgv_stock.Rows.Add(
                        stock.Id,
                        stock.StockCode,
                        stock.ProductId,
                        stock.ProductName,
                        stock.Count,
                        stock.Unit
                    );
                }

                // Arama sonuçlarını göster
                MessageBox.Show($"{stocks.Count} adet stok bulundu.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void stock_search_btn_Click(object sender, EventArgs e)
        {
            string ProductName = txtbox_stock_ProductName.Text;
            string StockCode = txtbox_stock_code.Text;
            float Count = float.TryParse(txtbox_stock_count.Text.Trim(), out float result) ? result : 0;
            string Unit = txtbox_stock_unit.Text;

            Stock newstock = new Stock
            {
                ProductName = ProductName,
                StockCode = StockCode,
                Count = Count,
                Unit = Unit
            };


            List<Stock> teststock = _stockController.GetStock(newstock);/*_stockController.GetStock(newstock);*/
            dgv_stock.Rows.Clear();

            foreach (var stk in teststock)
            {
                dgv_stock.Rows.Add(stk.ProductName, stk.StockCode, stk.Count, stk.Unit);
            }

        }
        private void Menu_Company_btn_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = CompanyPage;
        }

        private void btn_add_company_Click(object sender, EventArgs e)
        {
            string taxNo = txt_company_taxno.Text.Trim();
            string name = txt_company_name.Text.Trim();
            string description = txt_company_description.Text.Trim();
            string type = txt_company_type.Text;
            string adress = txt_company_adress.Text;



            if (string.IsNullOrEmpty(taxNo) || string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Lütfen şirketin vergi  numarasını ve adını girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Company company = new Company
            {
                TaxNo = taxNo,
                Name = name,
                Description = description,
                Type = type,
                Adress = adress

            };


            try
            {
                _companyController.AddCompany(company);


                MessageBox.Show("Şirket  eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);


                txt_company_taxno.Clear();
                txt_company_name.Clear();
                txt_product_description.Clear();
                txt_company_type.Clear();
                txt_company_adress.Clear();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Şirket eklenirken hata oluştu" +
                     ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void company_search_btn_Click(object sender, EventArgs e)
        {
            string TaxNo = txtbox_company_TaxNo.Text;
            string Name = txtbox_company_Name.Text;
            string Description = txtbox_company_Description.Text;
            string Type = txtbox_company_Type.Text;
            string Adress = txtbox_company_Adress.Text;


            Company ncompany = new Company
            {
                TaxNo = TaxNo,
                Name = Name,
                Description = Description,
                Type = Type,
                Adress = Adress

            };


            List<Company> testcompany = _companyController.GetCompany(ncompany);
            dgv_company.Rows.Clear();

            foreach (var cmpny in testcompany)
            {
                dgv_company.Rows.Add(cmpny.Id, cmpny.TaxNo, cmpny.Name, cmpny.Description, cmpny.Type, cmpny.Adress);
            }

        }

        private void company_searchtext_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {

                string searchText = company_searchtext.Text.Trim();


                if (string.IsNullOrEmpty(searchText))
                {
                    MessageBox.Show("Lütfen arama terimini girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                var testcompany = _companyController.GetCompany(searchText);


                dgv_company.Rows.Clear();

                foreach (var cmpny in testcompany)
                {
                    dgv_company.Rows.Add(
                        cmpny.Id,
                        cmpny.TaxNo,
                        cmpny.Name,
                        cmpny.Description,
                        cmpny.Type,
                        cmpny.Adress
                    );
                }

                MessageBox.Show($"{testcompany.Count} adet şirket bulundu.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cmbx_supplier_prductId_TextChanged(object sender, EventArgs e)
        {
            string searchText = cmbx_supplier_prductId.Text;

            var product = _productController.GetProduct(searchText);

            cmbx_supplier_prductId.DataSource = product;
            cmbx_supplier_prductId.DisplayMember = "Name";
            cmbx_supplier_prductId.ValueMember = "Id";

        }

        private void cmbx_supplier_companyname_TextChanged(object sender, EventArgs e)
        {
            string searchText = cmbx_supplier_companyname.Text;

            var company = _companyController.GetCompany(searchText);

            cmbx_supplier_companyname.DataSource = company;
            cmbx_supplier_companyname.DisplayMember = "Name";
            cmbx_supplier_companyname.ValueMember = "Id";
        }

        private void btn_add_supplier_Click(object sender, EventArgs e)
        {
            Guid selectedProductId;
            Guid selectedCompanyId;
            float supplierPrice;
            string selectedCurrency;

            // Validate ProductId
            if (!Guid.TryParse(cmbx_supplier_prductId.SelectedValue.ToString(), out selectedProductId))
            {
                MessageBox.Show("Geçerli bir ürün seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate CompanyId
            if (!Guid.TryParse(cmbx_supplier_companyname.SelectedValue.ToString(), out selectedCompanyId))
            {
                MessageBox.Show("Geçerli bir şirket seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Retrieve and validate price and currency
            string price = txt_supplierprice.Text.Trim();
            selectedCurrency = cmbx_suppliercurrency.SelectedItem?.ToString();

            // Validate currency selection
            if (string.IsNullOrEmpty(selectedCurrency))
            {
                MessageBox.Show("Lütfen geçerli bir para birimi seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate price
            if (!float.TryParse(price, out supplierPrice))
            {
                MessageBox.Show("Lütfen geçerli bir fiyat girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Create Supplier object
            Supplier supplier = new Supplier
            {
                ProductId = selectedProductId,
                CompanyId = selectedCompanyId,
                Price = supplierPrice,
                Currency = selectedCurrency
            };

            try
            {
                // Call the controller to add the supplier
                _supplierController.AddSupplier(supplier);

                MessageBox.Show("Tedarikçi başarılı bir şekilde eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Clear input fields
                cmbx_supplier_prductId.SelectedIndex = -1;
                cmbx_supplier_companyname.SelectedIndex = -1;
                txt_supplierprice.Clear();
                cmbx_suppliercurrency.SelectedIndex = -1; // Clear currency selection
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tedarikçi eklenirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void supplier_searchtext_KeyDown(object sender, KeyEventArgs e)

        {
            // Enter tuşuna basıldığında işlemi gerçekleştirin
            if (e.KeyCode == Keys.Enter)
            {
                // Arama terimini al
                string searchText = supplier_searchtext.Text.Trim();

                // Arama terimini kontrol et
                if (string.IsNullOrEmpty(searchText))
                {
                    MessageBox.Show("Lütfen arama terimini girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // StockController üzerinden stokları al
                var suppliers = _supplierController.GetSupplier(searchText);

                // DataGridView'yi temizle
                dgv_supplier.Rows.Clear();

                // Stokları DataGridView'ye ekle
                foreach (var supplier in suppliers)
                {
                    dgv_supplier.Rows.Add(
                        supplier.Id,
                        supplier.ProductName,
                        supplier.CompanyName,
                        supplier.ProductId,
                        supplier.CompanyId,
     
                        supplier.Price,
                        supplier.Currency
                        //dgv deki satın sıramla aynı olmak zorunda burası


                       
                    );
                }

                // Arama sonuçlarını göster
                MessageBox.Show($"{suppliers.Count} adet stok bulundu.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void supplier_search_btn_Click(object sender, EventArgs e)
        { // Kullanıcıdan filtreleme verilerini alma
            string productName = txtbox_supplier_ProductName.Text.Trim();
            string companyName = txtbox_supplier_CompanyName.Text.Trim();
            float minPrice = float.TryParse(txt_minPrice.Text.Trim(), out float minPriceResult) ? minPriceResult : 0;
            float maxPrice = float.TryParse(txt_minPrice.Text.Trim(), out float maxPriceResult) ? maxPriceResult : float.MaxValue; // Varsayılan olarak maksimum float değeri
            string currency = txtbox_supplier_Currency.Text.Trim();

           
            var supplierFilter = new SupplierFilter
            {
                ProductName = productName, 
                CompanyName = companyName, 
                MinPrice = minPrice > 0 ? (float?)minPrice : null,
                MaxPrice = maxPrice < float.MaxValue ? (float?)maxPrice : null,
                Currency = !string.IsNullOrEmpty(currency) ? currency : null
            };

            // Supplier verilerini çekme
            List<Supplier> suppliers = _supplierController.GetSupplier(supplierFilter);

            // DataGridView kontrolünü temizleme
            dgv_supplier.Rows.Clear();

            // Sonuçları DataGridView kontrolüne ekleme
            foreach (var supplier in suppliers)
            {
                dgv_supplier.Rows.Add(
                        supplier.Id,
                        supplier.ProductName,
                        supplier.CompanyName,
                        supplier.ProductId,
                        supplier.CompanyId,
                        supplier.Price,
                        supplier.Currency
                );
            }
        }

        private void btn_product_ex_Click(object sender, EventArgs e)
        {
            // Dosya yolunu belirleyin (kullanıcının seçmesi için bir SaveFileDialog kullanabilirsiniz)
            string filePath = @"C:\Path\To\Your\File.xlsx";

            // DataGridView'inizi ve dosya yolunu geçirin
            ExcellFunctions.ExportDataGridViewToExcel(dgvProducts, filePath);

            MessageBox.Show("Veriler başarıyla Excel dosyasına aktarıldı.");
        }

        private void btn_company_ex_Click(object sender, EventArgs e)
        {
            // SaveFileDialog'ı başlat
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                
                saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*";
                saveFileDialog.Title = "Excel Dosyası Olarak Kaydet";
                saveFileDialog.FileName = "File.xlsx";

              
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    
                    string filePath = saveFileDialog.FileName;

                    
                    ExcellFunctions.ExportDataGridViewToExcel(dgv_company, filePath);

                  
                    MessageBox.Show("Veriler başarıyla Excel dosyasına aktarıldı.");
                }
            }
        }

        private void btn_campany_pdf_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PDF Files (*.pdf)|*.pdf",
                Title = "Save as PDF"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;

                // Dosya yolunun geçerli olduğunu kontrol etme
                if (string.IsNullOrWhiteSpace(filePath) || !filePath.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Geçersiz dosya yolu. Lütfen geçerli bir PDF dosyası seçin.");
                    return;
                }

                PdfFunctions pdfFunctions = new PdfFunctions();
                pdfFunctions.ExportDataGridViewToPdf(dgv_company, filePath);
            }
        }
    }
    }
    















    


    