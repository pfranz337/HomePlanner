using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HPAdmin.Shared.Enums;

namespace HPAdmin.Data.Dto;

public abstract class DtoDataBase(Guid id, DtoState state, byte[] stamp)
{
    public Guid Id { get; set; } = id;

    [Timestamp]
    public byte[] Stamp { get; set; } = stamp;

    [NotMapped]
    public DtoState State { get; set; } = state;

    protected DtoDataBase() : this(Guid.NewGuid(), DtoState.New, [])
    {
    }
}