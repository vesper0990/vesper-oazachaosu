using System.Linq;
using System.Threading.Tasks;
using OazachaosuRepository.Model;

namespace OazachaosuRepository.IRepo {
  public interface IWordkiRepository {

    IQueryable<Group> GetGroups(long userId);
    IQueryable<Word> GetWords(long userId);
    IQueryable<Result> GetResults(long userId);

    Group GetGroup(long userId, long groupId);

    bool InsertGroup(Group group);
    bool UpdateGroup(Group group);
    bool DeleteGroup(Group group);

    bool InsertWord(Word word);
    bool UpdateWord(Word word);
    bool DeleteWord(Word word);

    bool InsertResult(Result result);
    bool UpdateResult(Result result);
    bool DeleteResult(Result result);

    int SaveChanges();
    Task<int> SaveChangesAsync();
  }
}