using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public interface IWordkiRepo
    {
        IQueryable<User> GetUsers();

        IQueryable<Group> GetGroups();
        IQueryable<Group> GetGroups(long userId);
        Group GetGroup(long id);

        IQueryable<Word> GetWords();
        IQueryable<Word> GetWords(long userId);

        IQueryable<Result> GetResults();
        IQueryable<Result> GetResults(long userId);

        void AddGroup(Group group);
        void UpdateGroup(Group group);
        void RemoveGroup(Group group);

        void AddResult(Result result);
        void UpdateResult(Result result);

        void AddWord(Word word);
        void UpdateWord(Word word);

        void SaveChanges();
        Task SaveChangesAsync();
    }
}
