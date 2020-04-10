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
    /// Interaktionslogik für AddContractWindow.xaml
    /// </summary>
    public partial class AddContractWindow : Window
    {
        private ViewModel vwModel; 
        public Contract Contract { get; set; }

        public AddContractWindow(ViewModel vwModel)
        {
            this.vwModel = vwModel;
            this.DataContext = vwModel;

            InitializeComponent();

            if (vwModel.BudgetingOptions.Count > 0) combobox_category.SelectedIndex = 0;
        }

        private void txtBox_canellationPeriod_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckNumerousInput(txtBox_canellationPeriod);
        }
        private void txtBox_value_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckNumerousInput(txtBox_value);
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
              
        private void btn_addContract_Click(object sender, RoutedEventArgs e)
        {
            Contract = new Contract();
            Contract.Name = txtBox_name.Text;
            Contract.Value = Convert.ToDouble(txtBox_value.Text);
            Contract.StartDate = (DateTime)datepicker_startDate.SelectedDate;
            Contract.EndDate = (DateTime)datepicker_endDate.SelectedDate;
            Contract.CancellationPeriod = Convert.ToDouble(txtBox_canellationPeriod.Text);
            Contract.Category = Convert.ToString(combobox_category.SelectedItem);

            DialogResult = true;
        }

        private void btn_cancelContract_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
