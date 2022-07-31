namespace BankSystem.Application.Dtos
{
    public class CreateClientDto
    {
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int PersonalId { get; set; }
        public int Account { get; set; }
        public string MobileNumber { get; set; }
        public string Sex { get; set; }
        public AddressDto AddressDto { get; set; }
        public RoleDto RoleDto { get; set; }
    }
}
