using System.Collections.ObjectModel;
using System.Collections.Specialized;
using HomePlanner.Shared.Dto;
using HomePlanner.Shared.Enums;

namespace HomePlanner.UI.Models.Base;

public sealed class ModelListBase<TModel, TDto> : ObservableCollection<TModel>
    where TModel : ModelBase<TDto>
    where TDto : DtoDataBase
{
    private List<TDto> DtoList { get; } = [];

    public ModelListBase()
    {
        CollectionChanged += models_CollectionChanged;
    }

    public List<TDto> GetDtoList()
    {
        return DtoList;
    }

    protected override void ClearItems()
    {
        DtoList.Clear();
        base.ClearItems();
    }

    protected override void InsertItem(int index, TModel item)
    {
        if (item.State == DtoState.Deleted)
            throw new InvalidOperationException("Nelze přidat model ve stavu Deleted.");

        if (DtoList.All(d => d.Id != item.Id))
        {
            DtoList.Add(item.GetDto());
        }

        base.InsertItem(index, item);
    }

    protected override void RemoveItem(int index)
    {
        var item = Items[index];
        var dto = item.GetDto();

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
                if (DtoList.Any(d => d.Id == model.Id))
                    continue;

                DtoList.Add(model.GetDto());
            }
        }

        if (e.OldItems is not { Count: > 0 })
            return;

        foreach (TModel model in e.OldItems)
        {
            switch (model.State)
            {
                case DtoState.New:
                    DtoList.Remove(model.GetDto());
                    break;
                case DtoState.Clean:
                case DtoState.Modified:
                case DtoState.Deleted:
                default:
                    model.State = DtoState.Deleted;
                    break;
            }
        }
    }
}