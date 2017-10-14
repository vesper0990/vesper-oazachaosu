using Repository.Models;

namespace Repository.Helper
{
    public interface ILanguageSwaper
    {
        void Swap(IGroup group);
        void Swap(IWord word);
    }
}