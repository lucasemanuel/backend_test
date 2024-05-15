
using Domain.Repositories;
using Domain.UseCases;

namespace CustomerApp.Services;

public class GenerateAccountNumberService : IGenerateNumberAccount
{
    private readonly IBankAccountRepository _bankAccountRepository;

    public GenerateAccountNumberService(IBankAccountRepository bankAccountRepository)
    {
        _bankAccountRepository = bankAccountRepository;
    }

    public string Generate()
    {
        Random rand = new Random();
        return rand.Next(10000000, 99999999).ToString();
    }
    
    public string GenerateUnique()
    {
        while (true)
        {
            var number = this.Generate();
            var bankAccount = _bankAccountRepository.GetByNumberAccount(number);
            if (bankAccount == null) {
                return number;
            }
        }
    }
}