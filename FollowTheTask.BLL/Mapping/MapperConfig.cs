using AutoMapper.Configuration;
using AutoMapper.Mappers;
using FollowTheTask.BLL.Services.User.ViewModels;
using FollowTheTask.TransferObjects.User.DataObjects;

namespace FollowTheTask.BLL.Mapping
{
    public static class MapperConfig
    {
        public static void LoadConfig(ref MapperConfigurationExpression config)
        {
            DAL.Mapping.MapperConfig.LoadConfig(ref config);

            config.AddConditionalObjectMapper().Where((s, d) => s.Name.Replace("ViewModel", "Dto") == d.Name);
            config.AddConditionalObjectMapper().Where((s, d) => s.Name.Replace("Dto", "ViewModel") == d.Name);

            config.CreateMap<RegisterViewModel, UserDto>();
            config.CreateMap<UserDto, ManageUserViewModel>();
            config.CreateMap<UserDto, EditUserViewModel>();
            config.CreateMap<EditUserViewModel, UserDto>();
        }
    }
}
