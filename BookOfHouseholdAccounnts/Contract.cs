using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookOfHouseholdAccounnts
{
    public class Contract
    {
        public string Name { get; set; }
        public double Value { get; set; }
        public string StartDateStr { get; private set; }
        public string EndDateStr { get; private set; }
        public double CancellationPeriod { get; set; }
        public string Category { get; set; }

        private DateTime startDate;
        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                StartDateStr = value.Date.ToString("dd/MM/yyyy");
                startDate = value;
            }
        }

        private DateTime endDate;
        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                EndDateStr = value.Date.ToString("dd/MM/yyyy");
                endDate = value;
            }
        }
    }
}
