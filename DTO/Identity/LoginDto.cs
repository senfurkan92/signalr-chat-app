using System.ComponentModel.DataAnnotations;

namespace DTO.Identity
{
	public class LoginDto
	{
		[Required(ErrorMessage = "Required")]
		[EmailAddress(ErrorMessage = "Invalid email")]
		[Display(Name = "Email")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Required")]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; }

		public bool RemeberMe { get; set; }
	}
}
