using HPAdmin.Data.Dto;
using HPAdmin.UI.Models.Base;

namespace HPAdmin.UI.Models;

public class HomeTaskModel(HomeTaskDto dto) : ModelBase<HomeTaskDto>(dto)
{
    public string Title
    {
        get => Dto.Title;
        set => SetData(Dto.Title, value, (newValue) => Dto.Title = newValue);
    }

    public string Description
    {
        get => Dto.Description;
        set => SetData(Dto.Description, value, (newValue) => Dto.Description = newValue);
    }

    public bool IsCompleted
    {
        get => Dto.IsCompleted;
        set => SetData(Dto.IsCompleted, value, (newValue) => Dto.IsCompleted = newValue);
    }
}