using System.Runtime.CompilerServices;
using HomePlanner.Shared.Dto;
using HomePlanner.Shared.Enums;

namespace HomePlanner.UI.Models.Base;

public class ModelBase<TDto>(TDto dto) : BindableBase
    where TDto : DtoDataBase
{
    protected TDto Dto { get; } = dto;

    public Guid Id => Dto.Id;

    public DtoState State
    {
        get => Dto.State;
        set => SetData(Dto.State, value, (newValue) => Dto.State = newValue);
    }

    public TDto GetDto()
    {
        return Dto;
    }

    protected bool SetData<TValue>(
        TValue oldValue, 
        TValue newValue, 
        Action<TValue> updateAction, 
        [CallerMemberName] string? propertyName = null)
    {
        if (!SetProperty(ref oldValue, newValue, propertyName)) 
            return false;

        updateAction(newValue);

        if (Dto.State == DtoState.Clean)
            Dto.State = DtoState.Modified;

        return true;
    }
}