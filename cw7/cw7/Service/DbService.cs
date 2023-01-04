using cw7.Models;
using cw7.Models.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw7.Service
{
    public class DbService : IDbService
    {
        private readonly _2019SBDContext _dbContext;

        public DbService(_2019SBDContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task addTripToClient(Client client, int trip)
        {

            var attach = new ClientTrip { IdClient = client.IdClient, IdTrip = trip };
            _dbContext.Attach(attach);
            _dbContext.SaveChangesAsync();
        }

        public async Task<Boolean> checkifPeselExists(string pesel)
        {
            var exist = _dbContext.Clients.Where(e => e.Pesel ==pesel).FirstOrDefaultAsync();
            if (exist == null)
            {
                return false;
            }
            return true;

        }

     

        public async Task<bool> czyzapisany(Client client, int tripId)
        {
            var exist = _dbContext.CountryTrips.Where(e => e.IdTrip == tripId && e.clientId == client.IdClient).FirstOrDefault();
            if (exist == null)
            {
                return false;
            }
            return true;
        }

        public async Task<IEnumerable<SomesortOfTrip>> GetTrips()

        {
            return await _dbContext.Trips.Select(e => new SomesortOfTrip
            {
                Name = e.Name,
                Description = e.Description,
                MaxPeople = e.MaxPeople,
                DateFrom = e.DateFrom,
                DateTo = e.DateTo,
                Countries = e.CountryTrips.Select(e => new SomeSortOfCountry
                {
                    Name = e.IdCountryNavigation.Name
                }).ToList(),
                Clients = e.ClientTrips.Select(e => new SomeSortOfClient { FirstName = e.IdClientNavigation.FirstName, LastName = e.IdClientNavigation.LastName }).ToList()
            }).ToListAsync();
           
        }

        public Task<bool> ifTripExist(int idTrip)
        {
            var exist = _dbContext.Trips.Where(e => e.IdTrip == idTrip).FirstOrDefaultAsync();
            if (exist == null)
            {
                return false;
            }
            return true;
        }

        public async Task RemoveTrip(int id)
        {
           var trip =  _dbContext.Trips.Where(e => e.IdTrip == id).FirstOrDefaultAsync();

            _dbContext.Remove(trip);
            await _dbContext.SaveChangesAsync();
        }
    }
}
