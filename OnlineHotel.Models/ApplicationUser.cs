using Microsoft.AspNetCore.Identity;

namespace OnlineHotel.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string  Name { get; set; }
        public string?  City { get; set; }
        public string?  PinCode { get; set; }
    }
}