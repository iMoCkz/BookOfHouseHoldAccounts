using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaktionslogik für FilterTransactionsWindow.xaml
    /// </summary>
    public partial class FilterTransactionsWindow : Window
    {
        private TransactionFilter transFilter; 
        public FilterTransactionsWindow(ViewModel vwModel, TransactionFilter transactionFilter)
        {
            InitializeComponent();
            this.DataContext = vwModel;
            transFilter = transactionFilter;
            ApplyCurrentFilter(); 
        }

        private void btn_apply_Click(object sender, RoutedEventArgs e)
        {
            transFilter.IsInstituteFilter = (bool)chckbx_bankInstitute.IsChecked;
            transFilter.IsValueFilter = (bool)chckbx_valueRange.IsChecked;
            transFilter.IsDateFilter = (bool)chckbx_dateRange.IsChecked;
            transFilter.IsKeywordFilter = (bool)chckbx_keyword.IsChecked;
            if (transFilter.IsValueFilter)
            {
                transFilter.MinimumValue = (int)iud_min.Value;
                transFilter.MinimumValue = (int)iud_max.Value;
            }
            if (transFilter.IsDateFilter)
            {
                transFilter.MinimumDate = (DateTime)datepicker_start.SelectedDate;
                transFilter.MaximumDate = (DateTime)datepicker_end.SelectedDate;
            }
            if (transFilter.IsKeywordFilter)
            {
                transFilter.IsPartnerKeyword = (bool)chckbx_partner.IsChecked;
                transFilter.IsDescriptionKeyword = (bool)chckbx_description.IsChecked;
                transFilter.Keyword = txtbx_keyword.Text;
            }
            DialogResult = true;
        }

        private void ApplyCurrentFilter()
        {
            chckbx_bankInstitute.IsChecked = transFilter.IsInstituteFilter;
            chckbx_valueRange.IsChecked = transFilter.IsValueFilter;
            chckbx_dateRange.IsChecked = transFilter.IsDateFilter;
            chckbx_keyword.IsChecked = transFilter.IsKeywordFilter;
            iud_min.Value = transFilter.MinimumValue;
            iud_max.Value = transFilter.MinimumValue;
            datepicker_start.SelectedDate = transFilter.MinimumDate;
            datepicker_end.SelectedDate = transFilter.MaximumDate;
            chckbx_partner.IsChecked = transFilter.IsPartnerKeyword;
            chckbx_description.IsChecked = transFilter.IsDescriptionKeyword;
            txtbx_keyword.Text = transFilter.Keyword;
        }

        private void txtbx_keyword_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtbx_keyword.Text == "Enter your keyword")
            {
                txtbx_keyword.Text = String.Empty;
            }
        }

        private void txtbx_keyword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtbx_keyword.Text == String.Empty)
            {
                txtbx_keyword.Text = "Enter your keyword";
            }
        }

        private void iud_max_LostFocus(object sender, RoutedEventArgs e)
        {
            if (iud_max.Value == null)
            {
                iud_max.Value = 100;
            }
        }

        private void iud_min_LostFocus(object sender, RoutedEventArgs e)
        {
            if (iud_min.Value == null)
            {
                iud_min.Value = -100;
            }
        }
    }
}
