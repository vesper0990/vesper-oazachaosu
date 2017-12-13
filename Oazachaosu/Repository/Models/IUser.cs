using Repository.Models.Enums;
using System;

namespace Repository.Models
{
    public interface IUser
    {
        long LocalId { get; set; }
        string Name { get; set; }
        string Password { get; set; }
        string ApiKey { get; set; }
        DateTime CreateDateTime { get; set; }
        DateTime LastLoginDateTime { get; set; }
        bool IsAdmin { get; set; }

        bool IsLogin { get; set; }
        bool IsRegister { get; set; }
        DateTime DownloadTime { get; set; }
        TranslationDirection TranslationDirection { get; set; }
        bool AllWords { get; set; }
    }
}