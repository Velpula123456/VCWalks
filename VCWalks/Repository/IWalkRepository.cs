using Microsoft.EntityFrameworkCore;
using VCWalks.Models.Domain;

namespace VCWalks.Repository
{
    public interface IWalkRepository
    {
        Task<Walk> CreateAsync(Walk walk);
        Task<List<Walk>> GetAllAsync(string? filterOn, string? filterQuery = null, string? sortBy=null,
            bool isAscending= true);

        Task<Walk?> GetByIdAsync(Guid id);

        Task<Walk?>UpdateAsync(Guid id,  Walk walk);

        Task<Walk?> DeleteAsync(Guid id);

       
    }
}
