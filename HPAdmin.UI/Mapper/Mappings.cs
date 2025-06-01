using HPAdmin.Shared.Dto;
using HPAdmin.UI.Models;

namespace HPAdmin.UI.Mapper;

public static class Mappings
{
    public static Dictionary<Type, Type> TypeMappings { get; } = new()
    {
        { typeof(HomeTaskDto), typeof(HomeTaskModel) },
    };
}