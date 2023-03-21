using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebApi.Data;
using WebApi.Models.Expense;
using WebApi.Models.Job;

namespace WebApi.Models.Model
{
    public class ModelDto
    {
        public long ModelId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNo { get; set; }

        public string? AddresLine1 { get; set; }
        public string? AddresLine2 { get; set; }

        public string? Zip { get; set; }

        public string? City { get; set; }

        public DateTime BirthDay { get; set; }
        public double Height { get; set; }
        public int ShoeSize { get; set; }
        public string? HairColor { get; set; }
        public string? Comments { get; set; }
    }

    public class ModelDtoFull
    {
        public long ModelId { get; set; }
        
        public string? FirstName { get; set; }
        
        public string? LastName { get; set; }
        
        public string? Email { get; set; }
        
        public string? PhoneNo { get; set; }
        
        public string? AddressLine1 { get; set; }
        
        public string? AddressLine2 { get; set; }
        
        public string? Zip { get; set; }
        
        public string? City { get; set; }
        
        public DateTime BirthDay { get; set; }
        public double Height { get; set; }
        public int ShoeSize { get; set; }
        
        public string? HairColor { get; set; }
        
        public string? Comments { get; set; }

        public List<Job.JobDtoSimple> Jobs { get; set; }
        public List<Expense.ExpenseDto> Expenses { get; set; }
    }

    public class ModelDtoNoId
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNo { get; set; }

        public string? AddresLine1 { get; set; }
        public string? AddresLine2 { get; set; }

        public string? Zip { get; set; }

        public string? City { get; set; }

        public DateTime BirthDay { get; set; }
        public double Height { get; set; }
        public int ShoeSize { get; set; }
        public string? HairColor { get; set; }
        public string? Comments { get; set; }
    }
    /*
    public class ModelDto : ModelDtoFull
    {
        public List<JobListExpenseDto>? Jobs { get; set; }
        public List<Expense.Expense>? Expenses { get; set; }
    }

    public class ModelNameDto
    {
        public long ModelId { get; set; }
        [MaxLength(64)]
        public string? FirstName { get; set; }
        [MaxLength(32)]
        public string? LastName { get; set; }

       // public List<JobListExpenseDto>? Jobs { get; set; }
    }*/
}