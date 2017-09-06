using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNet.Identity.EntityFramework;
using Repository.Models;

namespace OazachaosuRepository.Model {
  using Repository.Models.Enums;
  using System;
  using System.ComponentModel.DataAnnotations;

  [Serializable]
  public class User : IdentityUser, IUser {

    public long LocalId { get; set; }
    [MaxLength(32)]
    public string Name { get; set; }
    [MaxLength(32)]
    public string Password { get; set; }
    [MaxLength(32)]
    public string ApiKey { get; set; }
    public bool IsAdmin { get; set; }
    public DateTime CreateDateTime { get; set; }
    public DateTime LastLoginDateTime { get; set; }
    public int State { get; set; }
  }

  public class Hash {
    public static string GetMd5Hash(MD5 md5Hash, string input) {
      StringBuilder lBuilder = new StringBuilder();
      byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
      foreach (byte t in data) {
        lBuilder.Append(t.ToString("x2"));
      }
      return lBuilder.ToString();
    }
  }
}
