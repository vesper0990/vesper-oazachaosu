using WordkiModelCore.DTO;

namespace Repository.Model.DTOConverters
{
    public static class ResultConverter
    {

        public static ResultDTO GetDTOFromModel(Result result)
        {
            return new ResultDTO()
            {
                Id = result.Id,
                GroupId = result.ParentId,
                Correct = result.Correct,
                Accepted = result.Accepted,
                Wrong = result.Wrong,
                Invisible= result.Invisible,
                TimeCount = result.TimeCount,
                TranslationDirection = result.TranslationDirection,
                LessonType= result.LessonType,
                DateTime = result.DateTime,
                State = result.State,
            };
        }

        public static Result GetModelFromDTO(ResultDTO result)
        {
            return new Result()
            {
                Id = result.Id,
                ParentId = result.GroupId,
                Correct = result.Correct,
                Accepted = result.Accepted,
                Wrong = result.Wrong,
                Invisible = result.Invisible,
                TimeCount = result.TimeCount,
                TranslationDirection = result.TranslationDirection,
                LessonType = result.LessonType,
                DateTime = result.DateTime,
                State = result.State,
            };
        }
    }
}
