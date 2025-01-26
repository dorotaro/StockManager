using System.Text;

namespace StockManager.Models;

using System.ComponentModel.DataAnnotations;

public class StockItem
{
	public string Name { get; set; }
	/// <summary>
	/// International Securities Identification Number
	/// </summary>
	[MinLength(12)]
	[MaxLength(12)] //TODO: find a better way to do this
	public string ISIN { get; set; }
	
	public decimal Price { get; set; }
	
	public decimal Quantity { get; set; } //TODO: make sure this is decimal and not int

	public override string ToString()
	{
		var sb = new StringBuilder();
		sb.AppendLine($"Name: {Name}");
		sb.AppendLine($"ISIN: {ISIN}");
		sb.AppendLine($"Price: {Price}");
		sb.AppendLine($"Quantity: {Quantity}");
		
		return sb.ToString();
	}
}