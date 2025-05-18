using AutoMapper;
using HPAdmin.Data.Dto;
using HPAdmin.UI.Models.Base;

namespace HPAdmin.UI.Mapper;

public class MappingProfile<TDto, TModel> : Profile where TDto : DtoDataBase where TModel : ModelBase<TDto>
{
    public MappingProfile()
    {
        CreateMap<TDto, TModel>()
            .ConstructUsing((dto, context) =>
            {
                var modelType = typeof(TModel);
                var instance = Activator.CreateInstance(modelType, dto);
                return instance switch
                {
                    null => throw new InvalidOperationException($"Cannot create instance of {modelType}"),
                    TModel model => model,
                    _ => throw new InvalidOperationException($"Cannot cast instance of {modelType} to {typeof(TModel)}")
                };
            });
    }
}