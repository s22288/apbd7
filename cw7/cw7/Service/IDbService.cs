using cw7.Models;
using cw7.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw7.Service
{
   public interface IDbService
    {
        Task<IEnumerable<SomesortOfTrip>> GetTrips();

        Task RemoveTrip(int id);
        Task<Boolean> checkifPeselExists(string pesel);
        Task<Boolean> czyzapisany(Client client, int tripId);
        Task<Boolean> ifTripExist(int idTrip);

        Task addTripToClient(Client client ,int trip);  
    }
}
