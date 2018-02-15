using AutoMapper;
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
                cfg.CreateMap<Word, WordDTO>();
                cfg.CreateMap<WordDTO, Word>();
                cfg.CreateMap<Result, ResultDTO>();
                cfg.CreateMap<ResultDTO, Result>();
            })
            .CreateMapper();
    }
}
