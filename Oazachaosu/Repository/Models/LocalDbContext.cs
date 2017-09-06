using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Repository.Models {
  public class LocalDbContext : DbContext {

    public LocalDbContext()
      : base("oazachaosuDb") {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Word> Words { get; set; }
    public DbSet<Result> Results { get; set; }

    public DbSet<Article> Articles { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Article_Category> ArticleCategories { get; set; }
    public DbSet<ArticleComment> ArticleComments { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder) {
      base.OnModelCreating(modelBuilder);

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