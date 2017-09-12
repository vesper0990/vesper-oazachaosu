using OazachaosuRepository.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OazachaosuRepository.Model;

namespace OazachaosuRepository.Repo
{
    public class BlogRepositoryTest : IBlogRepository
    {
        private static List<Category> _categories;
        private static int _categoryCount = 10;
        private static int _articleCounter = 0;

        static BlogRepositoryTest()
        {
            _categories = new List<Category>();
            for (int i = 0; i < _categoryCount; i++)
            {
                var newCategory = new Category
                {
                    Id = (short)i,
                    Name = $"Kategoria{i}",
                    Url = $"Kategoria{i}",
                    Articles = new List<Article>()
                };
                _categories.Add(newCategory);

                for (int j = 0; j < i; j++)
                {
                    var newArticle = new Article
                    {
                        Id = _articleCounter++,
                        Abstract = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum",
                        DateTime = DateTime.Now.AddDays(i),
                        Title = $"Artykuł{_articleCounter}",
                        ContentUrl = "test",
                        Categories = _categories.Where(x => x.Id == j).ToList(),
                    };
                    _categories.First(x => x.Id == j).Articles.Add(newArticle);
                }
            }

        }

        public bool AddCategoryToArticle(long articleId, long categoryId)
        {
            //unnessesery to implement
            return true;
        }

        public bool CheckArticleCategoryConnection(long articleId, long categoryId)
        {
            return _categories.FirstOrDefault(x => x.Id == categoryId).Articles.FirstOrDefault(x => x.Id == articleId) != null;
        }

        public Article GetArticleById(long id)
        {
            foreach (var cat in _categories)
            {
                var article = cat.Articles.FirstOrDefault(x => x.Id == id);
                if (article != null)
                {
                    return article;
                }
            }
            return null;
        }

        public Article GetArticleByUrl(string url)
        {
            foreach (var cat in _categories)
            {
                var article = cat.Articles.FirstOrDefault(x => x.ContentUrl == url);
                if (article != null)
                {
                    return article;
                }
            }
            return null;
        }

        public IQueryable<Article> GetArticles()
        {
            return _categories.SelectMany(x => x.Articles).AsQueryable();
        }

        public ICollection<Article> GetArticlesByCategory(string url)
        {
            return _categories.Where(x => x.Url == url).SelectMany(x => x.Articles).ToList();
        }

        public IQueryable<Category> GetCategories()
        {
            return _categories.AsQueryable();
        }

        public Category GetCategoryById(long id)
        {
            return _categories.FirstOrDefault(x => x.Id == (short)id);
        }

        public Category GetCategoryByUrl(string categoryUrl)
        {
            return _categories.FirstOrDefault(x => x.Url == categoryUrl);
        }

        public bool RemoveArticle(Article article)
        {
            return true;
        }

        public bool RemoveCategory(Category category)
        {
            return true;
        }

        public bool RemoveCategoryFromArticle(long articleId, long categoryId)
        {
            return true;
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public bool UpdateArticle(Article article)
        {
            return true;
        }
    }
}