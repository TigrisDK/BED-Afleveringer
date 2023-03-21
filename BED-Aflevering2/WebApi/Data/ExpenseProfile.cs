using WebApi.Models.Expense;
using AutoMapper;


namespace WebApi.Data
{
    public class ExpenseProfile : Profile
    {
        public ExpenseProfile() 
        {
            CreateMap<Expense, ExpenseDto>();
            CreateMap<Expense, ExpenseDtoPost>().ReverseMap();
        }
    }
}
