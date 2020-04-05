using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookOfHouseholdAccounnts
{
    public class TransactionFilter
    {
        public bool IsInstituteFilter { get; set; }
        public bool IsValueFilter { get; set; }
        public bool IsDateFilter { get; set; }
        public bool IsKeywordFilter { get; set; }
        public int MinimumValue { get; set; }
        public int MaximumValue { get; set; }
        public DateTime MinimumDate { get; set; }
        public DateTime MaximumDate { get; set; }
        public bool IsPartnerKeyword { get; set; }
        public bool IsDescriptionKeyword { get; set; }
        public string Keyword { get; set; }

    }
}
