using UserAuthentication.Models;
using UserAuthentication.Models.DTO;

namespace UserAuthentication.Interfaces
{
    public interface ITravellerDTOUserAdapter
    {
        public User GetUserFromTravellerDTOAsync(TravellerDTO travellerDTO);
    }
}
