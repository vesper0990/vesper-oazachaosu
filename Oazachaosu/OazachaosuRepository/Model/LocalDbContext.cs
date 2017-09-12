using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.AspNet.Identity.EntityFramework;
using OazachaosuRepository.IRepo;

namespace OazachaosuRepository.Model
{
    public class LocalDbContext : IdentityDbContext<User>, ILocalDbContext
    {

        public LocalDbContext()
          : base("DefaultConnection")
        {
        }

        public DbSet<Group> Groups { get; set; }
        public DbSet<Word> Words { get; set; }
        public DbSet<Result> Results { get; set; }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ArticleComment> ArticleComments { get; set; }
        public IdentityDbContext<User> This { get { return this; } }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Article>()
                      .HasMany<Category>(s => s.Categories)
                      .WithMany(c => c.Articles)
                      .Map(cs =>
                      {
                          cs.MapLeftKey("ArticleId");
                          cs.MapRightKey("CategoryId");
                          cs.ToTable("ArticleCategory");
                      });

            //modelBuilder.Entity<Word>().Property(x =>x.Drawer).

            // using System.Data.Entity.ModelConfiguration.Conventions;
            //Wyłącza konwencję, która automatycznie tworzy liczbę mnogą dla nazw tabel w bazie danych
            // Zamiast Kategorie zostałaby stworzona tabela o nazwie Kategories
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Wyłącza konwencję CascadeDelete
            // CascadeDelete zostanie włączone za pomocą Fluent API
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            //// Używa się Fluent API, aby ustalić powiązanie pomiędzy tabelami 
            //// i włączyć CascadeDelete dla tego powiązania
            //modelBuilder.Entity<Ogloszenie>().HasRequired(x => x.Uzytkownik)
            //    .WithMany(x => x.Ogloszenia)
            //    .HasForeignKey(x => x.UzytkownikId)
            //    .WillCascadeOnDelete(true);
        }

    }
}