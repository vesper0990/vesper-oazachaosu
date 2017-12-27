﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using WordkiModel.Enums;

namespace OazachaosuCore.Models
{
    public class Result
    {
        [Column("Id")]
        public long Id { get; set; }

        public Group Group { get; set; }

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

        public Result()
        {
            Id = -1;
            State = int.MaxValue;
        }
    }
}
