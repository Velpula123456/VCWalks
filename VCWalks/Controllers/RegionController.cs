using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VCWalks.API.Models.DTO;
using VCWalks.CustomActionFilters;
using VCWalks.DataCollection;
using VCWalks.Models.Domain;
using VCWalks.Models.DTO;
using VCWalks.Repository;



namespace VCWalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RegionController : ControllerBase
    {
        private readonly VCWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionController(VCWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        //Get All Regions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Get data from Database - Domian models
            var regionsDomain = await regionRepository.GetAllAsync();
            //Map from  Domain Model to DTOs
            //Return DTOs
                return Ok(mapper.Map<List<RegionDTO>>(regionsDomain));

        }


        // GET SINGLE REGION (Get Region By ID)
        // GET: https://localhost:portnumber/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //Get data from Database - Domian models
            var regionsDomain = await regionRepository.GetByIdAsync(id);

            if (regionsDomain == null)
            {
                return NotFound();
            }
            //Map from  Domain Model to  Region DTOs
            var regionsDTO = new RegionDTO
            {
                Id = regionsDomain.Id,
                Code = regionsDomain.Code,
                Name = regionsDomain.Name,
                RegionImageUrl = regionsDomain.RegionImageUrl,
            };

            //Return back to client
            return Ok(regionsDTO);
        }


        // POST To Create New Region
        // POST: https://localhost:portnumber/api/regions
        [HttpPost]
        [ValidationModel]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDTO addRegionRequestDTO)
        {
            
                // Map or Convert From DTO  to Domain Model
                var regionDomainModel = new Region
                {
                    Code = addRegionRequestDTO.Code,
                    Name = addRegionRequestDTO.Name,
                    RegionImageUrl = addRegionRequestDTO.RegionImageUrl,
                };

                //Use domain model to create a region
                regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);


                //Back to DTO
                var regionDTO = new RegionDTO
                {
                    Id = regionDomainModel.Id,
                    Code = regionDomainModel.Code,
                    Name = regionDomainModel.Name,
                    RegionImageUrl = regionDomainModel.RegionImageUrl,
                };
                return CreatedAtRoute(nameof(GetById), new { id = regionDTO.Id }, regionDTO);
           
        }

        // Update region
        // PUT: https://localhost:portnumber/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidationModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDTO updateRegionRequestDTO)
        {
            //Map DTO to Domain
            var regionDomainmodel = new Region
            {
                Code = updateRegionRequestDTO.Code,
                Name = updateRegionRequestDTO.Name,
                RegionImageUrl = updateRegionRequestDTO.RegionImageUrl
            };
            //Check if region is exists
            regionDomainmodel = await regionRepository.UpdateAsync(id, regionDomainmodel);
            if (regionDomainmodel == null)
            {
                return NotFound();
            }

            //Convert Domain to DTO
            var regionDto = new RegionDTO
            {
                Id = regionDomainmodel.Id,
                Code = regionDomainmodel.Code,
                Name = regionDomainmodel.Name,
                RegionImageUrl = regionDomainmodel.RegionImageUrl,
            };
            return Ok(regionDto);
        }


        // Delete Region
        // DELETE: https://localhost:portnumber/api/regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
           var regionDomainmodel = await regionRepository.DeleteAsync(id);

            if (regionDomainmodel == null)
            {
                return NotFound();
            }

            
            //return the deleted region back
            //Map Domain model to DTO
            var regionDto = new RegionDTO
            {
                Id = regionDomainmodel.Id,
                Code = regionDomainmodel.Code,
                Name = regionDomainmodel.Name,
                RegionImageUrl = regionDomainmodel.RegionImageUrl,
            };
            return Ok(regionDto);

            
        }

    }

}
