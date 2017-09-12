using OazachaosuRepository.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OazachaosuRepository.Model;
using System.Threading.Tasks;
using Repository.Models.Language;

namespace OazachaosuRepository.Repo
{
    public class WordkiRepositoryTest : IWordkiRepository
    {
        private static int _groupCount = 20;
        private static List<Group> _groups;
        private static int _wordCounter = 0;
        private static int _resultCounter = 0;

        static WordkiRepositoryTest()
        {
            _groups = new List<Group>();
            Random random = new Random();

            for (int i = 0; i < _groupCount; i++)
            {
                var newGroup = new Group
                {
                    Id = i,
                    Name = $"Grupa {i}",
                    UserId = 1,
                    Language1Type = LanguageType.Polish,
                    Language2Type = LanguageType.French,
                    Results = new List<Result>(),
                    Words = new List<Word>(),
                };
                _groups.Add(newGroup);

                for (int j = 0; j < i + 5; j++)
                {
                    var newWord = new Word
                    {
                        Id = _wordCounter,
                        Language1 = $"Słowo {_wordCounter}",
                        Language2 = $"Word {_wordCounter}",
                        Drawer = (byte)random.Next(0, 4),
                        Language1Comment = $"Komentarz do słowa {_wordCounter}",
                        Language2Comment = $"Comment for word {_wordCounter}",
                        Visible = true,
                        UserId = 1,
                        GroupId = newGroup.Id,
                    };
                    _wordCounter++;
                    newGroup.Words.Add(newWord);
                }

                for (int j = 0; j < i + 1; j++)
                {
                    var newResult = new Result
                    {
                        Id = _resultCounter,
                        Accepted = (short)random.Next(10, 20),
                        Correct = (short)random.Next(10, 20),
                        Wrong = (short)random.Next(10, 20),
                        Invisibilities = (short)random.Next(10, 20),
                        GroupId = newGroup.Id,
                        UserId = 1,
                        DateTime = DateTime.Now.AddDays(-j),
                        LessonType = Repository.Models.Enums.LessonType.FiszkiLesson,
                        TimeCount = (short)random.Next(1000, 2000),
                        TranslationDirection = random.Next(1, 2) == 1 ? Repository.Models.Enums.TranslationDirection.FromFirst : Repository.Models.Enums.TranslationDirection.FromSecond,
                    };
                    newGroup.Results.Add(newResult);
                }

            }

        }

        public bool DeleteGroup(Group group)
        {
            return true;
        }

        public bool DeleteResult(Result result)
        {
            return true;
        }

        public bool DeleteWord(Word word)
        {
            return true;
        }

        public Group GetGroup(long userId, long groupId)
        {
            return _groups.FirstOrDefault(x => x.Id == groupId);
        }

        public IQueryable<Group> GetGroups(long userId)
        {
            return _groups.AsQueryable();
        }

        public IQueryable<Result> GetResults(long userId)
        {
            return _groups.SelectMany(x => x.Results).AsQueryable();
        }

        public IQueryable<Word> GetWords(long userId)
        {
            return _groups.SelectMany(x => x.Words).AsQueryable();
        }

        public bool InsertGroup(Group group)
        {
            return true;
        }

        public bool InsertResult(Result result)
        {
            return true;
        }

        public bool InsertWord(Word word)
        {
            return true;
        }

        public int SaveChanges()
        {
            return 1;
        }

        public Task<int> SaveChangesAsync()
        {
            return new Task<int>(() => { return 0; });
        }

        public bool UpdateGroup(Group group)
        {
            return true;
        }

        public bool UpdateResult(Result result)
        {
            return true;
        }

        public bool UpdateWord(Word word)
        {
            return true;
        }
    }
}