﻿using API.Contracts;
using API.Data;
using API.Utilities.Handlers.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    //membuat class generalrepository menggunakan generic 
    public class GeneralRepository<TEntity> : IGeneralRepository<TEntity> where TEntity : class
    {
        private readonly BookingManagementDbContext _context;
        public GeneralRepository(BookingManagementDbContext context)
        {
            _context = context;
        }
        //methood create 
        public TEntity? Create(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Add(entity);
                _context.SaveChanges();
                return entity;
            }
            catch (Exception ex)
            {
                if (ex.InnerException is not null && ex.InnerException.Message.Contains("IX_tb_m_employees_nik"))
                {
                    throw new ExceptionHandler("NIK already exists");
                }
                if (ex.InnerException is not null && ex.InnerException.Message.Contains("IX_tb_m_employees_email"))
                {
                    throw new ExceptionHandler("Email already exists");
                }
                if (ex.InnerException != null && ex.InnerException.Message.Contains("IX_tb_m_employees_phone_number"))
                {
                    throw new ExceptionHandler("Phone number already exists");
                }
                throw new ExceptionHandler(ex.InnerException?.Message ?? ex.Message);
            }


        }
        //method delete
        public bool Delete(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Remove(entity);
                _context.SaveChanges();
                return true;
            } catch 
            {
                throw;
            }
        }
        //method getall
        public IEnumerable<TEntity> GetAll()
        {
           return _context.Set<TEntity>().ToList();
        }
        //method getbyguid
        public TEntity? GetByGuid(Guid guid)
        {
            return _context.Set<TEntity>().Find(guid);
        }
        //method update
        public bool Update(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Update(entity);
                _context.SaveChanges();
                return true;
            } catch (Exception ex)
            {
                if (ex.InnerException is not null && ex.InnerException.Message.Contains("IX_tb_m_employees_nik"))
                {
                    throw new ExceptionHandler("NIK already exists");
                }
                if (ex.InnerException is not null && ex.InnerException.Message.Contains("IX_tb_m_employees_email"))
                {
                    throw new ExceptionHandler("Email already exists");
                }
                if (ex.InnerException != null && ex.InnerException.Message.Contains("IX_tb_m_employees_phone_number"))
                {
                    throw new ExceptionHandler("Phone number already exists");
                }
                throw new ExceptionHandler(ex.InnerException?.Message ?? ex.Message);
            }
        }
    }
}
