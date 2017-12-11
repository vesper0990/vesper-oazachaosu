namespace Repository.Models
{
    public static class WordExtenstions
    {

        public static void ChangeVisibility(this IWord word)
        {
            word.Visible = !word.Visible;
        }

    }
}
