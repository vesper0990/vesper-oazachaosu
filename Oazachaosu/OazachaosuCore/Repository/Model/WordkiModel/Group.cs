using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using WordkiModel;

namespace Repository
{
    public class Group
    {

        [Column("Id")]
        public long Id { get; set; }

        [Column("UserId")]
        public long UserId { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("Language1")]
        public LanguageType Language1 { get; set; }

        [Column("Language2")]
        public LanguageType Language2 { get; set; }

        [Column("State")]
        public int State { get; set; }

        [Column("CreationDate")]
        public DateTime CreationDate { get; set; }

        public IList<Word> Words { get; set; }

        public IList<Result> Results { get; set; }


        public Group()
        {
            Id = -1;
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
            word.Group = this;
            Words.Add(word);

        }

        public void AddResult(Result result)
        {
            result.Group = this;
            Results.Add(result);
        }

    }
}
