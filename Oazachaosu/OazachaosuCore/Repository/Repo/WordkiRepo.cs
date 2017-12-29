using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class WordkiRepo : IWordkiRepo
    {

        private readonly IDatabaseContext dbContext;

        public DbContext Context { get { return dbContext.Context; } }

        public WordkiRepo(IDatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IQueryable<Group> GetGroups()
        {
            return dbContext.Groups.AsNoTracking().Include(x => x.Words).Include(x => x.Results);
        }

        public IQueryable<Group> GetGroups(long userId)
        {
            return GetGroups().Where(x => x.UserId == userId);
        }

        public Group GetGroup(long id)
        {
            return dbContext.Groups.SingleOrDefault(x => x.Id == id);
        }

        public IQueryable<Result> GetResults()
        {
            return dbContext.Results.AsNoTracking().Include(x => x.Group);
        }

        public IQueryable<Result> GetResults(long userId)
        {
            return GetResults().Where(x => x.Group.UserId == userId);
        }

        public IQueryable<Word> GetWords()
        {
            return dbContext.Words.AsNoTracking().Include(x => x.Group);
        }

        public IQueryable<Word> GetWords(long userId)
        {
            return GetWords().Where(x => x.Group.UserId == userId);
        }

        public void AddGroup(Group group)
        {
            dbContext.Groups.Add(group);
        }

        public void UpdateGroup(Group group)
        {
            dbContext.Groups.Update(group);
        }

        public void RemoveGroup(Group group)
        {

        }

        public void AddResult(Result result)
        {
            dbContext.Results.Add(result);
        }

        public void UpdateResult(Result result)
        {
            dbContext.Results.Update(result);
        }

        public void AddWord(Word word)
        {
            dbContext.Words.Add(word);
        }

        public void UpdateWord(Word word)
        {
            dbContext.Words.Update(word);
        }

        public void Add(object obj)
        {
            dbContext.Add(obj);
        }

        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }

        public Task SaveChangesAsync()
        {
            return dbContext.SaveChangesAsync();
        }
    }
}
