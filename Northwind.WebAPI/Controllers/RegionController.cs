using Microsoft.AspNetCore.Mvc;
using Northwind.Contract.Models;
using Northwind.Domain.Base;
using Northwind.Services.Abstraction;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Northwind.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;

        public RegionController(IRepositoryManager repositoryManager, ILoggerManager logger)
        {
            this._repositoryManager = repositoryManager;
            _logger = logger;
        }

        // GET: api/<RegionController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var regions = _repositoryManager.RegionRepository.FindAllRegion().ToList();

                //use DTO
                var regionDTO = regions.Select(r => new RegionDto()
                {
                    RegionId = r.RegionId,
                    RegionDescription = r.RegionDescription,
                });

                return Ok(regionDTO);
            }
            catch (Exception)
            {
                _logger.LogError($"Error : {nameof(Get)}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // GET api/<RegionController>/5
        [HttpGet("{id}")]
        public IActionResult GetByID(int id)
        {
            var region = _repositoryManager.RegionRepository.FindRegionByID(id);
            if (region == null)
            {
                _logger.LogError("Region Data Not Found");
                return BadRequest("Region Not Found");
            }
            var regionDTO = new RegionDto
            {
                RegionId = region.RegionId,
                RegionDescription = region.RegionDescription,
            };

            return Ok(regionDTO);
        }

        // POST api/<RegionController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<RegionController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RegionController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}