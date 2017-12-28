using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Repository
{
    public interface IDatabaseContext : IDisposable
    {

        DbSet<Group> Groups { get; set; }
        DbSet<Word> Words { get; set; }
        DbSet<Result> Results { get; set; }
        DbContext Context { get; }

        void Add(object obj);

        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}