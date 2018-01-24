using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Oazachaosu.Core
{
    public class WordkiRepo : IWordkiRepo
    {

        private readonly IDatabaseContext dbContext;

        public WordkiRepo(IDatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IQueryable<User> GetUsers()
        {
            return dbContext.Users.AsNoTracking();
        }

        public IQueryable<Group> GetGroups()
        {
            return dbContext.Groups.AsNoTracking();
        }

        public IQueryable<Group> GetGroups(long userId)
        {
            return GetGroups().Where(x => x.UserId == userId);
        }

        public Group GetGroup(long id, long userId)
        {
            return dbContext.Groups.SingleOrDefault(x => x.Id == id && x.UserId == userId);
        }

        public IQueryable<Result> GetResults()
        {
            return dbContext.Results.AsNoTracking();
        }

        public IQueryable<Result> GetResults(long userId)
        {
            return GetResults().Where(x => x.UserId == userId);
        }

        public IQueryable<Word> GetWords()
        {
            return dbContext.Words.AsNoTracking();
        }

        public IQueryable<Word> GetWords(long userId)
        {
            return GetWords().Where(x => x.UserId == userId);
        }

        public void AddUser(User user)
        {
            dbContext.Users.Add(user);
        }

        public void UpdateUser(User user)
        {
            dbContext.Users.Update(user);
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
