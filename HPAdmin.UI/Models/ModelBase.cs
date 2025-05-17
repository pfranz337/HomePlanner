using HPAdmin.Data.Dto;

namespace HPAdmin.UI.Models;

public class ModelBase<TDto>(TDto dto) : BindableBase
    where TDto : DtoDataBase
{
    public TDto Dto { get; set; } = dto;
}