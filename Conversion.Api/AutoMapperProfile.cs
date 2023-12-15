using AutoMapper;
using Balance.Api.ViewModels.Convert;
using Balance.Domain.Entities;

namespace Bank.Api
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AccountBankRequest, AccountBank>();
            CreateMap<AccountBank, AccountBankResponse>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom((src, dest, dataMember, context) => src.RetornaDataAberturaFormatada()))
                .ForMember(dest => dest.Balance, opt => opt.MapFrom((src, dest, dataMember, context) => src.RetornaSaldoFormatado()));
        }
    }
}
