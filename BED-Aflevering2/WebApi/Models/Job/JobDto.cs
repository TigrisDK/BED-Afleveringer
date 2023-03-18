using System.ComponentModel.DataAnnotations;
using WebApi.Models.Model;

namespace WebApi.Models.Job
{
    public class JobDto
    {
        public long JobId { get; set; }
        [MaxLength(64)]
        public string? Customer { get; set; }
        public DateTime StartDate { get; set; }
        public int Days { get; set; }
        [MaxLength(128)]
        public string? Location { get; set; }
        [MaxLength(2000)]
        public string? Comments { get; set; }
        
    }

    public class JobListModelDto : JobDto
    {
        public List<Model.Model>? Models { get; set; }

    }

    public class JobListModelNamesDto : JobDto
    {
        public List<ModelNameDto>? Models { get; set; }
    }


    public class JobListExpenseDto : JobDto
    {
        public List<Expense.Expense>? Expenses { get; set; }

    }

    public class JobPutDto
    {
        public long JobId { get; set; }

        public DateTime StartDate { get; set; }
        public int Days { get; set; }
        public string? Location { get; set; }
        [MaxLength(2000)]
        public string? Comments { get; set; }
    }
}
