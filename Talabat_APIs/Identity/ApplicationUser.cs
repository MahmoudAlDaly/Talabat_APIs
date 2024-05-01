using Microsoft.AspNetCore.Identity;

namespace Talabat.APIs.Identity
{
	public class ApplicationUser : IdentityUser
	{
		public string DisplayName { get; set; } = null!;

        public Address Address { get; set; }

        public ApplicationUser()
        {
            
        }

    }
}
