using Talabat.Core.Entities;

namespace Talabat.APIs.DTOS
{
	public class ProductToReturnDTO
	{
        public int ID { get; set; }
        public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public string PictureUrl { get; set; }

		public string Category { get; set; }
		public int CategoryID { get; set; }

		public string Brand { get; set; }
		public int BrandID { get; set; }
	}
}
