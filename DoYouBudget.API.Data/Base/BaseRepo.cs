using DoYouBudget.API.Models.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;

namespace DoYouBudget.API.Data.Base
{
    public class BaseRepo<T> where T : DbContext
    {
        private readonly T _context;

        public BaseRepo(T context)
        {
            _context = context;
        }

        public bool SaveChanges()
        {
            var entries = _context.ChangeTracker.Entries().Where(baseDomain =>
                baseDomain is EntityEntry && (baseDomain.State == EntityState.Added || baseDomain.State == EntityState.Modified)
            );

            foreach (EntityEntry entry in entries)
            {
                BaseDomain baseDomain = ((BaseDomain)entry.Entity);
                baseDomain.ModifiedDate = DateTime.Now;
                if (entry.State == EntityState.Added)
                    baseDomain.CreatedDate = DateTime.Now;
            }

            return (_context.SaveChanges() > 0);
        }
    }
}
