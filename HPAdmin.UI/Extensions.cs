using HomePlanner.Shared.Dto;
using HomePlanner.UI.Models.Base;

namespace HomePlanner.UI;

public static class Extensions
{
    public static TDto[] GetDtoList<TDto, TModel>(this IEnumerable<TModel> models)
        where TDto : DtoDataBase
        where TModel : ModelBase<TDto>
    {
        return models.Select(m => m.GetDto()).ToArray();
    }
}