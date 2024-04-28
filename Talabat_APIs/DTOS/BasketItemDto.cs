using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.DTOS
{
	public class BasketItemDto
	{
		[Required]
		public int Id { get; set; }
		[Required]
		public string ProductName { get; set; }
		[Required]
		public string PictureUrl { get; set; }
		[Required]
		public string Catagory { get; set; }

		[Required]
		[Range(0.1,double.MaxValue,ErrorMessage ="Price Must be greter then zero")]
		public decimal Price { get; set; }
		[Required]
		public string Brand { get; set; }

		[Required]
		[Range(1, int.MaxValue, ErrorMessage = "Quantity Must be at least one item")]
		public int Quantity { get; set; }
	}
}
