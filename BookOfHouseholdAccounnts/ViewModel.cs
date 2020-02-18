using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BookOfHouseholdAccounnts
{
    public class ViewModel : INotifyPropertyChanged
    {
        #region IconImages
        public string addIconPath = Path.GetFullPath("pics/add.png");
        public string AddIconPath
        {
            get { return addIconPath; }
            set
            {
                addIconPath = value;
                NotifyPropertyChanged("AddIconPath");
            }
        }

        public string deleteIconPath = Path.GetFullPath("pics/delete.png");
        public string DeleteIconPath
        {
            get { return deleteIconPath; }
            set
            {
                deleteIconPath = value;
                NotifyPropertyChanged("DeleteIconPath");
            }
        }

        public string saveIconPath = Path.GetFullPath("pics/save.png");
        public string SaveIconPath
        {
            get { return saveIconPath; }
            set
            {
                saveIconPath = value;
                NotifyPropertyChanged("SaveIconPath");
            }
        }

        public string filterIconPath = Path.GetFullPath("pics/filter.png");
        public string FilterIconPath
        {
            get { return filterIconPath; }
            set
            {
                filterIconPath = value;
                NotifyPropertyChanged("FilterIconPath");
            }
        }

        public string editIconPath = Path.GetFullPath("pics/edit.png");
        public string EditIconPath
        {
            get { return editIconPath; }
            set
            {
                editIconPath = value;
                NotifyPropertyChanged("EditIconPath");
            }
        }

        public string exitIconPath = Path.GetFullPath("pics/exit.png");
        public string ExitIconPath
        {
            get { return exitIconPath; }
            set
            {
                exitIconPath = value;
                NotifyPropertyChanged("ExitIconPath");
            }
        }

        public string openIconPath = Path.GetFullPath("pics/open.png");
        public string OpenIconPath
        {
            get { return openIconPath; }
            set
            {
                openIconPath = value;
                NotifyPropertyChanged("OpenIconPath");
            }
        }

        public Uri infoIconPath = new Uri(Path.GetFullPath("info/open.png"));
        public Uri InfoIconPath
        {
            get { return infoIconPath; }
            set
            {
                infoIconPath = value;
                NotifyPropertyChanged("InfoIconPath");
            }
        }
        #endregion

        public ObservableCollection<TransactionOverview> Transactions { get; set; } =
            new ObservableCollection<TransactionOverview>();

        public ObservableCollection<string> BankInstituteOptions { get; set; } =
            new ObservableCollection<string>();

        public ObservableCollection<string> BudgetingOptions { get; set; } =
         new ObservableCollection<string>();

        public ObservableCollection<string> PeriodicityTimeUnit { get; set; } =
         new ObservableCollection<string>();

        public ObservableCollection<string> TransactionOptions { get; set; } =
         new ObservableCollection<string>();

        private DateTime currentDateTime = DateTime.Now;
        public DateTime CurrentDateTime
        {
            get { return currentDateTime; }
            set
            {
                currentDateTime = value;
                NotifyPropertyChanged("CurrentDateTime");
            }
        }

        private string bankInstituteFilterContent;
        public string BankInstituteFilterContent
        { 
            get { return bankInstituteFilterContent; }
            set
            {
                bankInstituteFilterContent = value;
                NotifyPropertyChanged("BankInstituteFilterContent");
            }
        }

        private string bankInstituteFilterChecked;
        public string BankInstituteFilterChecked
        {
            get { return bankInstituteFilterChecked; }
            set
            {
                bankInstituteFilterChecked = value;
                NotifyPropertyChanged("BankInstituteFilterChecked");
            }
        }

        private bool periodicityEnabled = false;
        public bool PeriodicityEnabled
        {
            get { return periodicityEnabled; }
            set
            {
                periodicityEnabled = value;
                NotifyPropertyChanged("PeriodicityEnabled");
            }
        }

        private decimal totalBalance = 0.0m;
        public decimal TotalBalance
        {
            get { return totalBalance; }
            set
            {
                totalBalance = value;
                NotifyPropertyChanged("TotalBalance");
                if (value < 0)
                {
                    TotalBalanceColor = Brushes.Red;
                }
                else
                {
                    if (value == 0)
                    {
                        TotalBalanceColor = Brushes.Black;
                    }
                    else
                    {
                        TotalBalanceColor = Brushes.Green;
                    }
                }
            }
        }

        private SolidColorBrush totalBalanceColor = Brushes.Black;
        public SolidColorBrush TotalBalanceColor
        {
            get { return totalBalanceColor; }
            set
            {
                totalBalanceColor = value;
                NotifyPropertyChanged("TotalBalanceColor");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }

    public class TransactionOverview : INotifyPropertyChanged
    {
        public TransactionOverview(Transaction transaction)
        {
            BankInstitute = transaction.BankInstitute;
            TransactionValue = transaction.TotalValue;
            TransactionPartner = transaction.Partner;
            Category = transaction.TransactionCategory;
            Date = transaction.Date.ToString("dd/MM/yyyy");
            ID = transaction.ID;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        //
        private string bankInstitute = "Unknown";
        public string BankInstitute
        {
            get { return bankInstitute; }
            set
            {
                bankInstitute = value;
                NotifyPropertyChanged("BankInstitute");
            }
        }
        //
        private decimal transactionValue = 0;
        public decimal TransactionValue
        {
            get { return transactionValue; }
            set
            {
                transactionValue = value;
                NotifyPropertyChanged("TransactionValue");
            }
        }
        //
        private string transactionPartner = "Unknown";
        public string TransactionPartner
        {
            get { return transactionPartner; }
            set
            {
                transactionPartner = value;
                NotifyPropertyChanged("TransactionPartner");
            }
        }
        //
        private string category = "Unknown";
        public string Category
        {
            get { return category; }
            set
            {
                category = value;
                NotifyPropertyChanged("Category");
            }
        }
        //
        private string date;
        public string Date
        {
            get { return date; }
            set
            {
                date = value;
                NotifyPropertyChanged("Date");
            }
        }
        public string ID { get; private set; }
    }
}
