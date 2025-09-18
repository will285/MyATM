using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMData.Data
{
    public class Transaction
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionId { get; set; }

        [Required]
        public bool IsDeposit { get; set; } = false;

        [Required]
        public bool IsTransfer { get; set; } = false;

        [Required]
        public double Amount { get; set; }

        [ForeignKey(nameof(FromAccount))]
        public int FromAccountNumber { get; set; }
        public Account FromAccount { get; set; }


        [ForeignKey(nameof(ToAccount))]
        public int? ToAccountNumber { get; set; }
        public Account? ToAccount { get; set; }
    }
}
