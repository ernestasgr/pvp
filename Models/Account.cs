using System;
using System.ComponentModel.DataAnnotations;

namespace test.Models
{
	public class Account
	{
		[Key]
		public int IDAccount { get; set; }

		[Required]
		public string? Nickname { get; set; }

		[Required]
		public string? Email { get; set; }

		[Required]
		public string? Hash { get; set; }

        [Required]
		public string? Role { get; set; }

		[Required]
		public string? Salt { get; set; }
	}
}