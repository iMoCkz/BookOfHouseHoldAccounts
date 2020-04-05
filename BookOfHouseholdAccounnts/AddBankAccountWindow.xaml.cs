using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace BookOfHouseholdAccounnts
{
    /// <summary>
    /// Interaktionslogik für AddBankAccount.xaml
    /// </summary>
    public partial class AddBankAccountWindow : Window
    {
        public string BankAccountName { get; private set; }
        public float BankAccountBalance { get; private set; }
        public DateTime BankAccountBalanceDate { get; private set; }
        public string BankAccountDescription { get; private set; }
        
        public AddBankAccountWindow(ViewModel vwModel)
        {
            this.DataContext = vwModel;
            InitializeComponent();
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {            
            if (txtBox_name.Text.Replace(" ", "") != "" && txtBox_balance.Text.Replace(" ", "") != "")
            {
                BankAccountName = txtBox_name.Text;
                BankAccountBalance = Convert.ToSingle(txtBox_balance.Text);
                BankAccountBalanceDate = (DateTime)datepicker_balanceDate.SelectedDate;
                BankAccountDescription = txtBox_description.Text;
                DialogResult = true; 
            }            
        }

        private void txtBox_balance_TextChanged(object sender, TextChangedEventArgs e)
        {
            var currentBalance = txtBox_balance.Text;
            if (!Regex.IsMatch(currentBalance, "^[0-9.\\s]*$") || currentBalance.Count(x => x == '.') > 1)
            {
                currentBalance = currentBalance.Substring(0, currentBalance.Length - 1);
                txtBox_balance.Text = currentBalance;
                txtBox_balance.CaretIndex = currentBalance.Length;
            }
        }
    }
}
