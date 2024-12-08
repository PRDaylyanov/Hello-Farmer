using HelloFarmerWeb.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloFarmerWeb.DAL.Repositories
{
    public class AccountRepository
    {
        public void Create(Account account)
        {
            using (var connection = new DBConnection())
            {
                connection.Account.Add(account);

                connection.SaveChanges();
            }
        }

        public Account GetByEmail(string email)
        {
            using (var connection = new DBConnection())
            {
                return connection.Account.FirstOrDefault(a => a.Email == email);
            }
        }

        public List<Account> GetAll()
        {
            List<Account> accounts = new List<Account>();

            using (var connection = new DBConnection())
            {
                accounts = connection.Account.ToList();
            }

            return accounts;
        }
    }
}
