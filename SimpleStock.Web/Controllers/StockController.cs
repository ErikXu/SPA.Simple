using System.Web.Http;
using SimpleStock.Common.Entities;
using SimpleStock.Common.Mongo;
using SimpleStock.Web.Models;

namespace SimpleStock.Web.Controllers
{
    [RoutePrefix("api/stock")]
    public class StockController : ApiController
    {
        private readonly MongoRepository<Stock> _stockRepository;

        public StockController()
        {
            _stockRepository = new MongoRepository<Stock>();
        }

        [HttpGet]
        [Route("list")]
        public PagedResult<Stock> List([FromUri]PagedFilter filter)
        {
            var query = _stockRepository.AsQueryable();
            return PagedResult<Stock>.From(query, filter);
        }
    }
}
