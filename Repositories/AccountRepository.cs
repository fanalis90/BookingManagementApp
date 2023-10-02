using BookingManagementApp.Contracts;
using BookingManagementApp.Data;
using BookingManagementApp.Models;

namespace BookingManagementApp.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        //membuat context dari booking management dbcontext
        private readonly BookingManagementDbContext _context;
        //melakukan dependency injection
        public AccountRepository(BookingManagementDbContext context) {
            _context = context;
        }
        //implementasi interface method create
        public Account? Create(Account account)
        {
            try
            {
                _context.Set<Account>().Add(account);
                _context.SaveChanges();
                return account;
            } catch
            {
                return null;
            }
        }
        //implementasi interface method delete
        public bool Delete(Account account)
        {
            try
            {
                _context.Set<Account>().Remove(account);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        //implementasi interface method getall
        public IEnumerable<Account> GetAll()
        {
            return _context.Set<Account>().ToList();
        }
        //implementasi interface method getbyguid
        public Account? GetByGuid(Guid guid)
        {
            return _context.Set<Account>().Find(guid);
        }
        //implementasi interface method update
        public bool Update(Account account)
        {
            try
            {
                _context.Set<Account>().Update(account);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
