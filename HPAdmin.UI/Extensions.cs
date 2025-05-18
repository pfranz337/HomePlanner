using HPAdmin.Data.Dto;
using HPAdmin.UI.Models.Base;

namespace HPAdmin.UI;

public static class Extensions
{
    public static TDto[] GetDtoList<TDto, TModel>(this IEnumerable<TModel> models)
        where TDto : DtoDataBase
        where TModel : ModelBase<TDto>
    {
        return models.Select(m => m.GetDto()).ToArray();
    }
}