using AutoMapper;
using HPAdmin.Data;
using HPAdmin.Data.DbContext;
using HPAdmin.Data.Dto;
using HPAdmin.UI.Models;
using HPAdmin.UI.Models.Base;
using HPAdmin.UI.ViewModels.Base;

namespace HPAdmin.UI.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public DelegateCommand ViewLoaded { get; }
    public DelegateCommand AddTask { get; }
    public DelegateCommand<HomeTaskModel> DeleteTask { get; }
    public DelegateCommand Save { get; }
    public ModelListBase<HomeTaskModel, HomeTaskDto> HomeTasks { get; } = [];

    private string _title = "Home Planner - Admin";
    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }


    public MainWindowViewModel(AppDbContext context, IMapper mapper) : base(context, mapper)
    {
        ViewLoaded = new DelegateCommand(onViewLoaded);
        AddTask = new DelegateCommand(onAddTask);
        DeleteTask = new DelegateCommand<HomeTaskModel>(onDeleteTask);
        Save = new DelegateCommand(onSave);
    }


    private int index = 0;
    private void onAddTask()
    {
        var dto = new HomeTaskDto
        {
            Title = "New Task" + ++index,
            Description = "New Task Description",
            IsCompleted = false
        };

        var model = DataMapper.Map<HomeTaskModel>(dto);
        HomeTasks.Add(model);
    }

    private void onViewLoaded()
    {
        var dtoList = DbContext.HomeTasks.ToList();
        foreach (var model in dtoList.Select(dto => DataMapper.Map<HomeTaskModel>(dto)))
        {
            HomeTasks.Add(model);
        }
    }

    private void onSave()
    {
        DbContext.HomeTasks.UpdateDbSet(HomeTasks.DtoList);
        DbContext.SaveChanges();
    }

    private void onDeleteTask(HomeTaskModel task)
    {
        HomeTasks.Remove(task);
    }
}