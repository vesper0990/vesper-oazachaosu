using Repository.Models;
using Repository.Models.Language;

namespace Repository.Helper
{
    public class LanguageSwaper : ILanguageSwaper
    {
        private IDirectionSwaper _directionSwaper;

        public LanguageSwaper()
        {
            _directionSwaper = new DirectionSwaper();
        }

        public void Swap(IWord word)
        {
            string temp = word.Language1;
            word.Language1 = word.Language2;
            word.Language2 = temp;

            temp = word.Language1Comment;
            word.Language1Comment = word.Language2Comment;
            word.Language2Comment = temp;
        }

        public void Swap(IGroup group)
        {
            if (group == null)
            {
                return;
            }
            LanguageType temp = group.Language1;
            group.Language1 = group.Language2;
            group.Language2 = temp;
            foreach (var word in group.Words)
            {
                Swap(word);
            }
            foreach (var result in group.Results)
            {
                _directionSwaper.Swap(result);
            }
        }

    }
}
