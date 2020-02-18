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
        public FilterTransactionsWindow(ViewModel vwModel)
        {
            this.DataContext = vwModel;
            InitializeComponent();

        }

        private void Btn_apply_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;

        }
    }
}
