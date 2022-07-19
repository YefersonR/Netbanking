using AutoMapper;
using Core.Application.ViewModels.Beneficiary;
using Core.Application.ViewModels.CreditCard;
using Core.Application.ViewModels.Loans;
using Core.Application.ViewModels.SavingsAccount;
using Core.Application.ViewModels.Transation;
using Core.Application.ViewModels.User;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Beneficiary, BeneficiaryViewModel>()
                .ReverseMap()
                    .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                        .ForMember(dest => dest.Created, opt => opt.Ignore())
                            .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                                .ForMember(dest => dest.Updated, opt => opt.Ignore());

            CreateMap<Beneficiary, BeneficiarySaveViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                        .ForMember(dest => dest.Created, opt => opt.Ignore())
                            .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                                .ForMember(dest => dest.Updated, opt => opt.Ignore())
                                    .ForMember(dest => dest.User, opt => opt.Ignore())
                                        .ForMember(dest => dest.BeneficiaryUser, opt => opt.Ignore());
            
            CreateMap<CreditCard, CreditCardViewModel>()
                .ReverseMap()
                    .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                        .ForMember(dest => dest.Created, opt => opt.Ignore())
                            .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                                .ForMember(dest => dest.Updated, opt => opt.Ignore());

            CreateMap<CreditCard, CreditCardSaveViewModel>()
                .ReverseMap()
                    .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                        .ForMember(dest => dest.Created, opt => opt.Ignore())
                            .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                                .ForMember(dest => dest.Updated, opt => opt.Ignore())
                                    .ForMember(dest => dest.User, opt => opt.Ignore());

            CreateMap<Loans, LoansViewModel>()
                .ReverseMap()
                    .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                        .ForMember(dest => dest.Created, opt => opt.Ignore())
                            .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                                .ForMember(dest => dest.Updated, opt => opt.Ignore())
                                    .ForMember(dest => dest.User, opt => opt.Ignore());
            
            CreateMap<Loans, LoansSaveViewModel>()
                .ReverseMap()
                    .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                        .ForMember(dest => dest.Created, opt => opt.Ignore())
                            .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                                .ForMember(dest => dest.Updated, opt => opt.Ignore())
                                    .ForMember(dest => dest.User, opt => opt.Ignore());

            CreateMap<SavingsAccount, SavingsAccountViewModel>()
                .ReverseMap()
                    .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                        .ForMember(dest => dest.Created, opt => opt.Ignore())
                            .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                                .ForMember(dest => dest.Updated, opt => opt.Ignore());


            CreateMap<SavingsAccount, SavingsAccountSaveViewModel>()
                .ReverseMap()
                    .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                        .ForMember(dest => dest.Created, opt => opt.Ignore())
                            .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                                .ForMember(dest => dest.Updated, opt => opt.Ignore())
                                    .ForMember(dest => dest.User, opt => opt.Ignore())
                                        .ForMember(dest => dest.Transations, opt => opt.Ignore())
                                            .ForMember(dest => dest.Beneficiaries, opt => opt.Ignore());

            CreateMap<Transations, TransationsViewModel>()
                .ReverseMap()
                    .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                        .ForMember(dest => dest.Created, opt => opt.Ignore())
                            .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                                .ForMember(dest => dest.Updated, opt => opt.Ignore());

            CreateMap<Transations, TransationsSaveViewModel>()
                .ReverseMap()
                    .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                        .ForMember(dest => dest.Created, opt => opt.Ignore())
                            .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                                .ForMember(dest => dest.Updated, opt => opt.Ignore())
                                    .ForMember(dest => dest.User, opt => opt.Ignore())
                                        .ForMember(dest => dest.AccountToPay, opt => opt.Ignore());

            CreateMap<User, UserViewModel>()
                .ReverseMap()
                    .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                        .ForMember(dest => dest.Created, opt => opt.Ignore())
                            .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                                .ForMember(dest => dest.Updated, opt => opt.Ignore());

            CreateMap<User, UserSaveViewModel>()
                .ForMember(dest => dest.ConfirmPassword, opt => opt.Ignore())
                .ReverseMap()
                    .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                        .ForMember(dest => dest.Created, opt => opt.Ignore())
                            .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                                .ForMember(dest => dest.Updated, opt => opt.Ignore())
                                    .ForMember(dest => dest.CardCredit, opt => opt.Ignore())
                                        .ForMember(dest => dest.PrincipalSavingAccount, opt => opt.Ignore())
                                            .ForMember(dest => dest.Beneficiary, opt => opt.Ignore())
                                                .ForMember(dest => dest.CreditCard, opt => opt.Ignore())
                                                    .ForMember(dest => dest.Transations, opt => opt.Ignore())
                                                        .ForMember(dest => dest.SavingsAccount, opt => opt.Ignore())
                                                            .ForMember(dest => dest.Loans, opt => opt.Ignore());
        }
    }
}
