using AutoMapper;
using System;

namespace MEMCore.API.Mapping
{
    public static class ExtensionMethod
    {
        public static string ExtTest(this string s)
        {
            return $"A Test only {s}";
        }
    }
    public class MEMMappingProfiles : Profile
    {
        public MEMMappingProfiles()
        {
            CreateMap<Domain.ExpenseCategory, Models.Category>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.CategoryName, opts => opts.MapFrom(src => src.Category.ToString()));

            CreateMap<Domain.Currency, Models.Currency>();

            CreateMap<Domain.Expense, Models.ExpenseForList>()
                .ForMember(dest => dest.ExpenseTitle, opts => opts.MapFrom(src => src.ExpenseTitle))
                .ForMember(dest => dest.ExpensesAmount, opts => opts.MapFrom(src => src.ExpensesAmount))
                .ForMember(dest => dest.ExpenseDate, opts => opts.MapFrom(src => src.ExpenseDate))
                .ForMember(dest => dest.Signature, opts => opts.MapFrom(src => src.Signature))
                .ForMember(dest => dest.PaymentMethod, opts => opts.MapFrom(src => src.PaymentMethod)) // may not required to map with below approach
                .ForMember(dest => dest.PaymentType, opts => opts.MapFrom(src => src.PaymentType))  // may not required to map
                //.ForMember(dest => dest.PaymentMethodValue, opts => opts.MapFrom(src => Enum.GetName(typeof(Domain.PaymentMethod), src.PaymentMethod)))
                //.ForMember(dest => dest.PaymentTypeValue, opts => opts.MapFrom(src => Enum.GetName(typeof(Domain.PaymentType), src.PaymentType)))
                .ForMember(dest => dest.ExpenseDetail, opts => opts.MapFrom(src => src.ExpenseDetail.Detail == null ? null : src.ExpenseDetail.Detail))
                .ForMember(dest => dest.Category, opts => opts.MapFrom(src => src.Category.Category))
                .ForMember(dest => dest.Currency, opts => opts.MapFrom(src => src.Currency.CurrencyName))
                  ;

            CreateMap<Domain.Expense, Models.Expense>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.ExpenseTitle, opts => opts.MapFrom(src => src.ExpenseTitle))
                .ForMember(dest => dest.ExpensesAmount, opts => opts.MapFrom(src => src.ExpensesAmount))
                .ForMember(dest => dest.ExpenseDate, opts => opts.MapFrom(src => src.ExpenseDate))
                .ForMember(dest => dest.Signature, opts => opts.MapFrom(src => src.Signature))
                .ForMember(dest => dest.PaymentMethod, opts => opts.MapFrom(src => src.PaymentMethod))
                .ForMember(dest => dest.PaymentType, opts => opts.MapFrom(src => src.PaymentType))
               // .ForMember(dest => dest.PaymentMethodValue, opts => opts.MapFrom(src => Enum.GetName(typeof(Domain.PaymentMethod), src.PaymentMethod)))
                //.ForMember(dest => dest.PaymentTypeValue, opts => opts.MapFrom(src => Enum.GetName(typeof(Domain.PaymentType), src.PaymentType)))
                .ForMember(dest => dest.ExpenseDetail, opts => opts.MapFrom(src => src.ExpenseDetail.Detail == null ? null : src.ExpenseDetail.Detail))
                .ForMember(dest => dest.CategoryId, opts => opts.MapFrom(src => src.ExpenseCategoryId))
                .ForMember(dest => dest.Category, opts => opts.MapFrom(src => src.Category.Category))
                .ForMember(dest => dest.CurrencyId, opts => opts.MapFrom(src => src.CurrencyId))
                .ForMember(dest => dest.Currency, opts => opts.MapFrom(src => src.Currency.CurrencyName))
                ;

            CreateMap<Models.ExpenseForInsert, Domain.Expense>()
                .ForMember(dest => dest.ExpenseTitle, opts => opts.MapFrom(src => src.ExpenseTitle))
                .ForMember(dest => dest.ExpensesAmount, opts => opts.MapFrom(src => src.ExpensesAmount))
                .ForMember(dest => dest.ExpenseDate, opts => opts.MapFrom(src => src.ExpenseDate))
                .ForMember(dest => dest.Signature, opts => opts.MapFrom(src => src.Signature))
                .ForMember(dest => dest.PaymentMethod, opts => opts.MapFrom(src => src.PaymentMethod))
                .ForMember(dest => dest.PaymentType, opts => opts.MapFrom(src => src.PaymentType))
                // //.ForMember(dest => dest.ExpenseDetail, opts => opts.MapFrom(src => src.ExpenseDetail.Detail == null ? null : src.ExpenseDetail.Detail))
                // //.ForMember(dest => dest.ExpenseDetail.Detail, opts => opts.MapFrom(src => src.ExpenseDetail == null ? null : src.ExpenseDetail))
                .ForPath(dest => dest.ExpenseDetail.Detail, opts => opts.MapFrom(src => src.ExpenseDetail==null ? null: src.ExpenseDetail))          
                .ForMember(dest => dest.ExpenseCategoryId, opts => opts.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.CurrencyId, opts => opts.MapFrom(src => src.CurrencyId))
                
            ;
            //CreateMap<Models.Expense, Domain.Expense>()
            //    .ForMember(dest => dest.ExpenseTitle, opt => opt.MapFrom(src => src.ExpenseTitle))
            //    .ForMember(dest => dest.ExpensesAmount, opt => opt.MapFrom(src => src.ExpensesAmount))
            //    .ForMember(dest => dest.ExpenseDate, opt => opt.MapFrom(src => src.ExpenseDate))
            //    .ForMember(dest => dest.Signature, opt => opt.MapFrom(src => src.Signature))
            //    .ForMember(dest => dest.ExpenseCategoryId, opt => opt.MapFrom(src => src.CategoryId))
            //    .ForMember(dest => dest.ExpenseDetail.Detail, opt => opt.MapFrom(src => src.ExpenseDetail))
            //    .ForMember(dest => dest.ExpenseDetail.Detail, opt => opt.MapFrom(src => src.ExpenseDetail.ExtTest()))       
            //    .ForMember(dest => dest.CurrencyId, opt => opt.MapFrom(src => src.CurrencyId))
            //     ;

            CreateMap<Models.ExpenseForUpdate, Domain.Expense>()
                .ForMember(dest => dest.ExpenseTitle, opt => opt.MapFrom(src => src.ExpenseTitle))
                .ForMember(dest => dest.ExpensesAmount, opt => opt.MapFrom(src => src.ExpensesAmount))
                .ForMember(dest => dest.ExpenseDate, opt => opt.MapFrom(src => src.ExpenseDate))
                .ForMember(dest => dest.Signature, opt => opt.MapFrom(src => src.Signature))
                .ForMember(dest => dest.PaymentMethod, opts => opts.MapFrom(src => src.PaymentMethod))
                .ForMember(dest => dest.PaymentType, opts => opts.MapFrom(src => src.PaymentType))
                .ForMember(dest => dest.ExpenseDetail, opts => opts.MapFrom(src => src.ExpenseDetail != null ? src.ExpenseDetail : null))
                .ForMember(dest => dest.ExpenseCategoryId, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.CurrencyId, opt => opt.MapFrom(src => src.CurrencyId))
                 ;
          CreateMap<Domain.Expense, Models.ExpenseForUpdate>()
                .ForMember(dest => dest.ExpenseTitle, opt => opt.MapFrom(src => src.ExpenseTitle))
                .ForMember(dest => dest.ExpensesAmount, opt => opt.MapFrom(src => src.ExpensesAmount))
                .ForMember(dest => dest.ExpenseDate, opt => opt.MapFrom(src => src.ExpenseDate))
                .ForMember(dest => dest.Signature, opt => opt.MapFrom(src => src.Signature))
                .ForMember(dest => dest.PaymentMethod, opts => opts.MapFrom(src => src.PaymentMethod))
                .ForMember(dest => dest.PaymentType, opts => opts.MapFrom(src => src.PaymentType))
                .ForMember(dest => dest.ExpenseDetail, opts => opts.MapFrom(src => src.ExpenseDetail != null ? src.ExpenseDetail : null))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.ExpenseCategoryId))
                .ForMember(dest => dest.CurrencyId, opt => opt.MapFrom(src => src.CurrencyId))
                 ;
        }
    }
}
