using AutoMapper.Configuration;
using AutoMapper.Mappers;

namespace FollowTheTask.DAL.Mapping
{
    public static class MapperConfig
    {
        public static void LoadConfig(ref MapperConfigurationExpression config)
        {
            config.AddConditionalObjectMapper().Where((s, d) => s.Name.Replace("Model", "Dto") == d.Name);
            config.AddConditionalObjectMapper().Where((s, d) => s.Name.Replace("Dto", "Model") == d.Name);
        }
    }
}
