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
    public DelegateCommand ViewLoadedCommand { get; }
    public DelegateCommand AddTaskCommand { get; }
    public DelegateCommand<HomeTaskModel> DeleteTaskCommand { get; }
    public DelegateCommand SaveDataCommand { get; }
    public ModelListBase<HomeTaskModel, HomeTaskDto> HomeTasks { get; } = [];

    private string _title = "Home Planner - Admin";
    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }


    public MainWindowViewModel(AppDbContext context, IMapper mapper) : base(context, mapper)
    {
        ViewLoadedCommand = new DelegateCommand(onViewLoaded);
        AddTaskCommand = new DelegateCommand(onAddTask);
        DeleteTaskCommand = new DelegateCommand<HomeTaskModel>(onDeleteTask);
        SaveDataCommand = new DelegateCommand(onSave);
    }


    private int _index;
    private void onAddTask()
    {
        var dto = new HomeTaskDto
        {
            Title = "New Task" + ++_index,
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