using AutoMapper;
using comandaXpress_api_net.Models;
using comandaXpress_api_net.Models.Dto;

namespace comandaXpress_api_net
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Pedido, PedidoDto>().ReverseMap();

        }
    }
}
