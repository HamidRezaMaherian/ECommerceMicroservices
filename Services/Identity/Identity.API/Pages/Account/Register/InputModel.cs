// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using System.ComponentModel.DataAnnotations;

namespace Identity.API.Pages.Register;

public class InputModel
{
	[Required]
	public string Email { get; set; }

	[Required]
	public string UserName { get; set; }
	[Required]
	public string Password { get; set; }
	[Required]
	[Compare(nameof(Password))]
	public string ConfirmPassword { get; set; }

	public bool RememberLogin { get; set; }

	public string ReturnUrl { get; set; }

	public string Button { get; set; }
}