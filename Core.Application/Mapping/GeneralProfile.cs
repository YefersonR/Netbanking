using AutoMapper;
using Core.Application.DTOs.Account;
using Core.Application.ViewModels.Beneficiary;
using Core.Application.ViewModels.CreditCard;
using Core.Application.ViewModels.Loans;
using Core.Application.ViewModels.SavingsAccount;
using Core.Application.ViewModels.Transation;
using Core.Application.ViewModels.User;
using Core.Domain.Entities;

namespace Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Beneficiary, BeneficiaryViewModel>()
                .ReverseMap()
                    .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                            .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                                .ForMember(dest => dest.Updated, opt => opt.Ignore());

            CreateMap<Beneficiary, BeneficiarySaveViewModel>()
                .ForMember(dest => dest.HasError, opt => opt.Ignore())
                    .ForMember(dest => dest.Error, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                            .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                                .ForMember(dest => dest.Updated, opt => opt.Ignore());

            CreateMap<CreditCard, CreditCardViewModel>()
                .ReverseMap()
                    .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                            .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                                .ForMember(dest => dest.Updated, opt => opt.Ignore());

            CreateMap<CreditCard, CreditCardSaveViewModel>()
                .ReverseMap()
                    .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                            .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                                .ForMember(dest => dest.Updated, opt => opt.Ignore());

            CreateMap<Loans, LoansViewModel>()
                .ReverseMap()
                    .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                            .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                                .ForMember(dest => dest.Updated, opt => opt.Ignore());
            
            CreateMap<Loans, LoansSaveViewModel>()
                .ReverseMap()
                    .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                            .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                                .ForMember(dest => dest.Updated, opt => opt.Ignore());

            CreateMap<SavingsAccount, SavingsAccountViewModel>()
                .ReverseMap()
                    .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                            .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                                .ForMember(dest => dest.Updated, opt => opt.Ignore());


            CreateMap<SavingsAccount, SavingsAccountSaveViewModel>()
                .ReverseMap()
                    .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                            .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                                .ForMember(dest => dest.Updated, opt => opt.Ignore());

            CreateMap<Transations, TransationsViewModel>()
                .ReverseMap()
                    .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                            .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                                .ForMember(dest => dest.Updated, opt => opt.Ignore());

            CreateMap<Transations, TransationsSaveViewModel>()
                .ForMember(dest => dest.CreditCards, opt => opt.Ignore())
                .ForMember(dest => dest.Loans, opt => opt.Ignore())
                .ForMember(dest => dest.savingsAccounts, opt => opt.Ignore())
                .ForMember(dest => dest.CreditCards, opt => opt.Ignore())
                .ReverseMap()
                    .ForMember(dest => dest.Created, opt => opt.Ignore())
                        .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                            .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                                .ForMember(dest=>dest.Loans,opt=>opt.MapFrom(src=>src.Loan))
                                .ForMember(dest=>dest.CreditCard,opt=>opt.MapFrom(src=>src.CardNumber))
                                .ForMember(dest => dest.Updated, opt => opt.Ignore());

            CreateMap<Transations, TransationsInfoViewModel>()
                .ReverseMap()
                    .ForMember(dest => dest.Created, opt => opt.Ignore())
                        .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                            .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                                .ForMember(dest => dest.Updated, opt => opt.Ignore());


            CreateMap<AuthenticationRequest, LoginViewModel>()
                .ForMember(dest => dest.HasError, opt => opt.Ignore())
                    .ForMember(dest => dest.Error, opt => opt.Ignore())
                        .ReverseMap();
            CreateMap<RegisterRequest, UserSaveViewModel>()
                .ForMember(dest => dest.HasError, opt => opt.Ignore())
                    .ForMember(dest => dest.Error, opt => opt.Ignore())
                        .ReverseMap()
                        .ForMember(dest=>dest.isActive,opt=>opt.MapFrom(src=>src.isActive));
            CreateMap<ForgotPasswordRequest, ForgotPasswordViewModel>()
                .ForMember(dest => dest.HasError, opt => opt.Ignore())
                    .ForMember(dest => dest.Error, opt => opt.Ignore())
                        .ReverseMap();
            CreateMap<ResetPasswordRequest, ResetPasswordViewModel>()
                .ForMember(dest => dest.HasError, opt => opt.Ignore())
                    .ForMember(dest => dest.Error, opt => opt.Ignore())
                        .ReverseMap();
 
        }
    }
}
