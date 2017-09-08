﻿using System;

namespace Repository.Models {
  public interface IUser {
    long LocalId { get; set; }
    string Name { get; set; }
    string Password { get; set; }
    string ApiKey { get; set; }
    DateTime CreateDateTime { get; set; }
    DateTime LastLoginDateTime { get; set; }
    bool IsAdmin { get; set; }

  }
}