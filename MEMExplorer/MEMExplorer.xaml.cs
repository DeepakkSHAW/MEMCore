using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Linq;
using System.Windows.Controls;

namespace MEMExplorer
{
    // https://stackoverflow.com/questions/9620278/how-do-i-make-calls-to-a-rest-api-using-c

    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class WindowMEMExplorer : Window
    {
        //String _connectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
        private static readonly string _uriExpCategory = "MEM/ExpCategory?IsSorted=true";
        private static readonly string _uriExpCurrency = "MEM/ExpCurrency?IsSorted=true";
        private static readonly string _uriExpenses = "MEM/Expenses";
        private static readonly string _uriExpense = "MEM/Expense";
        private static List<Category> _catList;
        private static List<Currency> _curList;
        private static List<Expense> _expList;

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            //string expression = @"(?<Number>^[0-9]*\.?[0-9]*)";
            // "[^0-9]+[.[^0-9]+]?";
            string expression = "[^0-9]+";
            Regex regex = new Regex(expression);
            e.Handled = regex.IsMatch(e.Text);
        }
        private void LoadDataFromApi()
        {
            try
            {
                Task<List<Category>> catTask = Task.Run(() => ApiCaller.Get<List<Category>>(_uriExpCategory));
                catTask.Wait();
                _catList = catTask.Result;

                Task<List<Currency>> curTask = Task.Run(() => ApiCaller.Get<List<Currency>>(_uriExpCurrency));
                curTask.Wait();
                _curList = curTask.Result;

                Task<List<Expense>> expTask = Task.Run(() => ApiCaller.Get<List<Expense>>(_uriExpenses));
                expTask.Wait();
                _expList = expTask.Result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
                lblInfo.Text = ex.Message;
            }
        }

        private void LoadDataIntoControls()
        {
            try
            {
                // category Binding  
                this.cbCategory.DisplayMemberPath = "categoryName";
                this.cbCategory.SelectedValuePath = "id";
                this.cbCategory.ItemsSource = _catList;
                this.cbCategory.SelectedValue = 1;
                // currency drop down Binding  
                this.cbCurrency.DisplayMemberPath = "CurrencyName";
                this.cbCurrency.SelectedValuePath = "Id";
                this.cbCurrency.ItemsSource = _curList;
                this.cbCurrency.SelectedValue = 1;
                // Expense data grid Binding  
                this.dgExpense.ItemsSource = _expList;
                if (_expList != null)
                {
                   // this.dgExpense.SelectedIndex = 1;
                    this.dgExpense.Columns[0].Visibility = Visibility.Hidden;
                    this.dgExpense.Columns[6].Visibility = Visibility.Hidden;
                    this.dgExpense.Columns[8].Visibility = Visibility.Hidden;
                    dgExpense.Items.Refresh();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
                lblInfo.Text = ex.Message;
            }
        }
        public WindowMEMExplorer()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lblInfo.Text = "Fetching data from Api..";
            LoadDataFromApi();
            lblInfo.Text = "Fetching completed.";
            ClearValue();
            LoadDataIntoControls();
            //  ((NavigationWindow)LogicalTreeHelper.GetParent(this)).ResizeMode = ResizeMode.NoResize;
        }

        private void ClearValue()
        {
            try
            {
            txtExpTitle.Text = "";
            txtAmt.Text = "0.0";
            txtSing.Text = "XX";
            txtDetails.Text = "";
            cbCategory.SelectedValue = 1;
            cbCurrency.SelectedValue = 1;
            dtExp.Text = DateTime.Now.ToString();
            dgExpense.UnselectAll();
            btnUpdate.IsEnabled = false;

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
                lblInfo.Text = ex.Message;
            }
        }

        private void DgExpense_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                if (dgExpense.SelectedItem != null)
                {
                    var exp_rowlist = (Expense)dgExpense.SelectedItem;
                    System.Diagnostics.Debug.WriteLine($"DataGrid row selected, ExpID: [{exp_rowlist.Id}]");

                    txtExpTitle.Text = exp_rowlist.ExpenseTitle;
                    txtAmt.Text = exp_rowlist.ExpensesAmount.ToString();
                    txtSing.Text = exp_rowlist.Signature;
                    dtExp.Text = exp_rowlist.ExpenseDate.ToString();
                    txtDetails.Text = exp_rowlist.ExpenseDetail;
                    cbCategory.SelectedValue = exp_rowlist.CategoryId;
                    cbCurrency.SelectedValue = exp_rowlist.CurrencyId;
                    btnUpdate.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
                lblInfo.Text = ex.Message;
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.ClearValue();
        }
        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadDataIntoControls();
        }
        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            var oExp = new Expense();
            try
            {
                oExp.ExpenseTitle = txtExpTitle.Text.Trim();
                oExp.ExpensesAmount = Convert.ToDouble(txtAmt.Text);
                oExp.ExpenseDate = Convert.ToDateTime(dtExp.Text);
                oExp.Signature = txtSing.Text;
                oExp.CategoryId = (int)cbCategory.SelectedValue;
                oExp.CurrencyId = (int)cbCurrency.SelectedValue;
                if (!string.IsNullOrEmpty(txtDetails.Text))
                    oExp.ExpenseDetail = txtDetails.Text;

                // Expense Binding  
                var newExpTask = Task.Run(() => ApiCaller.Post<Expense>(_uriExpense, oExp));
                newExpTask.Wait();
                var expenseId = int.Parse(newExpTask.Result.ToString().Split('/').Last());
                var expense = newExpTask.IsCompleted;
                if (expense)
                {
                    oExp.Id = expenseId;
                    _expList.Add(oExp);
                    LoadDataIntoControls();
                }
                ClearValue();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
                lblInfo.Text = ex.Message;
            }

        }
        
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var exp_rowlist = (Expense)dgExpense.SelectedItem;
            if (exp_rowlist == null)
                MessageBox.Show("No Expense has been selected to remove..", "Failed to remove", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            else
            {
                if (MessageBox.Show("Are you sure want to remove this item", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        var uriExpense = _uriExpense + "/" + exp_rowlist.Id;
                        var delExpTask = Task.Run(() => ApiCaller.Delete(uriExpense));
                        delExpTask.Wait();
                        //var expense = newExpTask.Result;
                        // Expense Binding  
                        var expense = delExpTask.IsCompleted;
                        if (expense)
                        {
                            _expList.RemoveAt(_expList.FindIndex(s => s.Id == exp_rowlist.Id));
                            LoadDataIntoControls();
                        }

                        ClearValue();
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
                        lblInfo.Text = ex.Message;
                    }
                }
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var oExp = new Expense();
            var exp_rowlist = (Expense)dgExpense.SelectedItem;
            if (exp_rowlist == null)
                MessageBox.Show("No Expense has been selected to updated..", "Failed to update", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            else
            {
                if (MessageBox.Show("Are you sure want to update this expense", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        oExp.ExpenseTitle = txtExpTitle.Text.Trim();
                        oExp.ExpensesAmount = Convert.ToDouble(txtAmt.Text);
                        oExp.ExpenseDate = Convert.ToDateTime(dtExp.Text);
                        oExp.Signature = txtSing.Text;
                        oExp.CategoryId = (int)cbCategory.SelectedValue;
                        oExp.CurrencyId = (int)cbCurrency.SelectedValue;
                        if (!string.IsNullOrEmpty(txtDetails.Text))
                            oExp.ExpenseDetail = txtDetails.Text;

                        var uriExpense = _uriExpense + "/" + exp_rowlist.Id;
                        var updateExpTask = Task.Run(() => ApiCaller.Put<Expense>(uriExpense, oExp));
                        updateExpTask.Wait();
                        //var expense = newExpTask.Result;
                        // Expense Binding  
                        var expense = updateExpTask.IsCompleted;
                        if (expense)
                        {
                            oExp.Id = exp_rowlist.Id;
                            _expList[(_expList.FindIndex(s => s.Id == exp_rowlist.Id))] = oExp;
                            LoadDataIntoControls();
                        }

                        ClearValue();
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
                        lblInfo.Text = ex.Message;
                    }
                }
            }
        }

        private void Txtbox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)e.OriginalSource;
            tb.SelectAllText();
        }

    }
    //internal static class MEMExplorerHelper
    //{

    //}
}


