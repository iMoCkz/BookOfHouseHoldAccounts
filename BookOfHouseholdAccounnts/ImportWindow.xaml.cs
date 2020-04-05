using Microsoft.Win32;
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
    /// Interaktionslogik für ImportWindow.xaml
    /// </summary>
    public partial class ImportWindow : Window
    {
        private ViewModel vwModel;

        public string FilePath { get; set; }
        public string BankInstitute { get; set; }
        public ImportWindow(ViewModel vwModel)
        {
            this.vwModel = vwModel;
            this.DataContext = vwModel;

            InitializeComponent();
        }

        private void FileImportPath()
        {
            var openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "CSV files (*.csv)|*.csv;|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);


            if (openFileDialog.ShowDialog() ?? false)
            {
                txtbx_filePath.Text = openFileDialog.FileName;
            }
        }

        private void btn_selectPath_Click(object sender, RoutedEventArgs e)
        {
            FileImportPath();
        }

        private void btn_import_Click(object sender, RoutedEventArgs e)
        {
            FilePath = txtbx_filePath.Text; 
            BankInstitute = ((BankInstituteView)combobox_bankInstituteImport.SelectedItem).Name;

            DialogResult = true;
        }
    }
}
