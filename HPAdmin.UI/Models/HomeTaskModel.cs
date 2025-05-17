using HPAdmin.Data.Data;

namespace HPAdmin.Models.Models;

public class ModelBase<TDto>(TDto dto) : BindableBase
    where TDto : DtoDataBase
{
    public TDto Dto { get; set; } = dto;
}

public class HomeTaskModel(HomeTaskDto dto) : ModelBase<HomeTaskDto>(dto)
{
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
}