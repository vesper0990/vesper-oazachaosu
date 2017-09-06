using System.Collections.Generic;
using System.Linq;
using OazachaosuRepository.Model;

namespace OazachaosuRepository.IRepo {
  public interface IBlogRepository {

    IQueryable<Article> GetArticles();
    ICollection<Article> GetArticlesByCategory(string url);
    IQueryable<Category> GetCategories();

    Article GetArticleByUrl(string url);
    Article GetArticleById(long id);

    Category GetCategoryById(long id);
    Category GetCategoryByUrl(string categoryUrl);

    bool UpdateArticle(Article article);

    bool RemoveArticle(Article article);
    bool RemoveCategory(Category category);

    bool RemoveCategoryFromArticle(long articleId, long categoryId);
    bool AddCategoryToArticle(long articleId, long categoryId);

    bool CheckArticleCategoryConnection(long articleId, long categoryId);

    void SaveChanges();

  }
}