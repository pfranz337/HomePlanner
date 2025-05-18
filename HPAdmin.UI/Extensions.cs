using System.Collections.ObjectModel;
using HPAdmin.Data.Dto;
using HPAdmin.UI.Models.Base;

public static class Extensions
{
    public static TDto[] GetDtos<TDto, TModel>(this ObservableCollection<TModel> models)
        where TDto : DtoDataBase
        where TModel : ModelBase<TDto>
    {
        return models.Select(m => m.Dto).ToArray();
    }
}