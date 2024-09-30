using System.ComponentModel.DataAnnotations;

namespace TestApplication.Models
{
    public class Account
    {
        [Key]
        public int UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
    }
}
