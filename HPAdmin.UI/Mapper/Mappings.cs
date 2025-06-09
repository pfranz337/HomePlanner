using HomePlanner.Shared.Dto;
using HomePlanner.UI.Models;

namespace HomePlanner.UI.Mapper;

public static class Mappings
{
    public static Dictionary<Type, Type> TypeMappings { get; } = new()
    {
        { typeof(HomeTaskDto), typeof(HomeTaskModel) },
    };
}