using Domain.Enums;

namespace CustomerApp.DTOs.Input;

public record RequestCreateCustomerDTO(string name, string email, string password, Role role);