using System;
using System.Collections.Generic;
using System.Text;
using WordkiModelCore.DTO;

namespace Repository.Model.DTOConverters
{
    public static class UserConverter
    {

        public static UserDTO GetDTOFromModel(User user)
        {
            return new UserDTO()
            {
                Id = user.Id,
                Name = user.Name,
                Password = user.Password,
                ApiKey = user.ApiKey,
            };
        }

        public static User GetModelFromDTO(UserDTO user)
        {
            return new User()
            {
                Id = user.Id,
                Name = user.Name,
                Password = user.Password,
                ApiKey = user.ApiKey,
            };
        }
    }
}
