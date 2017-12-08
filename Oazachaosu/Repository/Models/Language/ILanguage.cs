using System.Windows.Media.Imaging;

namespace Repository.Models.Language
{
    public interface ILanguage
    {

        LanguageType Type { get; }
        string Code { get; }
        string Description { get; }
        string Name { get; }
        string ShortName { get; }

    }
}