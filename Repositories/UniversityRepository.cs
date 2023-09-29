using BookingManagementApp.Contracts;
using BookingManagementApp.Data;
using BookingManagementApp.Models;
using System.Linq.Expressions;

namespace BookingManagementApp.Repositories
{
    //membuat university repo untuk jembatan menuju database dengan implemen contract interface
    public class UniversityRepository : IUniversityRepository
    {
        //membuat context private dan readonly hanya untuk kelas ini
        private readonly BookingManagementDbContext _context;
        //melakukan dependency injection
        public UniversityRepository(BookingManagementDbContext context)
        {
            _context = context;
        }
        //implementasi dbcontext getall
        public IEnumerable<University> GetAll()
        {
            return _context.Set<University>().ToList();
        }
        //implementasi dbcontext getbyGuid
        public University? GetByGuid(Guid guid)
        {
            return _context.Set<University>().Find(guid);
        }
        //implementasi dbcontext create
        public University? Create(University university)
        {
            try{
                _context.Set<University>().Add(university);
                _context.SaveChanges();
                return university;
            }catch {
                return null;
            }
        }
        //implementasi dbcontext Update
        public bool Update(University university)
        {
            try
            {
                _context.Set<University>().Update(university);
                _context.SaveChanges();
                return true;
            } catch
            {
                return false;
            }
        }
        //implementasi dbcontext Delete
        public bool Delete(University university)
        {
            try
            {
                _context.Set<University>().Remove(university);
                _context.SaveChanges();
                return true;
            } catch
            {
                return false;
            }
        }

       
    }
}
