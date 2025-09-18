using System.ComponentModel.DataAnnotations;

namespace ATMData.Data
{
    public class Account
    {
        [Key]
        public required int AccountNumber { get; set; }
        public required string AccountName { get; set; }
        public string? AccountDescription { get; set; }
        public double Balance { get; set; } = 0.0;
    }
}
