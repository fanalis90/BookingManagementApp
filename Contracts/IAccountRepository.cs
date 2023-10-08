﻿using API.Data;
using API.DTOs.Accounts;
using API.Models;

namespace API.Contracts
{
    public interface IAccountRepository : IGeneralRepository<Account>
    {
        BookingManagementDbContext GetContext();
        
    }
}
