using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models.Expense
{
    public class ExpenseDtoPost
    {
        [Microsoft.Build.Framework.Required]
        public long ModelId { get; set; }
        [Microsoft.Build.Framework.Required]
        public long JobId { get; set; }
        [Microsoft.Build.Framework.Required]
        [JsonProperty("date")]
        public DateTime Date { get; set; }
        [JsonProperty("text")]
        public string? Text { get; set; }
        [Column(TypeName = "decimal(9,2)")]
        [Microsoft.Build.Framework.Required]
        [JsonProperty("amount")]
        public decimal Amount { get; set; }

    }
}
