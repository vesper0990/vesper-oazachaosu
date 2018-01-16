using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository
{
    public class Word
    {

        [Column("Id")]
        [Required]
        public long Id { get; set; }

        [JsonIgnore]
        [Required]
        public Group Group { get; set; }

        private long parentId;
        [NotMapped]
        public long ParentId { get { return Group == null ? parentId : Group.Id; } set { parentId = value; } }

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

        [JsonIgnore]
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
                Group = Group,
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
