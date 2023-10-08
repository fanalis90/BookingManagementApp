using API.Repositories;
using API.Contracts;
using API.Data;
using API.Models;
using API.Utilities.Handlers.Exceptions;
using Microsoft.EntityFrameworkCore;
using API.DTOs.Accounts;
using API.Utilities.Handlers;
using System.Reflection.Metadata.Ecma335;

namespace API.Repositories
{
    public class AccountRepository : GeneralRepository<Account> , IAccountRepository
    {
        private readonly BookingManagementDbContext _context;
            public AccountRepository(BookingManagementDbContext context) : base(context) {
            _context = context;
        }

        public BookingManagementDbContext GetContext()
        {
            return _context;
        }
    }
}
