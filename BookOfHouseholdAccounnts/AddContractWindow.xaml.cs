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
        private bool isEditing;

        public AddContractWindow(ViewModel vwModel, Contract contract)
        {
            this.vwModel = vwModel;
            this.DataContext = vwModel;

            isEditing = !(contract == null);

            InitializeComponent();
            
            if (isEditing)
            {
                Contract = contract;
                txtBox_name.Text = Contract.Name;
                txtBox_value.Text = Contract.Value.ToString();
                datepicker_startDate.SelectedDate = Contract.StartDate;
                datepicker_endDate.SelectedDate = Contract.EndDate;
                txtBox_canellationPeriod.Text = Contract.CancellationPeriod.ToString(); 
                combobox_category.SelectedIndex = vwModel.BudgetingOptions.ToList().FindIndex(budget => budget == Contract.Category);
            }
            else
            {
                if (vwModel.BudgetingOptions.Count > 0) combobox_category.SelectedIndex = 0;
            }
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
            if (!isEditing) Contract = new Contract();

            Contract.Name = txtBox_name.Text;
            Contract.Value = Convert.ToDouble(txtBox_value.Text);
            Contract.StartDate = (DateTime)datepicker_startDate.SelectedDate;
            Contract.EndDate = (DateTime)datepicker_endDate.SelectedDate;
            Contract.CancellationPeriod = Convert.ToDouble(txtBox_canellationPeriod.Text);
            Contract.Category = Convert.ToString(combobox_category.SelectedItem);

            if (isEditing) ApplyChangesToView();

            DialogResult = true;
        }

        private void btn_cancelContract_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void ApplyChangesToView()
        {
            var viewElem = vwModel.Contracts.ToList().Find(ovrvw => ovrvw.ID == Contract.ID);

            // 'Contract' class does not implement INotifyPropertyChanged.
            // Hence changes in Contract object are not tracked - just changes in ObservableCollection<Contract>.
            vwModel.Contracts.Remove(viewElem);
            vwModel.Contracts.Add(Contract);
        }

    }
}
