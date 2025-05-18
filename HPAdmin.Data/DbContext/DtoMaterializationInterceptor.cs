using HPAdmin.Data.Dto;
using HPAdmin.Shared.Enums;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace HPAdmin.Data.DbContext;

public class DtoMaterializationInterceptor : IMaterializationInterceptor
{
    public object InitializedInstance(MaterializationInterceptionData materializationData, object entity)
    {
        if (entity is DtoDataBase dto)
        {
            dto.State = DtoState.Clean;
        }

        return entity;
    }
}