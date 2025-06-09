using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HomePlanner.Shared.Enums;

namespace HomePlanner.Shared.Dto;

public abstract class DtoDataBase(Guid id, DtoState state, byte[]? stamp)
{
    public Guid Id { get; set; } = id;

    [Timestamp]
    public byte[]? Stamp { get; set; } = stamp;

    [NotMapped]
    public DtoState State { get; set; } = state;

    protected DtoDataBase() : this(Guid.NewGuid(), DtoState.New, null)
    {
    }
}