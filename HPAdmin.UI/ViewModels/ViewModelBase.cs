using AutoMapper;
using HPAdmin.Data;

namespace HPAdmin.UI.ViewModels;

public abstract class ViewModelBase(AppDbContext context, IMapper mapper) : BindableBase
{
    public AppDbContext DbContext { get; } = context;
    public IMapper DataMapper { get; } = mapper;
}