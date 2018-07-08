using Oazachaosu.Core.Common;
using System;

namespace Oazachaosu.Core
{
    public class Result
    {
        public long Id { get; set; }

        public long GroupId { get; set; }
        public Group Group { get; set; }
        public long UserId { get; set; }
        public short Correct { get; set; }
        public short Accepted { get; set; }
        public short Wrong { get; set; }
        public short Invisible { get; set; }
        public short TimeCount { get; set; }
        public TranslationDirection TranslationDirection { get; set; }
        public LessonType LessonType { get; set; }
        public DateTime? DateTime { get; set; }
        public int State { get; set; }
        public DateTime? LastChange { get; set; }

        public Result()
        {
            Id = -1;
            State = int.MaxValue;
        }
    }
}
