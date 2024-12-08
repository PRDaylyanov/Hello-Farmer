using HelloFarmerWeb.DAL.Models;
using HelloFarmerWeb.DAL.Repositories;
using System.Security.Principal;

namespace HelloFarmerWeb.BLL
{
    public class AccountService
    {
        static private Account currentAccount;

        private readonly AccountRepository accountRepository = new AccountRepository();

        public void CreateAccount(Account account)
        {
            var existingAccount = accountRepository.GetByEmail(account.Email);
            if (existingAccount != null)
            {
                throw new InvalidOperationException("An account with this email already exists.");
            }

            accountRepository.Create(account);
        }

        public Account ValidateLogin(string email, string password)
        {
            var accounts = accountRepository.GetAll();

            var account = accounts.FirstOrDefault(acc => acc.Email == email && acc.Password == password);
            return account;
        }

        public void SetCurrentAccount(Account account)
        {
            currentAccount = account;
        }

        public Account GetCurrentAccount()
        {
            return currentAccount;
        }
    }
}
