﻿using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Oazachaosu.Core
{
    public class Word
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

        [Column("Language1")]
        public string Language1 { get; set; }

        [Column("Language2")]
        public string Language2 { get; set; }

        [Column("Language1Comment")]
        public string Language1Comment { get; set; }

        [Column("Language2Comment")]
        public string Language2Comment { get; set; }

        [Column("Drawer")]
        public byte Drawer { get; set; }

        [Column("Visible")]
        public bool IsVisible { get; set; }

        [Column("State")]
        public int State { get; set; }

        [Column("Selected")]
        public bool IsSelected { get; set; }

        [Column("RepeatingNumber")]
        public ushort RepeatingCounter { get; set; }

        [Column("LastRepeating")]
        public DateTime LastRepeating { get; set; }

        [Column("Comment")]
        public string Comment { get; set; }

        public DateTime LastChange { get; set; }

        public Word()
        {
            Id = -1;
            Language1 = string.Empty;
            Language2 = string.Empty;
            Language1Comment = string.Empty;
            Language2Comment = string.Empty;
            IsVisible = true;
            State = int.MaxValue;
            Comment = string.Empty;
        }

        public object Clone()
        {
            return new Word()
            {
                Id = Id,
                UserId = UserId,
                GroupId = GroupId,
                Language1 = Language1,
                Language2 = Language2,
                Language1Comment = Language1Comment,
                Language2Comment = Language2Comment,
                Drawer = Drawer,
                IsVisible = IsVisible,
                State = State,
                IsSelected = IsSelected,
                RepeatingCounter = RepeatingCounter,
                LastRepeating = LastRepeating,
                Comment = Comment
            };
        }
    }
}