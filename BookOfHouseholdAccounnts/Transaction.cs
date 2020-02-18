using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookOfHouseholdAccounnts
{
    public class Transaction
    {
        public decimal TotalValue { get; set; }        
        public string Partner { get; set; }
        public string BankInstitute { get; set; }
        public string TransactionCategory { get; set; }
        public DateTime Date { get; set; }
        public int PeriodicityDuration { get; set; }
        public string PeriodicityUnit { get; set; }
        public string Details { get; set; }
        public bool IsExpense { get; set; }
        public string ID { get; set; } = Guid.NewGuid().ToString("N");
    }
}
