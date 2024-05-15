namespace CustomerApp.DTOs.Output;

public record BankAccountDTO(string accountNumber, decimal balance, bool isInactived, string id);


