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

        [Column("GroupId")]
        [Required]
        public long GroupId { get; set; }
        public Group Group { get; set; }

        [Column("UserId")]
        [Required]
        public long UserId { get; set; }

        [Column("Correct")]
        public short Correct { get; set; }

        [Column("Accepted")]
        public short Accepted { get; set; }

        [Column("Wrong")]
        public short Wrong { get; set; }

        [Column("Invisible")]
        public short Invisible { get; set; }

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
