using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace OazachaosuCore.Data
{
    public class ApplicationDbContext : DbContext, IDatabaseContext
    {

        public static bool test = false;

        public DbSet<Group> Groups { get; set; }
        public DbSet<Word> Words { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<User> Users { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public ApplicationDbContext() : base()
        {
        }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync(default(CancellationToken));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (test)
            {
                return;
            }
            optionsBuilder.UseMySql(@"Server=localhost;database=test;uid=root;pwd=Akuku123;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .Property(g => g.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<Group>()
                .Property(g => g.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<Group>()
                .HasKey(g => new { g.Id, g.UserId });

            builder.Entity<Result>()
                .HasKey(r => new { r.Id, r.UserId });
            builder.Entity<Result>()
                .HasOne(x => x.Group)
                .WithMany(x => x.Results)
                .HasForeignKey(x => new { x.GroupId, x.UserId })
                .HasConstraintName("FK_Result_Group");
            
            builder.Entity<Word>()
                .HasKey(r => new { r.Id, r.UserId });
            builder.Entity<Word>()
                .HasOne(x => x.Group)
                .WithMany(x => x.Words)
                .HasForeignKey(x => new { x.GroupId, x.UserId })
                .HasConstraintName("FK_Word_Group");
        }

    }
}
