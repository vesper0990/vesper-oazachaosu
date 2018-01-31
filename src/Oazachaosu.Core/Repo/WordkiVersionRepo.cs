using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Oazachaosu.Core
{
    public class WordkiVersionRepo : IWordkiVersionRepo
    {

        private readonly IDatabaseContext dbContext;

        public WordkiVersionRepo(IDatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<WordkiVersion> GetAll()
        {
            return dbContext.WordkiVersions;
        }

        public async Task<WordkiVersion> GetLast()
        {
            return await dbContext.WordkiVersions.LastAsync();
        }

        public async Task Add(WordkiVersion version)
        {
            await dbContext.WordkiVersions.AddAsync(version);
        }

        public void Remove(WordkiVersion version)
        {
            dbContext.WordkiVersions.Remove(version);
        }

        public Task SaveChangesAsync()
        {
            return dbContext.SaveChangesAsync();
        }
    }
}
