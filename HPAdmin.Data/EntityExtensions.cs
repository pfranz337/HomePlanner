using HPAdmin.Shared.Dto;
using HPAdmin.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace HPAdmin.Data
{
    public static class EntityExtensions
    {
        public static void UpdateDbSet<TDto>(this DbSet<TDto> dbSet, IEnumerable<TDto> items) where TDto : DtoDataBase
        {
            foreach (var dto in items)
            {
                switch (dto.State)
                {
                    case DtoState.New:
                        dbSet.Add(dto);
                        dto.State = DtoState.Clean;
                        break;
                    case DtoState.Modified:
                        dbSet.Update(dto);
                        dto.State = DtoState.Clean;
                        break;
                    case DtoState.Clean:
                        break;
                    case DtoState.Deleted:
                        dbSet.Remove(dto);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}
