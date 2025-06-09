using System.Collections.ObjectModel;
using System.Windows;
using AutoMapper;
using HomePlanner.Data;
using HomePlanner.Data.DbContext;
using HomePlanner.Shared.Dto;
using HomePlanner.UI.Models;
using HomePlanner.UI.Models.Base;
using HomePlanner.UI.ViewModels.Base;
using Microsoft.EntityFrameworkCore;

namespace HomePlanner.UI.Controls.MVVM.ViewModels;

public class TasksControlViewModel : ViewModelBase, IRegionMemberLifetime, INavigationAware
{
    public bool KeepAlive => true;

    public DelegateCommand AddTaskCommand { get; }

    public DelegateCommand<HomeTaskModel> DeleteTaskCommand { get; }

    public DelegateCommand SaveDataCommand { get; }

    public ModelListBase<HomeTaskModel, HomeTaskDto> HomeTasks { get; } = [];



    public TasksControlViewModel(AppDbContext context, IMapper mapper) : base(context, mapper)
    {
        AddTaskCommand = new DelegateCommand(onAddTask);
        DeleteTaskCommand = new DelegateCommand<HomeTaskModel>(onDeleteTask);
        SaveDataCommand = new DelegateCommand(onSave);
    }



    public void OnNavigatedTo(NavigationContext navigationContext)
    {
        LoadData();
    }

    public bool IsNavigationTarget(NavigationContext navigationContext) => true;

    public void OnNavigatedFrom(NavigationContext navigationContext)
    {
        onSave();
    }
    public void LoadData()
    {
        HomeTasks.Clear();
        var dtoList = DbContext.HomeTasks.ToList();
        HomeTasks.AddRange(dtoList.Select(DataMapper.Map<HomeTaskModel>));
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

    private void onSave()
    {
        try
        {
            DbContext.HomeTasks.UpdateDbSet(HomeTasks.GetDtoList());
            DbContext.SaveChanges();
            LoadData();
        }
        catch (DbUpdateConcurrencyException _)
        {
            MessageBox.Show("Data byla změněna jiným uživatelem a budou přenačtena.");
            LoadData();
        }
    }

    private void onDeleteTask(HomeTaskModel task)
    {
        HomeTasks.Remove(task);
    }
} 