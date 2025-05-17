namespace HPAdmin.Data.Dto;

public class DtoDataBase(Guid id)
{
    public Guid Id
    {
        get;
        set;
    } = id;

    protected DtoDataBase() : this(Guid.NewGuid())
    {
    }
}