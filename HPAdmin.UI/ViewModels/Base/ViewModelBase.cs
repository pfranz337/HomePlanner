using AutoMapper;
using HomePlanner.Data.DbContext;

namespace HomePlanner.UI.ViewModels.Base;

public abstract class ViewModelBase(AppDbContext context, IMapper mapper) : BindableBase
{
    public AppDbContext DbContext { get; } = context;
    public IMapper DataMapper { get; } = mapper;
}