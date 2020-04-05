using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookOfHouseholdAccounnts
{
    public class Expense : Transaction
    {
        public float RefundableValue { get; set; }
        public bool IsRefunded { get; set; }
        public string BudgetingCategory { get; set; }
    }
}
