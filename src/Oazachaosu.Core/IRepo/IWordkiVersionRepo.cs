using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Oazachaosu.Core
{
    public interface IWordkiVersionRepo
    {

        IEnumerable<WordkiVersion> GetAll();
        Task<WordkiVersion> GetLast();
        Task Add(WordkiVersion version);
        void Remove(WordkiVersion version);
        Task SaveChangesAsync();

    }
}
