using API.Repositories;
using BookingManagementApp.Contracts;
using BookingManagementApp.Data;
using BookingManagementApp.Models;
using System.Linq.Expressions;

namespace BookingManagementApp.Repositories
{
    //membuat university repo untuk jembatan menuju database dengan implemen contract interface
    public class UniversityRepository : GeneralRepository<University>, IUniversityRepository
    {
        public UniversityRepository(BookingManagementDbContext context) : base(context)
        {
        }
    }
}
