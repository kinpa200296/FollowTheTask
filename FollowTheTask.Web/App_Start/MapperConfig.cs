using AutoMapper;
using AutoMapper.Configuration;

namespace FollowTheTask.Web
{
    public class MapperConfig
    {
        public static void Initialize()
        {
            var config = new MapperConfigurationExpression();

            Mapping.MapperConfig.LoadConfig(ref config);

            Mapper.Initialize(config);
        }
    }
}