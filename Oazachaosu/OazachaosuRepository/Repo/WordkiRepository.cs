using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using OazachaosuRepository.IRepo;
using OazachaosuRepository.Model;

namespace OazachaosuRepository.Repo {
  public class WordkiRepository : IWordkiRepository {

    private readonly ILocalDbContext _dbContext;

    public WordkiRepository(ILocalDbContext dbContext) {
      _dbContext = dbContext;
      _dbContext.This.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
    }

    public IQueryable<Group> GetGroups(long userId) {
      return _dbContext.Groups.AsNoTracking().Where(x => x.UserId == userId);
    }

    public IQueryable<Word> GetWords(long userId) {
      return _dbContext.Words.AsNoTracking().Where(x => x.UserId == userId);
    }

    public IQueryable<Result> GetResults(long userId) {
      return _dbContext.Results.AsNoTracking().Where(x => x.UserId == userId);
    }

    public Group GetGroup(long userId, long groupId) {
      return _dbContext.Groups.Find(groupId, userId);
    }

    public bool InsertGroup(Group group) {
      group.State = 1;
      group.LastUpdateTime = DateTime.Now;
      _dbContext.Groups.Add(group);
      return true;
    }

    public bool UpdateGroup(Group group) {
      Group dbGroup = _dbContext.Groups.FirstOrDefault(x => x.Id == group.Id && x.UserId == group.UserId);
      if (dbGroup == null) {
        _dbContext.Groups.Add(group);
      } else {
        dbGroup.UpdateElement(group);
        _dbContext.This.Entry(dbGroup).State = EntityState.Modified;
      }
      return true;
    }

    public bool DeleteGroup(Group group) {
      Group dbGroup = _dbContext.Groups.FirstOrDefault(x => x.Id == group.Id && x.UserId == group.UserId);
      if (dbGroup == null) {
        return false;
      }
      dbGroup.State = int.MinValue;
      dbGroup.LastUpdateTime = DateTime.Now;
      _dbContext.This.Entry(dbGroup).State = EntityState.Modified;
      return true;
    }

    public bool InsertWord(Word word) {
      Group group = _dbContext.Groups.SingleOrDefault(x => x.Id == word.GroupId && x.UserId == word.UserId);
      if (group == null) {
        return false;
      }
      word.Visible = true;
      word.Drawer = 0;
      word.State = 1;
      word.LastUpdateTime = DateTime.Now;
      group.Words.Add(word);
      return true;
    }

    public bool UpdateWord(Word word) {
      word.LastUpdateTime = DateTime.Now;
      Group group = _dbContext.Groups.SingleOrDefault(x => x.Id == word.GroupId && x.UserId == word.UserId);
      if (group == null) {
        return false;
      }
      Word dbWord = group.Words.SingleOrDefault(x => x.Id == word.Id);
      if (dbWord == null) {
        group.Words.Add(word);
      } else {
        dbWord.UpdateElement(word);
        _dbContext.This.Entry(dbWord).State = EntityState.Modified;
      }
      return true;
    }

    public bool DeleteWord(Word word) {
      Word dbWord = _dbContext.Words.FirstOrDefault(x => x.Id == word.Id && x.UserId == word.UserId);
      if (dbWord == null) {
        return false;
      }
      dbWord.State = -1;
      dbWord.LastUpdateTime = DateTime.Now;
      _dbContext.This.Entry(dbWord).State = EntityState.Modified;
      return true;
    }

    public bool InsertResult(Result result) {
      Group group = _dbContext.Groups.SingleOrDefault(x => x.Id == result.GroupId && x.UserId == result.UserId);
      if (group == null) {
        return false;
      }
      result.LastUpdateTime = DateTime.Now;
      group.Results.Add(result);
      return true;
    }

    public bool UpdateResult(Result result) {
      result.LastUpdateTime = DateTime.Now;
      Group group = _dbContext.Groups.SingleOrDefault(x => x.Id == result.GroupId && x.UserId == result.UserId);
      if (group == null) {
        return false;
      }
      Result dbResult = group.Results.SingleOrDefault(x => x.Id == result.Id);
      if (dbResult == null) {
        group.Results.Add(result);
      } else {
        dbResult.UpdateElement(result);
        _dbContext.This.Entry(dbResult).State = EntityState.Modified;
      }
      return true;
    }

    public bool DeleteResult(Result result) {
      Result dbResult = _dbContext.Results.FirstOrDefault(x => x.Id == result.Id && x.UserId == result.UserId);
      if (dbResult == null) {
        return false;
      }
      dbResult.State = int.MinValue;
      dbResult.LastUpdateTime = DateTime.Now;
      _dbContext.This.Entry(dbResult).State = EntityState.Modified;
      return true;
    }

    public int SaveChanges() {
      return _dbContext.SaveChanges();
    }

    public async Task<int> SaveChangesAsync() {
      return await _dbContext.SaveChangesAsync();
    }
  }
}