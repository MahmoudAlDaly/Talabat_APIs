using AutoMapper;
using AutoMapper.Execution;
using Talabat.APIs.DTOS;
using Talabat.Core.Entities;

namespace Talabat.APIs.Helpers
{
	public class ProductPictureUrlResolver : IValueResolver<Product, ProductToReturnDTO, string>
	{
		private readonly IConfiguration Configuration;

		public ProductPictureUrlResolver(IConfiguration configuration)
        {
			Configuration = configuration;
		}

        public string Resolve(Product source, ProductToReturnDTO destination, string destMember, ResolutionContext context)
		{
			if (!string.IsNullOrEmpty(source.PictureUrl))
			{
				return $"{Configuration["ApiBaseUrl"]}/{source.PictureUrl}";
			}

			return string.Empty ;
		}
	}
}
