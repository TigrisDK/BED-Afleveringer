using AutoMapper;
using WebApi.Models.Model;

namespace WebApi.Data
{
    public class ModelProfile : Profile
    {
        public ModelProfile()
        {
            CreateMap<Model, ModelDto>().ReverseMap();
            CreateMap<Model, ModelDtoFull>();
            CreateMap<Model, ModelDtoNoId>().ReverseMap();
        }
    }
}
