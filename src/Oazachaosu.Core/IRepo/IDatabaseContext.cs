﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Oazachaosu.Core
{
    public interface IDatabaseContext : IDisposable
    {

        DbSet<Group> Groups { get; set; }
        DbSet<Word> Words { get; set; }
        DbSet<Result> Results { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<WordkiVersion> WordkiVersions { get; set; }
        DbContext This { get; }

        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}