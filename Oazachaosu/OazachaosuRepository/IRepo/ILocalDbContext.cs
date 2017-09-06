using System.Data.Entity;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using OazachaosuRepository.Model;

namespace OazachaosuRepository.IRepo {
  public interface ILocalDbContext {

    DbSet<Group> Groups { get; set; }
    DbSet<Word> Words { get; set; }
    DbSet<Result> Results { get; set; }

    DbSet<Article> Articles { get; set; }
    DbSet<Category> Categories { get; set; }
    DbSet<ArticleComment> ArticleComments { get; set; }

    IdentityDbContext<User> This { get; }

    int SaveChanges();

    Task<int> SaveChangesAsync();

  }
}