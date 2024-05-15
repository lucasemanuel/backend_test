
using Domain.Enums;

namespace CustomerApp.DTOs.Input;

public record TransactionRequestDTO(decimal amount, TypeBankTransaction type, string bankAccountNumber);

