using System;

namespace Oazachaosu.Core
{
    public class Word
    {
        public long Id { get; set; }
        public long GroupId { get; set; }
        public Group Group { get; set; }
        public long UserId { get; set; }
        public string Language1 { get; set; }
        public string Language2 { get; set; }
        public string Language1Comment { get; set; }
        public string Language2Comment { get; set; }
        public byte Drawer { get; set; }
        public bool IsVisible { get; set; }
        public int State { get; set; }
        public bool IsSelected { get; set; }
        public ushort RepeatingCounter { get; set; }
        public DateTime LastRepeating { get; set; }
        public string Comment { get; set; }

        public DateTime LastChange { get; set; }

        public Word()
        {
            Id = 0;
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
