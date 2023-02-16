using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace DTO.Identity
{
	public class RegisterDto
	{
		[Required(ErrorMessage = "Required")]
		[Display(Name = "Name")]
		[MaxLength(50, ErrorMessage = "At most 50 characters")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Required")]
		[Display(Name = "Lastname")]
		[MaxLength(50, ErrorMessage = "At most 50 characters")]
		public string Surname { get; set; }

		[Required(ErrorMessage = "Required")]
		[Phone(ErrorMessage = "Invalid phone")]
		[Display(Name = "Phone")]
		public string PhoneNumber { get; set; }

		[Required(ErrorMessage = "Required")]
		[EmailAddress(ErrorMessage = "Invalid email")]
		[Display(Name = "Email")]
		[MaxLength(50, ErrorMessage = "At most 50 characters")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Required")]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		[RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).{8,16}$", ErrorMessage = "At least one lower case, upper case and one number with between 8 and 16 characters")]
		public string Password { get; set; }

		[Compare("Password", ErrorMessage = "Not matched with Password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password (Check)")]
		public string RePassword { get; set; }

		[DataType(DataType.Upload)]
        public IFormFile? Photo { get; set; }
    }
}
