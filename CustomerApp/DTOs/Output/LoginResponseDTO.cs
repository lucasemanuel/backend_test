namespace CustomerApp.DTOs.Output;

public record LoginResponseDTO(LoginUserDTO user, string token);

public record LoginUserDTO(string? id, string email, string name, string role)
{
    public LoginUserDTO() : this(null, "", "", "")
    {
    }
}