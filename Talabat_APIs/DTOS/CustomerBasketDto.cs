using System.ComponentModel.DataAnnotations;
using Talabat.Core.Entities;

namespace Talabat.APIs.DTOS
{
	public class CustomerBasketDto
	{
		[Required]
		public string ID { get; set; }
		public List<BasketItemDto> Items { get; set; }
	}
}
