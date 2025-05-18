using AutoMapper;
using HPAdmin.Data.DbContext;

namespace HPAdmin.UI.ViewModels.Base;

public abstract class ViewModelBase(AppDbContext context, IMapper mapper) : BindableBase
{
    public AppDbContext DbContext { get; } = context;
    public IMapper DataMapper { get; } = mapper;
}