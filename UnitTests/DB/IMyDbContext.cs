using Microsoft.EntityFrameworkCore;
using System;

namespace UnitTests.DB
{
    public interface IMyDbContext : IDisposable
    {
        int SaveChanges();
        DbSet<EcoClean.Models.Enterprise.Enterprise> Entities { get; set; }
    }
}
