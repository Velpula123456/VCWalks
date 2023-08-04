using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VCWalks.CustomActionFilters;
using VCWalks.DataCollection;
using VCWalks.Models.Domain;
using VCWalks.Models.DTO;
using VCWalks.Repository;

namespace VCWalks.Controllers
{
    //  /Api /walks
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly VCWalksDbContext dbContext;
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;

        public WalksController(VCWalksDbContext dbContext, IWalkRepository walkRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.walkRepository = walkRepository;
            this.mapper = mapper;
        }
        // CREATE Walk
        //Post:/Api/Walks
        [HttpPost]
        [ValidationModel]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDTO addWalkRequestDTO)
        {
            
                //Map DTO to Domain
                var walkDomainModel = mapper.Map<Walk>(addWalkRequestDTO);

                await walkRepository.CreateAsync(walkDomainModel);
                //Map Domain to DTO

                return Ok(mapper.Map<WalkDTO>(walkDomainModel));
            
        }

        [HttpGet]
        public async Task<IActionResult> GetAll ([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending)
        {
            var walkDomainModel = await walkRepository.GetAllAsync(filterOn, filterQuery,sortBy,isAscending ?? true);
            //Map Domain Model to DTO
            return Ok(mapper.Map<List<WalkDTO>>(walkDomainModel));
        }


        //Get Walk by Id
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepository.GetByIdAsync(id);

            if (walkDomainModel == null)
            {
                return NotFound();
            }
            //Map Domain Model to DTO
            return Ok(mapper.Map<WalkDTO>(walkDomainModel));
        }

        //Update Walk
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidationModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateWalkRequestDTO updateWalkRequestDTO)
        {
               //Map DTO to Domain
                var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDTO);

                await walkRepository.UpdateAsync(id, walkDomainModel);

                if (walkDomainModel == null)
                {
                    return NotFound();
                }
                //Map Domain Model to DTO
                return Ok(mapper.Map<WalkDTO>(walkDomainModel));

        }

        //Delete walk

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deletedWalkDomainModel = await walkRepository.DeleteAsync(id);
            if (deletedWalkDomainModel == null)
            {
                return NotFound();
            }

            //Map Domain Model to DTO
            return Ok(mapper.Map<WalkDTO>(deletedWalkDomainModel));
        }
    }
}
