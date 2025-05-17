using System.Collections.ObjectModel;
using AutoMapper;
using HPAdmin.Data;
using HPAdmin.Data.Dto;
using HPAdmin.UI.Models;

namespace HPAdmin.UI.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public DelegateCommand ViewLoaded { get; }
    public DelegateCommand AddTask { get; }
    public DelegateCommand Save { get; }


    public ObservableCollection<HomeTaskModel> HomeTasks { get; } = [];

    private string _title = "Home Planner - Admin";

    public MainWindowViewModel(AppDbContext context, IMapper mapper) : base(context, mapper)
    {
        ViewLoaded = new DelegateCommand(onViewLoaded);
        AddTask = new DelegateCommand(onAddTask);
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

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }


    private void onViewLoaded()
    {
        var dtos = DbContext.HomeTasks.ToList();
        foreach (var model in dtos.Select(dto => DataMapper.Map<HomeTaskModel>(dto)))
        {
            HomeTasks.Add(model);
        }
    }

    private void onSave()
    {
        DbContext.HomeTasks.AddRange(HomeTasks.GetDtos<HomeTaskDto, HomeTaskModel>());
        DbContext.SaveChanges();
    }
}