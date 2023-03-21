using Microsoft.EntityFrameworkCore.Internal;


namespace WebApi.Models.Expense
{
    public class ExpenseDtoPost
    {
        [Microsoft.Build.Framework.Required] 
        public long ModelId { get; set; }
        [Microsoft.Build.Framework.Required]
        public long JobId { get; set; }
        public DateTime Date { get; set; }
        public string? Text { get; set; }

        [Microsoft.Build.Framework.Required]
        public decimal Amount { get; set; }
    }
}
