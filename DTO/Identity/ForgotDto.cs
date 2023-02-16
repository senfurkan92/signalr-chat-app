using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DTO.Identity
{
	public class ForgotDto
	{
		[Required(ErrorMessage = "Required")]
		[EmailAddress(ErrorMessage = "Invalid email")]
		[Display(Name = "Email")]
		public string Email { get; set; }
	}
}
