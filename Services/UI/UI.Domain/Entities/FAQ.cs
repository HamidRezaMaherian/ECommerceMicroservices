﻿using Services.Shared.Common;

namespace UI.Domain.Entities;
public class FAQ : EntityBase<string>
{
	public string Question { get; set; }
	public string Answer { get; set; }
	public int CategoryId { get; set; }
	#region NavigationProps
	public FaqCategory Category { get; set; }
	#endregion
}
