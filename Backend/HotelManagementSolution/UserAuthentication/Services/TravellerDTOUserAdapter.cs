using System.Security.Cryptography;
using System.Text;
using UserAuthentication.Interfaces;
using UserAuthentication.Models;
using UserAuthentication.Models.DTO;

namespace UserAuthentication.Services
{
    public class TravellerDTOUserAdapter : ITravellerDTOUserAdapter
    {
        public User GetUserFromTravellerDTOAsync(TravellerDTO travellerDTO)
        {
            if (travellerDTO.Password != null)
            {
                var hmac = new HMACSHA512();
                travellerDTO.User ??= new User(); // Create a new User object if it's null
                travellerDTO.User.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(travellerDTO.Password));
                travellerDTO.User.PasswordKey = hmac.Key;
                travellerDTO.User.Role = "traveller";
                return travellerDTO.User;
            }
            else
            {
                throw new Exception("TravellerDTO password is null.");
            }
        }
    }
}
