using AutoMapper;

namespace MEMCore.API.Mapping
{
    public static class ExtensionMethod
    {
        public static string ExtTest(this string s)
        {
            return $"A Test only {s}";
        }
    }
    public class MEMMappingProfiles:Profile
    {
        public MEMMappingProfiles()
        {
            CreateMap<Domain.ExpenseCategory, Models.Category>()
                .ForMember(dest => dest.Id, source => source.MapFrom(scr => scr.Id))
                .ForMember(dest => dest.CategoryName, source => source.MapFrom(scr => scr.Category.ToString()));

            CreateMap<Domain.Currency, Models.Currency>();

            CreateMap<Domain.Expense, Models.Expense>()
                .ForMember(dest => dest.Id, source => source.MapFrom(scr => scr.Id))
                .ForMember(dest => dest.ExpenseTitle, source => source.MapFrom(scr => scr.ExpenseTitle))
                .ForMember(dest => dest.ExpensesAmount, source => source.MapFrom(scr => scr.ExpensesAmount))
                .ForMember(dest => dest.ExpenseDate, source => source.MapFrom(scr => scr.ExpenseDate))
                .ForMember(dest => dest.Signature, source => source.MapFrom(scr => scr.Signature))
                .ForMember(dest => dest.ExpenseDetail, source => source.MapFrom(scr => scr.ExpenseDetail.Detail == null ? null : scr.ExpenseDetail.Detail))
                .ForMember(dest => dest.CategoryId, source => source.MapFrom(scr => scr.ExpenseCategoryId))
                .ForMember(dest => dest.Category, source => source.MapFrom(scr => scr.Category.Category))
                .ForMember(dest => dest.CurrencyId, source => source.MapFrom(scr => scr.CurrencyId))
                .ForMember(dest => dest.Currency, source => source.MapFrom(scr => scr.Currency.CurrencyName))
                ;

            //CreateMap<Models.Expense, Domain.Expense>()
            //    .ForMember(dest => dest.ExpenseTitle, opt => opt.MapFrom(scr => scr.ExpenseTitle))
            //    .ForMember(dest => dest.ExpensesAmount, opt => opt.MapFrom(scr => scr.ExpensesAmount))
            //    .ForMember(dest => dest.ExpenseDate, opt => opt.MapFrom(scr => scr.ExpenseDate))
            //    .ForMember(dest => dest.Signature, opt => opt.MapFrom(scr => scr.Signature))
            //    .ForMember(dest => dest.ExpenseCategoryId, opt => opt.MapFrom(scr => scr.CategoryId))
            //    .ForMember(dest => dest.ExpenseDetail.Detail, opt => opt.MapFrom(scr => scr.ExpenseDetail))
            //    .ForMember(dest => dest.ExpenseDetail.Detail, opt => opt.MapFrom(scr => scr.ExpenseDetail.ExtTest()))       
            //    .ForMember(dest => dest.CurrencyId, opt => opt.MapFrom(scr => scr.CurrencyId))
            //     ;

            CreateMap<Models.ExpenseForUpdate, Domain.Expense>()
                .ForMember(dest => dest.ExpenseTitle, opt => opt.MapFrom(scr => scr.ExpenseTitle))
                .ForMember(dest => dest.ExpensesAmount, opt => opt.MapFrom(scr => scr.ExpensesAmount))
                .ForMember(dest => dest.ExpenseDate, opt => opt.MapFrom(scr => scr.ExpenseDate))
                .ForMember(dest => dest.Signature, opt => opt.MapFrom(scr => scr.Signature))
                .ForMember(dest => dest.ExpenseCategoryId, opt => opt.MapFrom(scr => scr.CategoryId))
                .ForMember(dest => dest.CurrencyId, opt => opt.MapFrom(scr => scr.CurrencyId))
                 ;
        }
    }
}
