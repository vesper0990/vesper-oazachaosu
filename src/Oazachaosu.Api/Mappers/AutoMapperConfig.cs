﻿using AutoMapper;
using Oazachaosu.Core;
using Oazachaosu.Core.Common;

namespace Oazachaosu.Api.Mappers
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDTO>();
                cfg.CreateMap<UserDTO, User>();

                cfg.CreateMap<Group, GroupDTO>();
                cfg.CreateMap<GroupDTO, Group>();
                cfg.CreateMap<Group, GroupItemDTO>();
                cfg.CreateMap<GroupItemDTO, Group>();
                cfg.CreateMap<Group, GroupDetailDTO>();
                cfg.CreateMap<GroupDetailDTO, Group>();
                cfg.CreateMap<Group, GroupToAddDTO>();
                cfg.CreateMap<GroupToAddDTO, Group>();
                cfg.CreateMap<Group, GroupToEditDTO>();
                cfg.CreateMap<GroupToEditDTO, Group>();

                cfg.CreateMap<Word, WordDTO>();
                cfg.CreateMap<WordDTO, Word>();
                cfg.CreateMap<Word, WordToAddDTO>();
                cfg.CreateMap<WordToAddDTO, Word>();


                cfg.CreateMap<Result, ResultDTO>();
                cfg.CreateMap<ResultDTO, Result>();
            })
            .CreateMapper();
    }
}
