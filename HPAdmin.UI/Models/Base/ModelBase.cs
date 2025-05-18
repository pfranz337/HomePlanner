using System.Runtime.CompilerServices;
using HPAdmin.Data.Dto;
using HPAdmin.Shared.Enums;

namespace HPAdmin.UI.Models.Base;

public class ModelBase<TDto>(TDto dto) : BindableBase
    where TDto : DtoDataBase
{
    public TDto Dto { get; set; } = dto;

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