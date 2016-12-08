using AutoMapper.Configuration;
using FollowTheTask.BLL.Services.User.ViewModels;
using FollowTheTask.Web.Auth;

namespace FollowTheTask.Web.Mapping
{
    public static class MapperConfig
    {
        public static void LoadConfig(ref MapperConfigurationExpression config)
        {
            BLL.Mapping.MapperConfig.LoadConfig(ref config);

            config.CreateMap<AuthUser, RegisterViewModel>();
            config.CreateMap<RegisterViewModel, AuthUser>();
        }
    }
}