using Oazachaosu.Core.Common;
using System;
using System.Collections.Generic;

namespace Oazachaosu.Core
{

    public class Group
    {

        public long Id { get; set; }
        public long UserId { get; set; }
        public string Name { get; set; }
        public LanguageType Language1 { get; set; }
        public LanguageType Language2 { get; set; }
        public int State { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastChange { get; set; }
        public IList<Word> Words { get; set; }
        public IList<Result> Results { get; set; }


        public Group()
        {
            Id = 0;
            UserId = -1;
            Name = string.Empty;
            Language1 = LanguageType.Default;
            Language2 = LanguageType.Default;
            State = int.MaxValue;
            CreationDate = DateTime.Now;
            Results = new List<Result>();
            Words = new List<Word>();
        }

        public void AddWord(Word word)
        {
            word.GroupId = Id;
            Words.Add(word);

        }

        public void AddResult(Result result)
        {
            result.GroupId = Id;
            Results.Add(result);
        }

    }
}
