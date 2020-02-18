using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml;
using System.Xml.Serialization;

namespace BookOfHouseholdAccounnts
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Variables
        private ViewModel vwModel;
        private List<BankAccount> BankAccounts { get; set; }
        private bool isSavedData { get; set; } = true;
        public MainWindow()
        {
            CultureInfo culture = new CultureInfo(ConfigurationManager.AppSettings["DefaultCulture"]);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            vwModel = new ViewModel();
            this.DataContext = vwModel;
            InitializeComponent();
            InitCategories();
            OpenBankAccountData();
        }
        #endregion

        #region Functions
        private void ShowTransactionDetails(object sender, RoutedEventArgs e)
        {
            vwModel.Transactions[0].BankInstitute = "HALLO";
        }

        private void InitCategories()
        {
            vwModel.BudgetingOptions = new ObservableCollection<string>() { "Essentials", "Savings", "Wants" }; 
            vwModel.TransactionOptions = new ObservableCollection<string>() { "Groceries", "Housing", "Utilities", "Car Payment", "Insurance", "ETF", "Stock",
                "Cryptocurrency", "Rainy Day", "Shopping", "Dining Out", "Hobby", "Entertainment", "Cats"};
            vwModel.PeriodicityTimeUnit = new ObservableCollection<string> { "Day(s)", "Week(s)", "Month(s)", "Quarter(s)", "Year(s)" };
            BankAccounts = new List<BankAccount>();
        }

        private void SaveBankAccountData()
        {
            var serializer = new XmlSerializer(BankAccounts.GetType());
            using (var writer = XmlWriter.Create("bank_acccounts.xml"))
            {
                serializer.Serialize(writer, BankAccounts);
            }
            isSavedData = true;
        }

        private void OpenBankAccountData()
        {
            try
            {
                // Load BankAccount data from .xml file 
                var serializer = new XmlSerializer(BankAccounts.GetType());
                using (var reader = XmlReader.Create("bank_acccounts.xml"))
                {
                    var bankAccs = (List<BankAccount>)serializer.Deserialize(reader);
                    BankAccounts.AddRange(bankAccs);
                    foreach (BankAccount bankAcc in bankAccs)
                    {
                        vwModel.BankInstituteOptions.Add(bankAcc.Name);
                        vwModel.TotalBalance += bankAcc.Balance;
                        //vwModel.TotalBalanceColor = Colors.Green;
                    }
                }
                // Add Transaction to ViewModel
                foreach (BankAccount bankAcc in BankAccounts)
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
                isSavedData = true;
            }
            catch 
            {
                MessageBox.Show("We could not find an existing file - create a new file.", "Bank account error", MessageBoxButton.OK, MessageBoxImage.Information);
            }            
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
                BankAccounts.Add(new BankAccount(
                    newBankAccountWindow.BankAccountName, 
                    newBankAccountWindow.BankAccountBalance, 
                    newBankAccountWindow.BankAccountBalanceDate, 
                    newBankAccountWindow.BankAccountDescription));
                vwModel.BankInstituteOptions.Add(newBankAccountWindow.BankAccountName);
                vwModel.TotalBalance += newBankAccountWindow.BankAccountBalance;
                isSavedData = false;
            }
        }

        private void btn_addTransaction_Click(object sender, RoutedEventArgs e)
        {
            if (vwModel.BankInstituteOptions.Count > 0)
            {
                var newAddTransactionWindow = new AddTransactionWindow(vwModel);
                if (newAddTransactionWindow.ShowDialog() ?? false)
                {
                    if (newAddTransactionWindow.IsExpense)
                    {
                        var expense = new Expense()
                        {
                            TotalValue = newAddTransactionWindow.TotalValue,
                            Partner = newAddTransactionWindow.Partner,
                            BankInstitute = newAddTransactionWindow.BankInstitute,
                            TransactionCategory = newAddTransactionWindow.TransactionCategory,
                            Date = newAddTransactionWindow.Date,
                            PeriodicityDuration = newAddTransactionWindow.PeriodicityDuration,
                            PeriodicityUnit = newAddTransactionWindow.PeriodicityUnit,
                            Details = newAddTransactionWindow.Details,
                            IsExpense = newAddTransactionWindow.IsExpense,
                            RefundableValue = newAddTransactionWindow.RefundableValue,
                            IsRefunded = newAddTransactionWindow.IsRefunded,
                            BudgetingCategory = newAddTransactionWindow.BudgetingCategory                        
                        };
                        BankAccounts.Find(account => account.Name == expense.BankInstitute).AddExpense(expense);
                        vwModel.Transactions.Add(new TransactionOverview(expense));
                    }
                    else
                    {
                        var income = new Income()
                        {
                            TotalValue = newAddTransactionWindow.TotalValue,
                            Partner = newAddTransactionWindow.Partner,
                            BankInstitute = newAddTransactionWindow.BankInstitute,
                            TransactionCategory = newAddTransactionWindow.TransactionCategory,
                            Date = newAddTransactionWindow.Date,
                            PeriodicityDuration = newAddTransactionWindow.PeriodicityDuration,
                            PeriodicityUnit = newAddTransactionWindow.PeriodicityUnit,
                            Details = newAddTransactionWindow.Details,
                            IsExpense = newAddTransactionWindow.IsExpense
                        };
                        BankAccounts.Find(account => account.Name == income.BankInstitute).AddIncome(income);
                        vwModel.Transactions.Add(new TransactionOverview(income));
                    }                                      
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

        }

        private void btn_filterTransaction_Click(object sender, RoutedEventArgs e)
        {
            var newFilterTransactionWindow = new FilterTransactionsWindow(vwModel);
            if (newFilterTransactionWindow.ShowDialog() ?? false)
            {

            }
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
                //
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
        #endregion

        
    }
}
