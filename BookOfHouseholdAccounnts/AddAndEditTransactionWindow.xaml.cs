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

namespace BookOfHouseholdAccounnts
{
    /// <summary>
    /// Interaktionslogik für AddTransactionWindow.xaml
    /// </summary>
    public partial class AddAndEditTransactionWindow : Window
    {
        private ViewModel vwModel;
        public Expense TransExpense { get; set; }
        public Income TransIncome { get; set; }

        private bool isAddingTransaction;
        private bool isImportingTransaction;


        public AddAndEditTransactionWindow(ViewModel vwModel, Transaction transaction, bool isAdding, bool isImporting)
        {
            this.vwModel = vwModel;
            this.DataContext = vwModel;

            InitializeComponent();

            if (transaction != null)
            {
                if (transaction.GetType().Equals(typeof(Expense)))
                {
                    TransExpense = transaction as Expense;
                }
                else if (transaction.GetType().Equals(typeof(Income)))
                {
                    TransIncome = transaction as Income;
                }
            }

            isAddingTransaction = isAdding;
            isImportingTransaction = isImporting;

            tbItm_expense.IsEnabled = isAddingTransaction || TransExpense != null;
            tbItm_income.IsEnabled = isAddingTransaction || TransIncome != null;
            tabCtrl_transaction.SelectedIndex = tbItm_expense.IsEnabled ? 0 : 1;

            if (!isAddingTransaction)
            {
                InitTransaction();

                if (isImportingTransaction)
                {
                    btn_addExpense.Content = "Add";
                    btn_addIncome.Content = "Add";
                }
                else
                {
                    btn_addExpense.Content = "Edit";
                    btn_addIncome.Content = "Edit";
                }               
            }
            else
            {                
                btn_addIncome.Content = "Add";
                btn_addExpense.Content = "Add";

                if (vwModel.BankInstituteOptions.Count > 0) combobox_bankInstituteExpense.SelectedIndex = 0;
                if (vwModel.BankInstituteOptions.Count > 0) combobox_bankInstituteIncome.SelectedIndex = 0;
                if (vwModel.BudgetingOptions.Count > 0) combobox_budgetingExpense.SelectedIndex = 0;
                if (vwModel.TransactionOptions.Count > 0) combobox_transactionExpense.SelectedIndex = 0;
                if (vwModel.TransactionOptions.Count > 0) combobox_transactionIncome.SelectedIndex = 0;

                if (isImporting)
                {
                    InitTransaction();
                }
            }
        }

        private void InitTransaction()
        {
            if (TransExpense != null)
            {
                txtBox_totalValueExpense.Text = TransExpense.Value.ToString();
                txtBox_refundableValueExpense.Text = TransExpense.RefundableValue.ToString();
                checkbox_isRefundedExpense.IsChecked = TransExpense.IsRefunded;
                txtBox_partnerExpense.Text = TransExpense.Partner;
                combobox_bankInstituteExpense.SelectedIndex = vwModel.BankInstituteOptions.ToList().FindIndex(bank => bank.Name == TransExpense.BankInstitute);
                combobox_budgetingExpense.SelectedIndex = vwModel.BudgetingOptions.ToList().FindIndex(budget => budget == TransExpense.BudgetingCategory);
                combobox_transactionExpense.SelectedIndex = vwModel.TransactionOptions.ToList().FindIndex(transaction => transaction == TransExpense.TransactionCategory);
                datepicker_transactionDateExpense.SelectedDate = TransExpense.Date;
                checkbox_periodicityExpense.IsChecked = TransExpense.HasPeriodicity;
                iud_periodDurationExpense.Value = TransExpense.PeriodicityDuration;
                combobox_timeUnitExpense.SelectedIndex = vwModel.PeriodicityTimeUnit.ToList().FindIndex(unit => unit == TransExpense.PeriodicityUnit);
                txtBox_descriptionExpense.Text = TransExpense.Details;
            }
            else if (TransIncome != null)
            {
                txtBox_totalValueIncome.Text = TransIncome.Value.ToString();
                txtBox_partnerIncome.Text = TransIncome.Partner;
                combobox_bankInstituteIncome.SelectedIndex = vwModel.BankInstituteOptions.ToList().FindIndex(bank => bank.Name == TransIncome.BankInstitute);
                combobox_transactionIncome.SelectedIndex = vwModel.TransactionOptions.ToList().FindIndex(category => category == TransIncome.TransactionCategory);
                datepicker_transactionDateIncome.SelectedDate = TransIncome.Date;
                checkbox_periodicityIncome.IsChecked = TransIncome.HasPeriodicity;
                iud_periodDurationIncome.Value = TransIncome.PeriodicityDuration;
                combobox_timeUnitIncome.SelectedIndex = vwModel.PeriodicityTimeUnit.ToList().FindIndex(unit => unit == TransIncome.PeriodicityUnit);
                txtBox_descriptionIncome.Text = TransIncome.Details;
            }
            InformationFromImport();
        }

        private void InformationFromImport()
        {
            if (TransExpense != null)
            {
                if (TransExpense.Details.ToLower().Contains("kfz") || TransExpense.Details.ToLower().Contains("aral") || TransExpense.Details.ToLower().Contains("jet"))
                {
                    combobox_budgetingExpense.SelectedIndex = vwModel.BudgetingOptions.ToList().FindIndex(budget => budget == "Essentials");
                    combobox_transactionExpense.SelectedIndex = vwModel.TransactionOptions.ToList().FindIndex(category => category == "Car");
                }
                if (TransExpense.Details.ToLower().Contains("vattenfall") || TransExpense.Details.ToLower().Contains("wemag") || TransExpense.Details.ToLower().Contains("bev") ||
                    TransExpense.Details.ToLower().Contains("mitgas") || TransExpense.Details.ToLower().Contains("clauß") || TransExpense.Details.ToLower().Contains("telekom") ||
                    TransExpense.Details.ToLower().Contains("rsg group") || TransExpense.Details.ToLower().Contains("mcfit") || TransExpense.Details.ToLower().Contains("montana"))
                {
                    combobox_budgetingExpense.SelectedIndex = vwModel.BudgetingOptions.ToList().FindIndex(budget => budget == "Essentials");
                    combobox_transactionExpense.SelectedIndex = vwModel.TransactionOptions.ToList().FindIndex(category => category == "Housing");
                }
                if (TransExpense.Details.ToLower().Contains("techniker") || TransExpense.Details.ToLower().Contains("getsafe"))
                {
                    combobox_budgetingExpense.SelectedIndex = vwModel.BudgetingOptions.ToList().FindIndex(budget => budget == "Essentials");
                    combobox_transactionExpense.SelectedIndex = vwModel.TransactionOptions.ToList().FindIndex(category => category == "Insurance");
                }
                if (TransExpense.Details.ToLower().Contains("audible") || TransExpense.Details.ToLower().Contains("ruhr-universität"))
                {
                    combobox_budgetingExpense.SelectedIndex = vwModel.BudgetingOptions.ToList().FindIndex(budget => budget == "Essentials");
                    combobox_transactionExpense.SelectedIndex = vwModel.TransactionOptions.ToList().FindIndex(category => category == "Education");
                }
                if (TransExpense.Details.ToLower().Contains("paypal") || TransExpense.Partner.ToLower().Contains("paypal"))
                {
                    txtBox_partnerExpense.Text = "Paypal";
                    combobox_budgetingExpense.SelectedIndex = vwModel.BudgetingOptions.ToList().FindIndex(budget => budget == "Wants");
                    combobox_transactionExpense.SelectedIndex = vwModel.TransactionOptions.ToList().FindIndex(category => category == "Hobby");
                }
                if (TransExpense.Details.ToLower().Contains("amazon") || TransExpense.Partner.ToLower().Contains("amazon"))
                {
                    txtBox_partnerExpense.Text = "Amazon";
                    combobox_budgetingExpense.SelectedIndex = vwModel.BudgetingOptions.ToList().FindIndex(budget => budget == "Wants");
                    combobox_transactionExpense.SelectedIndex = vwModel.TransactionOptions.ToList().FindIndex(category => category == "Hobby");
                }

                if (TransExpense.Partner.ToLower().Contains("kaufland") || TransExpense.Partner.ToLower().Contains("netto") || TransExpense.Partner.ToLower().Contains("real") ||
                    TransExpense.Partner.ToLower().Contains("aldi") || TransExpense.Partner.ToLower().Contains("rewe"))
                {
                    combobox_budgetingExpense.SelectedIndex = vwModel.BudgetingOptions.ToList().FindIndex(budget => budget == "Essentials");
                    combobox_transactionExpense.SelectedIndex = vwModel.TransactionOptions.ToList().FindIndex(category => category == "Groceries");
                }
                if (TransExpense.Partner.ToLower().Contains("kaufland") || TransExpense.Details.ToLower().Contains("netto") || TransExpense.Details.ToLower().Contains("real"))
                {
                    combobox_budgetingExpense.SelectedIndex = vwModel.BudgetingOptions.ToList().FindIndex(budget => budget == "Essentials");
                    combobox_transactionExpense.SelectedIndex = vwModel.TransactionOptions.ToList().FindIndex(category => category == "Groceries");
                }
                if (TransExpense.Partner.ToLower().Contains("uci"))
                {
                    combobox_budgetingExpense.SelectedIndex = vwModel.BudgetingOptions.ToList().FindIndex(budget => budget == "Wants");
                    combobox_transactionExpense.SelectedIndex = vwModel.TransactionOptions.ToList().FindIndex(category => category == "Hobby");
                }
                if (TransExpense.Partner.ToLower().Contains("orlen") || TransExpense.Partner.ToLower().Contains("jet") || TransExpense.Partner.ToLower().Contains("audi"))
                {
                    combobox_budgetingExpense.SelectedIndex = vwModel.BudgetingOptions.ToList().FindIndex(budget => budget == "Wants");
                    combobox_transactionExpense.SelectedIndex = vwModel.TransactionOptions.ToList().FindIndex(category => category == "Hobby");
                }
            }
            else if (TransIncome != null)
            {
                if (TransIncome.Details.ToLower().Contains("kfz"))
                {
                    combobox_transactionIncome.SelectedIndex = vwModel.TransactionOptions.ToList().FindIndex(category => category == "Car");
                }
                if (TransIncome.Details.ToLower().Contains("arno") || TransIncome.Details.ToLower().Contains("monika") || TransIncome.Details.ToLower().Contains("peter"))
                {
                    combobox_transactionIncome.SelectedIndex = vwModel.TransactionOptions.ToList().FindIndex(category => category == "Financial Aid");
                }
                if (TransIncome.Details.ToLower().Contains("paypal"))
                {
                    txtBox_partnerIncome.Text = "Paypal";
                }

                if (TransIncome.Partner.ToLower().Contains("monika"))
                {
                    combobox_transactionIncome.SelectedIndex = vwModel.TransactionOptions.ToList().FindIndex(category => category == "Financial Aid");
                }
                if (TransIncome.Partner.ToLower().Contains("kostal") || TransIncome.Partner.ToLower().Contains("ifak"))
                {
                    combobox_transactionIncome.SelectedIndex = vwModel.TransactionOptions.ToList().FindIndex(category => category == "Regular Work");
                }
            }
        }
        private void CheckNumerousInput(TextBox txtBox_input)
        {
            var input = txtBox_input.Text;
            if (!Regex.IsMatch(input, "^[-0-9,\\s]*$") || input.Count(x => x == ',') > 1)
            {
                input = input.Substring(0, input.Length - 1);
                txtBox_input.Text = input;
                txtBox_input.CaretIndex = input.Length;
            }
        }

        private void txtBox_totalValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckNumerousInput(txtBox_totalValueExpense);
        }

        private void txtBox_refundableValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckNumerousInput(txtBox_refundableValueExpense);
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            if (isAddingTransaction || isImportingTransaction)
            {
                if (tabCtrl_transaction.SelectedIndex == 0)
                {
                    if (TransExpense == null)
                    {
                        TransExpense = new Expense();
                        TransExpense.IsExpense = true;
                    }
                }
                else
                {
                    if (TransIncome == null)
                    {
                        TransIncome = new Income();
                        TransIncome.IsExpense = false;
                    }
                }
            }            
            
            if (TransExpense != null)
            {
                var value = Convert.ToSingle(txtBox_totalValueExpense.Text);
                TransExpense.Value = value < 0 ? value : -value;
                TransExpense.RefundableValue = Convert.ToSingle(txtBox_refundableValueExpense.Text);
                TransExpense.IsRefunded = Convert.ToBoolean(checkbox_isRefundedExpense.IsChecked);
                TransExpense.Partner = txtBox_partnerExpense.Text;
                TransExpense.BankInstitute = ((BankInstituteView)combobox_bankInstituteExpense.SelectedItem).Name;
                TransExpense.BudgetingCategory = Convert.ToString(combobox_budgetingExpense.SelectedItem);
                TransExpense.TransactionCategory = Convert.ToString(combobox_transactionExpense.SelectedItem);
                TransExpense.Date = (DateTime)datepicker_transactionDateExpense.SelectedDate;
                TransExpense.HasPeriodicity = (bool)checkbox_periodicityExpense.IsChecked;
                if (TransExpense.HasPeriodicity)
                {
                    TransExpense.PeriodicityDuration = Convert.ToInt16(iud_periodDurationExpense.Value);
                    TransExpense.PeriodicityUnit = Convert.ToString(combobox_timeUnitExpense.SelectedItem);
                }
                TransExpense.Details = txtBox_descriptionExpense.Text;

                if (!isAddingTransaction && !isImportingTransaction) ApplyChangesToView(TransExpense);
            }
            else
            {
                TransIncome.Value = Convert.ToSingle(txtBox_totalValueIncome.Text);
                TransIncome.Partner = txtBox_partnerIncome.Text;
                TransIncome.BankInstitute = ((BankInstituteView)combobox_bankInstituteIncome.SelectedItem).Name;
                TransIncome.TransactionCategory = Convert.ToString(combobox_transactionIncome.SelectedItem);
                TransIncome.Date = (DateTime)datepicker_transactionDateIncome.SelectedDate;
                TransIncome.HasPeriodicity = (bool)checkbox_periodicityIncome.IsChecked;
                if (TransIncome.HasPeriodicity)
                {
                    TransIncome.PeriodicityDuration = Convert.ToInt16(iud_periodDurationIncome.Value);
                    TransIncome.PeriodicityUnit = Convert.ToString(combobox_timeUnitIncome.SelectedItem);
                }
                TransIncome.Details = txtBox_descriptionIncome.Text;

                if (!isAddingTransaction && !isImportingTransaction) ApplyChangesToView(TransIncome);
            }

            DialogResult = true;
        }

        private void ApplyChangesToView(Transaction transaction)
        {
            var overview = vwModel.Transactions.ToList().Find(ovrvw => ovrvw.ID == transaction.ID);

            overview.TransactionValue = transaction.Value;
            overview.BankInstitute = transaction.BankInstitute;
            overview.TransactionPartner = transaction.Partner;
            overview.Category = transaction.TransactionCategory;
            overview.Date = transaction.Date.ToString("dd/MM/yyyy"); 
        }
    }
}
