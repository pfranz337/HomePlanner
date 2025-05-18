using System.Collections.ObjectModel;
using System.Collections.Specialized;
using HPAdmin.Data.Dto;
using HPAdmin.Shared.Enums;

namespace HPAdmin.UI.Models.Base;

public sealed class ModelListBase<TModel, TDto> : ObservableCollection<TModel>
    where TModel : ModelBase<TDto>
    where TDto : DtoDataBase
{
    public List<TDto> DtoList { get; } = [];

    public ModelListBase()
    {
        CollectionChanged += models_CollectionChanged;
    }

    public void ClearAll()
    {
        foreach (var model in Items)
        {
            Remove(model);
        }
    }

    protected override void InsertItem(int index, TModel item)
    {
        if (item.Dto.State == DtoState.Deleted)
            throw new InvalidOperationException("Nelze přidat model ve stavu Deleted.");

        if (DtoList.All(d => d.Id != item.Dto.Id))
        {
            DtoList.Add(item.Dto);
        }

        base.InsertItem(index, item);
    }

    protected override void RemoveItem(int index)
    {
        var item = Items[index];
        var dto = item.Dto;

        switch (dto.State)
        {
            case DtoState.New:
                DtoList.RemoveAll(d => d.Id == dto.Id);
                break;
            case DtoState.Clean:
            case DtoState.Modified:
            case DtoState.Deleted:
            default:
                dto.State = DtoState.Deleted;
                break;
        }

        base.RemoveItem(index);
    }



    private void models_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems is { Count: > 0 })
        {
            foreach (TModel model in e.NewItems)
            {
                if (DtoList.Any(d => d.Id == model.Dto.Id))
                    continue;

                DtoList.Add(model.Dto);
            }
        }

        if (e.OldItems is not { Count: > 0 })
            return;

        foreach (TModel model in e.OldItems)
        {
            switch (model.Dto.State)
            {
                case DtoState.New:
                    DtoList.Remove(model.Dto);
                    break;
                case DtoState.Clean:
                case DtoState.Modified:
                case DtoState.Deleted:
                default:
                    model.Dto.State = DtoState.Deleted;
                    break;
            }
        }
    }
}