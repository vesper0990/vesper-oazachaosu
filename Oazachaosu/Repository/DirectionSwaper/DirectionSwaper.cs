using Repository.Models;
using Repository.Models.Enums;

namespace Repository.Helper
{
    public class DirectionSwaper : IDirectionSwaper
    {
        public void Swap(IResult result)
        {
            result.TranslationDirection = result.TranslationDirection == TranslationDirection.FromFirst ? TranslationDirection.FromSecond : TranslationDirection.FromFirst;
        }
    }
}
