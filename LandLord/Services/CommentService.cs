using LandLord.Contexts;
using LandLord.Entities;
using Microsoft.EntityFrameworkCore;

namespace LandLord.Services;

internal class CommentService
{
    public readonly DataContext _context = new();
    private readonly CaseService _caseService = new();

    public async Task CreateCommentAsync(CommentEntity entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
        if (entity.CaseId == 1) 
        {
            await _caseService.UpdateStatusOnCaseAsync(entity.CaseId, 2);
        }
        
    }

    public async Task<IEnumerable<CommentEntity>> GetAllAsync(int caseId)
    {
        return await _context.Comments
            .Where(x => x.CaseId == caseId)
            .OrderByDescending(x => x.Created)
            .ToListAsync();
    }
}
