using HPAdmin.Data.Dto;
using HPAdmin.UI.Models;

namespace HPAdmin.UI.Mapper;

public static class Mappings
{
    public static Dictionary<Type, Type> TypeMappings { get; } = new Dictionary<Type, Type>
    {
        { typeof(HomeTaskDto), typeof(HomeTaskModel) },
        // Přidejte další mapování podle potřeby
    };
}