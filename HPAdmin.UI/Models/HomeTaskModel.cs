using HPAdmin.Data.Dto;

namespace HPAdmin.UI.Models;

public class HomeTaskModel(HomeTaskDto dto) : ModelBase<HomeTaskDto>(dto)
{
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
}