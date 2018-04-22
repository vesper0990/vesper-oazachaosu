namespace Oazachaosu.Core.Common
{
    public class GroupItemDTO
    {

        public long Id { get; set; }
        public string Name { get; set; }
        public LanguageType Language1 { get; set; }
        public LanguageType Language2 { get; set; }
        public int WordsCount { get; set; }
        public int ResultsCount { get; set; }

    }
}
