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
                cfg.CreateMap<Group, GroupDTO>();
                cfg.CreateMap<Word, WordDTO>();
                cfg.CreateMap<Result, ResultDTO>();
            })
            .CreateMapper();
    }
}
