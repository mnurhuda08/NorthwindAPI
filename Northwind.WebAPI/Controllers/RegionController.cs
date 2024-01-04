using Microsoft.AspNetCore.Mvc;
using Northwind.Contract.Models;
using Northwind.Domain.Base;
using Northwind.Domain.Entities;
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
        [HttpGet("{id}", Name = "GetRegionByID")]
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
        public IActionResult Create([FromBody] RegionDto regionDto)
        {
            //check region DTO null
            if (regionDto == null)
            {
                _logger.LogError("RegionDTO is Null");
                return BadRequest("RegionDTO is Null");
            }
            var region = new Region
            {
                RegionId = regionDto.RegionId,
                RegionDescription = regionDto.RegionDescription
            };

            //post to db
            _repositoryManager.RegionRepository.Insert(region);

            //get inserted data
            return CreatedAtRoute("GetRegionByID", new { id = regionDto.RegionId }, regionDto);
        }

        // PUT api/<RegionController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] RegionDto regionDto)
        {
            if (regionDto == null)
            {
                _logger.LogError("Region Not Found");
                return BadRequest("region data not found");
            }
            var region = new Region
            {
                RegionId = id,
                RegionDescription = regionDto.RegionDescription
            };

            _repositoryManager.RegionRepository.Edit(region);

            return CreatedAtRoute(
                "GetRegionByID",
                new { id = regionDto.RegionId },
                new RegionDto { RegionId = id, RegionDescription = region.RegionDescription }
            );
        }

        // DELETE api/<RegionController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                _logger.LogError("ID Not Found");
                return BadRequest("ID Not Found");
            }

            var region = _repositoryManager.RegionRepository.FindRegionByID(id.Value);
            if (region == null)
            {
                _logger.LogError($"Region With Id : {id} Not Found");
                return NotFound();
            }

            _repositoryManager.RegionRepository.Remove(region);
            return Ok("Remove Data Success");
        }
    }
}