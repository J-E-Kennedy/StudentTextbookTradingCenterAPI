using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SttcBookTrade.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SttcBookTrade.Controllers
{
    [Route("api/trades")]
    public class TradesController : Controller
    {
        private IBookTradeRepository _bookTradeRepository;

        public TradesController(IBookTradeRepository bookTradeRepository)
        {
            _bookTradeRepository = bookTradeRepository;
        }

        [HttpGet()]
        public IActionResult GetTrades()
        {
            var tradeEntities = _bookTradeRepository.GetTrades();
            var results = Mapper.Map<IEnumerable<TradesController>>(tradeEntities);

            return Ok(results);
        }

        //[HttpPost()]
        //public IActionResult CreateTrade()
        //{
            
        //}

    }
}
