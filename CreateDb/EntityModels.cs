using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreateDb
{
/// <summary>
/// A 主表
/// </summary>
public class A
{
	/// <summary>
	/// ID
	/// </summary>
	[Display(Name = "ID", Prompt = "ID")]
	[Required(ErrorMessage = "請填寫{0}")]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }

	/// <summary>
	/// Guid
	/// </summary>
	[Display(Name = "Guid", Prompt = "Guid")]
	[Required(ErrorMessage = "請填寫{0}")]
	public Guid Guid { get; set; }

	/// <summary>
	/// 名稱
	/// </summary>
	[Display(Name = "名稱", Prompt = "名稱")]
	[Required(ErrorMessage = "請填寫{0}")]
	[StringLength(50, ErrorMessage = "{0} 長度要介於 {2} 及 {1} 之間")]
	public string Name { get; set; }

	public ICollection<ADetail> Details { get; set; }
}

/// <summary>
/// A 明細表
/// </summary>
public class ADetail
{
	/// <summary>
	/// ID
	/// </summary>
	[Display(Name = "ID", Prompt = "ID")]
	[Required(ErrorMessage = "請填寫{0}")]
	public int Id { get; set; }

	/// <summary>
	/// Guid
	/// </summary>
	[Display(Name = "Guid", Prompt = "Guid")]
	[Required(ErrorMessage = "請填寫{0}")]
	public Guid Guid { get; set; }

	/// <summary>
	/// A Guid
	/// </summary>
	[Display(Name = "A Guid", Prompt = "A Guid")]
	[Required(ErrorMessage = "請填寫{0}")]
	public Guid AGuid { get; set; }

	/// <summary>
	/// 名稱
	/// </summary>
	[Display(Name = "名稱", Prompt = "名稱")]
	[Required(ErrorMessage = "請填寫{0}")]
	[StringLength(50, ErrorMessage = "{0} 長度要介於 {2} 及 {1} 之間")]
	public string Name { get; set; }


	/// <summary>
	/// A
	/// </summary>
	public A A { get; set; }

}

}
