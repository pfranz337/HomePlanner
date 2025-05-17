namespace HPAdmin.Data.Dto;

public class HomeTaskDto : DtoDataBase
{
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
}