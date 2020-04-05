using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BookOfHouseholdAccounnts
{
    public class BankAccount
    {
        public string Name { get; set; }

        public float Balance { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime DateOfCreation { get; set; }

        public string Description { get; set; }

        public List<Expense> Expenses { get; set; }
        public List<Income> Incomes { get; set; } 

        public BankAccount()
        {
            Expenses = new List<Expense>();
            Incomes = new List<Income>(); 
        }

        public BankAccount(string bankInstitute, float balance, DateTime balanceDate, string description)
        {
            Name = bankInstitute;
            Balance = balance;
            DateOfCreation = balanceDate;
            Description = description;
            Expenses = new List<Expense>();
            Incomes = new List<Income>();
        }

        public void AddExpense(Expense expense)
        {
            Expenses.Add(expense);
            if (DateTime.Compare(expense.Date, DateOfCreation) > 0)
            {
                if (expense.IsExpense)
                {
                    Balance -= expense.Value;
                }
                else
                {
                    Balance += expense.Value;
                }
            }
        }
        public void AddIncome(Income income)
        {
            Incomes.Add(income);
            if (DateTime.Compare(income.Date, DateOfCreation) > 0)
            {
                if (income.IsExpense)
                {
                    Balance -= income.Value;
                }
                else
                {
                    Balance += income.Value;
                }
            }
        }
    }
}
