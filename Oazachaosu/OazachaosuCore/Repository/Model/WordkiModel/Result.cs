using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WordkiModel.Enums;

namespace Repository
{
    public class Result
    {
        [Column("Id")]
        [Required]
        public long Id { get; set; }

        [JsonIgnore]
        public Group Group { get; set; }

        private long parentId;
        [Column("GroupId")]
        public long GroupId { get { return Group == null ? parentId : Group.Id; } set { parentId = value; } }

        [Column("Correct")]
        public short Correct { get; set; }

        [Column("Accepted")]
        public short Accepted { get; set; }

        [Column("Wrong")]
        public short Wrong { get; set; }

        [Column("Invisibilities")]
        public short Invisibilities { get; set; }

        [Column("TimeCount")]
        public short TimeCount { get; set; }

        [Column("TranslationDirection")]
        public TranslationDirection TranslationDirection { get; set; }

        [Column("LessonType")]
        public LessonType LessonType { get; set; }

        [Column("DateTime")]
        public DateTime DateTime { get; set; }

        [Column("State")]
        public int State { get; set; }

        [JsonIgnore]
        public DateTime LastChange { get; set; }

        public Result()
        {
            Id = -1;
            State = int.MaxValue;
        }
    }
}
