using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions.Internal;
using System.Collections.Generic;
using System.Data.Common;
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

        public IEnumerable<Common.GroupItemDTO> GetGroupItems(long userId)
        {
            return from g in dbContext.Groups where g.UserId == userId && g.State >= 0
                   select new Common.GroupItemDTO()
                   {
                       Id = g.Id,
                       Language1 = g.Language1,
                       Language2 = g.Language2,
                       Name = g.Name,
                       WordsCount = g.Words.Count(x => x.State >= 0),
                       ResultsCount = g.Results.Count(x => x.State >= 0),
                       LastResultDate = g.Results.Select(x => x.DateTime).DefaultIfEmpty(null).Max(),
                   };
        }

        //public async Task<IEnumerable<Common.GroupItemDTO>> GetGroupItems(long userId)
        //{
        //    string query = "SELECT Id, Language1, Language2, Name," +
        //        "( SELECT COUNT(*) FROM test.Words words  WHERE words.GroupId = groups.Id) AS wordsCount ," +
        //        "( SELECT COUNT(*) FROM test.Words words WHERE words.GroupId = groups.Id ) AS resultCount  " +
        //        "FROM test.Groups groups WHERE groups.State >= 0;";
        //    var conn = dbContext.This.Database.GetDbConnection();
        //    List<Common.GroupItemDTO> list = new List<Common.GroupItemDTO>();
        //    try
        //    {
        //        await conn.OpenAsync();
        //        using (var command = conn.CreateCommand())
        //        {
        //            command.CommandText = query;
        //            DbDataReader reader = await command.ExecuteReaderAsync();

        //            if (reader.HasRows)
        //            {
        //                while (await reader.ReadAsync())
        //                {
        //                    list.Add(new Common.GroupItemDTO
        //                    {
        //                        Id = reader.GetInt64(0),
        //                        Language1 = (Common.LanguageType)reader.GetInt32(1),
        //                        Language2 = (Common.LanguageType)reader.GetInt32(2),
        //                        Name = reader.GetString(3),
        //                        WordsCount = reader.GetInt32(4),
        //                        ResultsCount = reader.GetInt32(5)
        //                    });
        //                }
        //            }
        //            reader.Dispose();
        //        }
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }
        //    return list;
        //}

        public Group GetGroup(long id, long userId)
        {
            return dbContext.Groups.SingleOrDefault(x => x.Id == id && x.UserId == userId);
        }

        public Group GetGroupWithChildren(long userId, long groupId)
        {
            return dbContext.Groups.Include(x => x.Words).Include(x => x.Results).SingleOrDefault(x => x.UserId == userId && x.Id == groupId);
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
