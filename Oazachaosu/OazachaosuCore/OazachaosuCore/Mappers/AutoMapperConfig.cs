using AutoMapper;
using Repository;
using WordkiModelCore.DTO;

namespace OazachaosuCore.Mappers
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
                cfg.CreateMap<Result, ResultDTO>();
            })
            .CreateMapper();
    }
}
