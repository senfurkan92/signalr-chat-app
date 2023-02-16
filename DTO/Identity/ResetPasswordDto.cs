using System.ComponentModel.DataAnnotations;

namespace DTO.Identity
{
	public class ResetPasswordDto
	{
		[Required(ErrorMessage = "Required")]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		[RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).{8,16}$", ErrorMessage = "At least one lower case, upper case and one number with between 8 and 16 characters")]
		public string Password { get; set; }

		[Compare("Password", ErrorMessage = "Not matched with Password")]
		[Display(Name = "Password (Check)")]
		public string RePassword { get; set; }
	}
}
