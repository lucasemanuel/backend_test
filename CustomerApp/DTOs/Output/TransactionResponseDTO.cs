using Domain.Enums;

namespace CustomerApp.DTOs.Output;

public record TransactionResponseDTO(decimal amount, string type, string createdAt, string id)
{
    public TransactionResponseDTO() : this(0, "", "", "")
    {
    }
};
