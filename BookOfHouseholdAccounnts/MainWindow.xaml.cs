using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml;
using System.Xml.Serialization;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace BookOfHouseholdAccounnts
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Variables
        private ViewModel vwModel;
        private List<BankAccount> bankAccounts;
        private List<Contract> contracts; 
        private bool isSavedData = true;
        private TransactionFilter transFilter, prevTransFilter;
        private ObservableValue value1;

        public MainWindow()
        {
            //CultureInfo culture = new CultureInfo(ConfigurationManager.AppSettings["DefaultCulture"]);
            //Thread.CurrentThread.CurrentCulture = culture;
            //Thread.CurrentThread.CurrentUICulture = culture;

            vwModel = new ViewModel();
            this.DataContext = vwModel;
            InitializeComponent();
            InitCategories();
            OpenBankAccountData();
        }
        #endregion

        #region Functions

        private void InitCategories()
        {
            vwModel.BudgetingOptions = new ObservableCollection<string>() { "Essentials", "Savings", "Wants" };
            vwModel.TransactionOptions = new ObservableCollection<string>() { "Groceries", "Housing", "Utilities", "Car", "Insurance", "ETF", "Stock",
                "Shopping", "Dining Out", "Hobby", "Entertainment", "Cats", "Financial Aid", "Education", "Regular Work"};
            vwModel.PeriodicityTimeUnit = new ObservableCollection<string> { "Day(s)", "Week(s)", "Month(s)", "Quarter(s)", "Year(s)" };

            bankAccounts = new List<BankAccount>();
            contracts = new List<Contract>(); 
            transFilter = new TransactionFilter();
        }

        private void InitDiagrams()
        {
            ((DefaultTooltip)chartesianChart_transactionSeries.DataTooltip).SelectionMode = TooltipSelectionMode.OnlySender;

            var i = 0;
            var pieChartColors = new SolidColorBrush[] { Brushes.LimeGreen, Brushes.CornflowerBlue, Brushes.IndianRed };
            Func<ChartPoint, string> PointLabel = chartPoint => string.Format("{0}€ ({1:P})", chartPoint.Y, chartPoint.Participation);

            pieChart_budgetingSeries.Series.Clear(); 
            foreach (string series in vwModel.BudgetingOptions)
            {
                double seriesValue = 0; 
                foreach (BankAccount bkkAcc in bankAccounts)
                {
                    seriesValue += bkkAcc.Expenses.Where(expense => expense.BudgetingCategory == series).Sum(expense => expense.Value);
                }

                pieChart_budgetingSeries.Series.Add(new PieSeries { 
                    Title = series, 
                    Fill = pieChartColors[i++], 
                    StrokeThickness = 0, 
                    Values = new ChartValues<double> { seriesValue },
                    DataLabels = true, 
                    LabelPoint = PointLabel 
                });
            }

            chartesianChart_transactionSeries.Series.Clear();
            foreach (string series in vwModel.TransactionOptions)
            {
                double seriesValue = 0;
                foreach (BankAccount bkkAcc in bankAccounts)
                {
                    seriesValue += bkkAcc.Expenses.Where(expense => expense.TransactionCategory == series).Sum(expense => expense.Value);
                    seriesValue += bkkAcc.Incomes.Where(incomes => incomes.TransactionCategory == series).Sum(incomes => incomes.Value);
                }

                chartesianChart_transactionSeries.Series.Add(new ColumnSeries { Title = series, Values = new ChartValues<double> { seriesValue }, DataLabels=true, MaxColumnWidth=55 });
            }
            //chartesianChart_transactionSeries.Series[0].Values = new ChartValues<double> { 35 };
            //chartesianChart_transactionSeries.Series[0].Values[0] = 15.0;
        }


        private void SaveBankAccountData()
        {
            var serializer = new XmlSerializer(bankAccounts.GetType());
            using (var writer = XmlWriter.Create("bank_acccounts.xml"))
            {
                serializer.Serialize(writer, bankAccounts);
            }
            isSavedData = true;
        }

        private void OpenBankAccountData()
        {
            try
            {
                // Load BankAccount data from .xml file 
                var serializer = new XmlSerializer(bankAccounts.GetType());
                using (var reader = XmlReader.Create("bank_acccounts.xml"))
                {
                    var bankAccs = (List<BankAccount>)serializer.Deserialize(reader);
                    bankAccounts.AddRange(bankAccs);
                    foreach (BankAccount bankAcc in bankAccs)
                    {
                        vwModel.BankInstituteOptions.Add(new BankInstituteView(bankAcc.Name));
                    }
                }
                // Add Transaction to ViewModel
                AddAllTransactionsToView();
                vwModel.TotalBalance = vwModel.Transactions.Sum(trans => trans.TransactionValue);
                isSavedData = true;
            }
            catch
            {
                MessageBox.Show("We could not find an existing file - create a new file.", "Bank account error", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void AddAllTransactionsToView()
        {
            vwModel.Transactions.Clear();

            foreach (BankAccount bankAcc in bankAccounts)
            {
                //
                foreach (Expense expense in bankAcc.Expenses)
                {
                    vwModel.Transactions.Add(new TransactionOverview(expense));
                }
                //
                foreach (Income income in bankAcc.Incomes)
                {
                    vwModel.Transactions.Add(new TransactionOverview(income));
                }
            }
        }

        private string[] ReadAndSeparateString(string bank, string str)
        {
            switch (bank)
            {
                case "Sparkasse":
                    return str.Replace("\"", "").Split(';');
                case "HypoVereinsbank":
                    return str.Replace("\0", "").Split(';');
                default:
                    return null;
            }
        }
        private Transaction AddTransactionFromImport(Transaction transaction)
        {
            var newAddTransactionWindow = new AddAndEditTransactionWindow(vwModel, transaction, false, true);

            if (newAddTransactionWindow.ShowDialog() ?? false)
            {
                if (newAddTransactionWindow.TransExpense != null)
                {
                    return newAddTransactionWindow.TransExpense;
                }
                else if (newAddTransactionWindow.TransIncome != null)
                {
                    return newAddTransactionWindow.TransIncome;
                }
            }

            return null;
        }
        #endregion

        #region Events
        private void dg_transactions_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        private void btn_addBankInstitute_Click(object sender, RoutedEventArgs e)
        {
            var newBankAccountWindow = new AddBankAccountWindow(vwModel);
            if (newBankAccountWindow.ShowDialog() ?? false)
            {
                bankAccounts.Add(new BankAccount(
                    newBankAccountWindow.BankAccountName,
                    newBankAccountWindow.BankAccountBalance,
                    newBankAccountWindow.BankAccountBalanceDate,
                    newBankAccountWindow.BankAccountDescription));
                vwModel.BankInstituteOptions.Add(new BankInstituteView(newBankAccountWindow.BankAccountName));
                vwModel.TotalBalance += newBankAccountWindow.BankAccountBalance;
                isSavedData = false;
            }            
        }

        private void btn_addTransaction_Click(object sender, RoutedEventArgs e)
        {
            if (vwModel.BankInstituteOptions.Count > 0)
            {
                var transactionWindow = new AddAndEditTransactionWindow(vwModel, null, true, false);
                if (transactionWindow.ShowDialog() ?? false)
                {
                    if (transactionWindow.TransExpense != null)
                    {
                        var expense = transactionWindow.TransExpense;
                        bankAccounts.Find(account => account.Name == expense.BankInstitute).AddExpense(expense);
                        vwModel.Transactions.Add(new TransactionOverview(expense));
                    }
                    else
                    {
                        var income = transactionWindow.TransIncome;
                        bankAccounts.Find(account => account.Name == income.BankInstitute).AddIncome(income);
                        vwModel.Transactions.Add(new TransactionOverview(income));
                    }
                    vwModel.TotalBalance = vwModel.Transactions.Sum(trans => trans.TransactionValue);
                    isSavedData = false;
                }
            }
            else
            {
                MessageBox.Show("Please add a bank account before adding a transaction.", "My App");
            }
        }

        private void btn_deleteTransaction_Click(object sender, RoutedEventArgs e)
        {
            var currentTransaction = (TransactionOverview)dg_transactions.SelectedItem;

            if (currentTransaction.TransactionValue < 0)
            {
                foreach (BankAccount bankAcc in bankAccounts)
                {
                    var expense = bankAcc.Expenses.Find(exp => exp.ID == currentTransaction.ID);

                    if (expense != null)
                    {
                        bankAcc.Expenses.Remove(expense);
                        break;
                    }
                }
            }
            else
            {
                foreach (BankAccount bankAcc in bankAccounts)
                {
                    var income = bankAcc.Incomes.Find(inc => inc.ID == currentTransaction.ID);

                    if (income != null)
                    {
                        bankAcc.Incomes.Remove(income);
                        break;
                    }
                }
            }
            vwModel.Transactions.Remove(currentTransaction);
            vwModel.TotalBalance = vwModel.Transactions.Sum(trans => trans.TransactionValue);
        }

        private void btn_filterTransaction_Click(object sender, RoutedEventArgs e)
        {
            var newFilterTransactionWindow = new FilterTransactionsWindow(vwModel, transFilter);
            if (newFilterTransactionWindow.ShowDialog() ?? false)
            {
                ApplyFilter();
            }
        }

        private void ApplyFilter()
        {
            vwModel.FilterAppliedColor = Brushes.Green;
            vwModel.FilterAppliedToolTip = "Filter applied. Press to undo filter(s).";

            vwModel.Transactions.Clear();

            foreach (BankAccount bkkAcc in bankAccounts)
            {
                if (!transFilter.IsInstituteFilter || transFilter.IsInstituteFilter && vwModel.BankInstituteOptions.ToList().Find(bkk => bkk.Name == bkkAcc.Name).IsFiltered)
                {
                    foreach (Income income in bkkAcc.Incomes)
                    {
                        if ((!transFilter.IsValueFilter || transFilter.IsValueFilter && transFilter.MinimumValue <= income.Value && income.Value <= transFilter.MaximumValue) &&
                            (!transFilter.IsDateFilter || transFilter.IsDateFilter && DateTime.Compare(income.Date, transFilter.MinimumDate) > 0 && DateTime.Compare(income.Date, transFilter.MaximumDate) < 0) &&
                            (!transFilter.IsKeywordFilter || transFilter.IsKeywordFilter && (transFilter.IsPartnerKeyword && income.Partner.Contains(transFilter.Keyword) || transFilter.IsDescriptionKeyword && income.Partner.Contains(transFilter.Keyword))))
                        {
                            vwModel.Transactions.Add(new TransactionOverview(income));
                        }
                    }
                    foreach (Expense expense in bkkAcc.Expenses)
                    {
                        if ((!transFilter.IsValueFilter || transFilter.IsValueFilter && transFilter.MinimumValue <= expense.Value && expense.Value <= transFilter.MaximumValue) &&
                            (!transFilter.IsDateFilter || transFilter.IsDateFilter && DateTime.Compare(expense.Date, transFilter.MinimumDate) > 0 && DateTime.Compare(expense.Date, transFilter.MaximumDate) < 0) &&
                            (!transFilter.IsKeywordFilter || transFilter.IsKeywordFilter && (transFilter.IsPartnerKeyword && expense.Partner.Contains(transFilter.Keyword) || transFilter.IsDescriptionKeyword && expense.Details.Contains(transFilter.Keyword))))
                        {
                            vwModel.Transactions.Add(new TransactionOverview(expense));
                        }
                    }
                }
            }
            vwModel.TotalBalance = vwModel.Transactions.Sum(trans => trans.TransactionValue);
        }

        private void menu_save_Click(object sender, RoutedEventArgs e)
        {
            SaveBankAccountData();
        }

        private void menu_open_Click(object sender, RoutedEventArgs e)
        {
            OpenBankAccountData();
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!isSavedData)
            {
                var result = MessageBox.Show("Do you want to save your data before leaving?", "Unsaved data", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    SaveBankAccountData();
                }
                else
                {
                    if (result == MessageBoxResult.Cancel)
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        private void dg_transactions_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var currentTransaction = (TransactionOverview)dg_transactions.SelectedItem;

            AddAndEditTransactionWindow newAddTransactionWindow;

            if (currentTransaction.TransactionValue < 0)
            {
                Expense expense = null;

                foreach (BankAccount bankAcc in bankAccounts)
                {
                    expense = bankAcc.Expenses.Find(ex => ex.ID == currentTransaction.ID);
                    if (expense != null) break;
                }

                newAddTransactionWindow = new AddAndEditTransactionWindow(vwModel, expense, false, false);
            }
            else
            {
                Income income = null;

                foreach (BankAccount bankAcc in bankAccounts)
                {
                    income = bankAcc.Incomes.Find(inc => inc.ID == currentTransaction.ID);
                    if (income != null) break;
                }

                newAddTransactionWindow = new AddAndEditTransactionWindow(vwModel, income, false, false);
            }

            newAddTransactionWindow.ShowDialog();
        }

        private void menu_import_Click(object sender, RoutedEventArgs e)
        {
            var newImportWindow = new ImportWindow(vwModel);
            if (newImportWindow.ShowDialog() ?? false)
            {
                using (var sr = new StreamReader(newImportWindow.FilePath))
                {
                    var transImportCt = File.ReadAllLines(newImportWindow.FilePath).Length - 2;

                    var columnNamnes = ReadAndSeparateString(newImportWindow.BankInstitute, sr.ReadLine()).ToList();

                    var valueIdx = columnNamnes.FindIndex(col => col.Contains("Betrag"));
                    var partnerIdx = columnNamnes.FindIndex(col => col.Contains("Beguenstigter"));
                    var dateIdx = columnNamnes.FindIndex(col => col.Contains("Buchungstag") || col.Contains("Buchungsdatum"));
                    var detailIdx = columnNamnes.FindIndex(col => col.Contains("Verwendungszweck"));

                    if (valueIdx + partnerIdx + dateIdx + detailIdx >= 6)
                    {
                        int transCt = 1; 

                        string currentLine;
                        while ((currentLine = sr.ReadLine()) != null)
                        {
                            var currentTransValues = ReadAndSeparateString(newImportWindow.BankInstitute, currentLine);

                            if (currentTransValues != null)
                            {
                                var value = Convert.ToSingle(currentTransValues[valueIdx]);

                                Transaction transaction;

                                if (value < 0)
                                {
                                    transaction = new Expense();
                                }
                                else
                                {
                                    transaction = new Income();
                                }

                                transaction.Partner = partnerIdx >= 0 ? currentTransValues[partnerIdx] : "";
                                transaction.Date = DateTime.Parse(currentTransValues[dateIdx]);
                                transaction.Value = value;
                                transaction.Details = currentTransValues[detailIdx];
                                transaction.BankInstitute = newImportWindow.BankInstitute;

                                vwModel.AdditionalTransactionInformation = String.Format("Transaction {0} / {1}", transCt++, transImportCt);
                                transaction = AddTransactionFromImport(transaction);
                                vwModel.AdditionalTransactionInformation = "-";

                                if (transaction != null)
                                {
                                    var bkkAcc = bankAccounts.Find(account => account.Name == transaction.BankInstitute);

                                    if (transaction.Value < 0)
                                    {
                                        bkkAcc.Expenses.Add(transaction as Expense);
                                    }
                                    else
                                    {
                                        bkkAcc.Incomes.Add(transaction as Income);
                                    }

                                    vwModel.Transactions.Add(new TransactionOverview(transaction));
                                }                                
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Unknown combination of file format and bank institute.\nImport has been aborted.", "Import error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }


        private void tbCntrl_mainWindow_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tabItem_evaluation.IsSelected)
            {
                this.Width = 900;
                InitDiagrams();

            }
            else
            {                
                this.Width = 600;

                if (tabItem_contracts.IsSelected) CheckExpirationOfContracts();                 
            }
        }

        private void CheckExpirationOfContracts()
        {
            for (int i = 0; i < dg_contracts.Items.Count; i++)
            {
                DataGridRow row = (DataGridRow)dg_contracts.ItemContainerGenerator.ContainerFromIndex(i);

                if (row != null)
                {
                    var currentContract = (Contract)row.Item;

                    // Is the contract in a critical time period (1 month before automatic contract extension)?
                    if ((currentContract.EndDate.AddMonths(-(int)Math.Ceiling(currentContract.CancellationPeriod) - 1) - DateTime.Now).TotalDays < 0)
                    {
                        if ((currentContract.EndDate.AddMonths(-(int)Math.Ceiling(currentContract.CancellationPeriod)) - DateTime.Now).TotalDays < 0) // Automatic contract extension 
                        {
                            row.Background = new SolidColorBrush(Color.FromRgb(252, 163, 159));
                        }
                        else // Remember user 1 month before the contract has to be declined
                        {
                            row.Background = new SolidColorBrush(Color.FromRgb(255, 250, 165));
                        }
                    }
                }               
            }
        }

        private void btn_addContract_Click(object sender, RoutedEventArgs e)
        {
            var newContractWindow = new AddContractWindow(vwModel, null);
            if (newContractWindow.ShowDialog() ?? false)
            {
                contracts.Add(newContractWindow.Contract);
                vwModel.Contracts.Add(newContractWindow.Contract);
            }
        }

        private void dg_contracts_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
            CheckExpirationOfContracts();
        }

        private void dg_contracts_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var newContractWindow = new AddContractWindow(vwModel, (Contract)dg_contracts.SelectedItem);
            newContractWindow.ShowDialog(); 
        }

        private void lbl_filterUnderscore_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (vwModel.FilterAppliedColor == Brushes.Red)
            {
                vwModel.FilterAppliedColor = Brushes.Green;
                vwModel.FilterAppliedToolTip = "Filter applied. Press to undo filter(s).";
                if (prevTransFilter != null)
                {
                    transFilter = prevTransFilter;
                }
                ApplyFilter();
            }
            else
            {
                vwModel.FilterAppliedColor = Brushes.Red;
                vwModel.FilterAppliedToolTip = "No filter applied. Press to apply selected filter(s).";
                prevTransFilter = transFilter;
                AddAllTransactionsToView();
            }
        }
        #endregion
    }
}
