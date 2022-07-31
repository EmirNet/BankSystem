namespace BankSystem.Domain.Entities
{
    public class Client
    {
        public int Id { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int PersonalId { get; set; }
        public int Account { get; set; }
        public string MobileNumber { get; set; }
        public string Sex { get; set; }
        public Address? Address { get; set; }
        public Role? Role { get; set; }


    }
}
