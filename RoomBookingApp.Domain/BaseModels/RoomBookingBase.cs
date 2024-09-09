using System;
using System.ComponentModel.DataAnnotations;

namespace RoomBookingApp.Domain.BaseModels
{
    //no one can take instance
	public abstract class RoomBookingBase: IValidatableObject
	{
        [Required]
        [StringLength(80)]
        public string? FullName { get; set; } = string.Empty;

        [Required]
        [StringLength(80)]
        [EmailAddress]
        public string? Email { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime Date { get; set; } 

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(Date < DateTime.Now.Date)
            {
                yield return new ValidationResult("Date must be in future", new[] { nameof(Date) });
            }
        }
    }
}

