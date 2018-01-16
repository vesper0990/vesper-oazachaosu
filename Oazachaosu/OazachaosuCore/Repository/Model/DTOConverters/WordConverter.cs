using System.Collections.Generic;
using WordkiModelCore.DTO;

namespace Repository.Model.DTOConverters
{
    public static class WordConverter
    {

        public static WordDTO GetDTOFromModel(Word word)
        {
            return new WordDTO()
            {
                Id = word.Id,
                GroupId = word.GroupId,
                Language1 = word.Language1,
                Language2 = word.Language2,
                Language1Comment = word.Language1Comment,
                Language2Comment = word.Language2Comment,
                Drawer = word.Drawer,
                IsVisible = word.IsVisible,
                State = word.State,
                IsSelected = word.IsSelected,
                RepeatingCounter = word.RepeatingCounter,
                Comment = word.Comment,
                LastRepeating = word.LastRepeating,
            };
        }

        public static Word GetModelFromDTO(WordDTO word)
        {
            return new Word()
            {
                Id = word.Id,
                GroupId = word.GroupId,
                Language1 = word.Language1,
                Language2 = word.Language2,
                Language1Comment = word.Language1Comment,
                Language2Comment = word.Language2Comment,
                Drawer = word.Drawer,
                IsVisible = word.IsVisible,
                State = word.State,
                IsSelected = word.IsSelected,
                RepeatingCounter = word.RepeatingCounter,
                Comment = word.Comment,
                LastRepeating = word.LastRepeating,
            };
        }

        public static IEnumerable<Word> GetWordsFromDTOs(IEnumerable<WordDTO> words)
        {
            foreach(WordDTO word in words)
            {
                yield return GetModelFromDTO(word);
            }
        }

        public static IEnumerable<WordDTO> GetDTOsFromWords(IEnumerable<Word> words)
        {
            foreach (Word word in words)
            {
                yield return GetDTOFromModel(word);
            }
        }
    }
}
