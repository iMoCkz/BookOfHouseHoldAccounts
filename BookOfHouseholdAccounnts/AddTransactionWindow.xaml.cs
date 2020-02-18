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
    public partial class AddTransactionWindow : Window
    {
        private ViewModel vwModel;
        public decimal TotalValue { get; private set; }
        public decimal RefundableValue { get; private set; }
        public bool IsRefunded { get; private set; }
        public string Partner { get; private set; }
        public string BankInstitute { get; private set; }
        public string BudgetingCategory { get; private set; }
        public string TransactionCategory { get; private set; }
        public DateTime Date{ get; private set; }
        public int PeriodicityDuration { get; private set; }
        public string PeriodicityUnit { get; private set; }
        public string Details { get; private set; }
        public bool IsExpense { get; set; }


        public AddTransactionWindow(ViewModel vwModel)
        {
            this.vwModel = vwModel;
            this.DataContext = vwModel;
            InitializeComponent();
            if (vwModel.BankInstituteOptions.Count > 0) combobox_bankInstituteExpense.SelectedIndex = 0;
            if (vwModel.BudgetingOptions.Count > 0) combobox_budgetingExpense.SelectedIndex = 0;
            if (vwModel.TransactionOptions.Count > 0) combobox_transactionExpense.SelectedIndex = 0;
        }

        private void CheckNumerousInput(TextBox txtBox_input)
        {
            var input = txtBox_input.Text;
            if (!Regex.IsMatch(input, "^[0-9.\\s]*$") || input.Count(x => x == '.') > 1)
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
            TotalValue = Convert.ToDecimal(txtBox_totalValueExpense.Text);
            RefundableValue = Convert.ToDecimal(txtBox_refundableValueExpense.Text);
            IsRefunded = Convert.ToBoolean(checkbox_isRefundedExpense.IsChecked);
            Partner = txtBox_partnerExpense.Text;
            BankInstitute = Convert.ToString(combobox_bankInstituteExpense.SelectedItem);
            BudgetingCategory = Convert.ToString(combobox_budgetingExpense.SelectedItem);
            TransactionCategory = Convert.ToString(combobox_transactionExpense.SelectedItem);
            Date = (DateTime)datepicker_transactionDateExpense.SelectedDate;
            if ((bool)checkbox_periodicityExpense.IsChecked)
            {
                PeriodicityDuration = Convert.ToInt16(UpDown_periodDuratiionExpense.Value);
                PeriodicityUnit = Convert.ToString(combobox_timeUnitExpense.SelectedItem);
            }            
            Details = txtBox_descriptionExpense.Text;
            IsExpense = tabCtrl_transaction.SelectedIndex == 0;
            DialogResult = true;
        }
    }
}
