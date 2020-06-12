using AutoMapper;
using transactions_api.Models;
using transactions_api.Dto;

namespace transactions_api.MapperProfiles
{
    public class TransactionsProfiles : Profile
    {
        public TransactionsProfiles()
        {
            CreateMap<Transaction, TransactionReadDto>();
        }
    }
}