using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StockManagerWithAI.Models
{
	public class StockItem
	{
		[Required]
		public string Name { get; set; }

		/// <summary>
		/// International Securities Identification Number
		/// </summary>
		[RegularExpression(@"^[A-Z]{2}[A-Z0-9]{10}$", ErrorMessage = "The ISIN must be exactly 12 characters and adhere to the ISIN structure.")]
		public string ISIN { get; set; }

		public decimal Price { get; set; }

		public decimal Quantity { get; set; }

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
}