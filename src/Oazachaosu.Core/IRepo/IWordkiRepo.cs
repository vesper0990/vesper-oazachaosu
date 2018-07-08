using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oazachaosu.Core
{
    public interface IWordkiRepo
    {
        IQueryable<User> GetUsers();

        IQueryable<Group> GetGroups();
        IQueryable<Group> GetGroups(long userId);
        Group GetGroup(long id, long userId);
        IEnumerable<Common.GroupItemDTO> GetGroupItems(long userId);
        Group GetGroupWithChildren(long userId, long groupId);

        IQueryable<Word> GetWords();
        IQueryable<Word> GetWords(long userId);

        IQueryable<Result> GetResults();
        IQueryable<Result> GetResults(long userId);

        void AddUser(User user);
        void UpdateUser(User user);

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
