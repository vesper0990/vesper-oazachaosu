using System.Collections.Generic;
using System.Linq;
using OazachaosuRepository.IRepo;
using OazachaosuRepository.Model;

namespace OazachaosuRepository.Repo {
  public class BlogRepository : IBlogRepository {

    private readonly ILocalDbContext dbContext;

    public BlogRepository(ILocalDbContext dbContext) {
      this.dbContext = dbContext;
    }

    public IQueryable<Article> GetArticles() {
      return dbContext.Articles.AsNoTracking();
    }

    public ICollection<Article> GetArticlesByCategory(string categoryUrl) {
      return dbContext.Categories.Single(x => x.Url.Equals(categoryUrl)).Articles;
    }

    public IQueryable<Category> GetCategories() {
      return dbContext.Categories.AsNoTracking();
    }

    public Article GetArticleByUrl(string url) {
      return dbContext.Articles.Single(x => x.ContentUrl.Equals(url));
    }

    public Article GetArticleById(long id) {
      return dbContext.Articles.Find(id);
    }

    public Category GetCategoryById(long id) {
      return dbContext.Categories.Find(id);
    }

    public Category GetCategoryByUrl(string categoryUrl) {
      return dbContext.Categories.Single(x => x.Url.Equals(categoryUrl));
    }

    public bool UpdateArticle(Article article) {
      var currentArticle = dbContext.Articles.Find(article.Id);
      currentArticle.Abstract = article.Abstract;
      currentArticle.Title = article.Title;
      return true;
    }

    public bool RemoveArticle(Article article) {
      dbContext.Articles.Remove(article);
      return true;
    }

    public bool RemoveCategory(Category category) {
      dbContext.Categories.Remove(category);
      return true;
    }

    public bool RemoveCategoryFromArticle(long articleId, long categoryId) {
      var article = GetArticleById(articleId);
      if (article == null) {
        return false;
      }
      var category = article.Categories.SingleOrDefault(x => x.Id == categoryId);
      if (category == null) {
        return false;
      }
      article.Categories.Remove(category);
      return true;
    }

    public bool AddCategoryToArticle(long articleId, long categoryId) {
      var article = GetArticleById(articleId);
      if (article == null) {
        return false;
      }
      var category = GetCategoryById(categoryId);
      if (category == null || article.Categories.Contains(category)) {
        return false;
      }
      article.Categories.Add(category);
      return true;
    }

    public bool CheckArticleCategoryConnection(long articleId, long categoryId) {
      return dbContext.Articles.Find(articleId).Categories.Any(x => x.Id == categoryId);
    }

    public void SaveChanges() {
      dbContext.SaveChanges();
    }
  }
}