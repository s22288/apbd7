using cw7.Models;
using cw7.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw7.Controllers
{
    [Route("api/trips")]
    [ApiController]
    public class ValuesController : ControllerBase 
    {

        private readonly IDbService _dbService;

        public ValuesController(IDbService dbService)
        {
            _dbService = dbService;
        }
        [HttpGet]
        public async Task<IActionResult> GetTrips()
        {
            var trips = await _dbService.GetTrips();
            trips.OrderBy(o => o.DateTo);
            return Ok(trips);
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteTrip(int id)
        {
            await _dbService.RemoveTrip(id);
            return Ok("remove trip with id " + id);
        }

        [HttpPost]
        [Route("{idTrip}")]
        public async Task<IActionResult> AddClientToTrip(int idTrip,Client client)
        {

            var pesExist = _dbService.checkifPeselExists(client.Pesel).Result;
            var wycieczka = _dbService.ifTripExist(idTrip).Result;
            var czyzapisany = _dbService.czyzapisany(client, idTrip);
            if (pesExist == false)
            {
                return BadRequest("nie istnieje taki pesel");
            }
            if (pesExist == true)
            {
                return BadRequest("klient jest juz zapisany");
            }
            if (wycieczka == false)
            {
                return BadRequest("wycieczka nie istnieje");
            }
            _dbService.addTripToClient(client, idTrip);



            return Ok("update client with trip ");
        }
    }
}
