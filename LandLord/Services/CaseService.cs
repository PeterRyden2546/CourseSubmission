using LandLord.Contexts;
using LandLord.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LandLord.Services;

internal class CaseService
{
    private readonly DataContext _context = new();

    public async Task CreateCaseAsync(CaseEntity entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<CaseEntity>> GetAllCaseAsync()
    {
        return await _context.Cases
            .Include(x => x.User)
            .Include(x => x.Status)
            .OrderByDescending(x => x.Created)
            .ToListAsync();
    }

    public async Task<IEnumerable<CaseEntity>> GetAllActiveCasesAsync()
    {
        return await _context.Cases
            .Include(x => x.User)
            .Include(x => x.Comments)
            .Include(x => x.Status)
            .Where(x => x.UserId != 4)
            .OrderByDescending(x => x.Created)
            .ToListAsync();
    }

    public async Task<CaseEntity> GetACaseAsync(Expression<Func<CaseEntity, bool>> predicate)
    {
        var _entity = await _context.Cases
            .FirstOrDefaultAsync(predicate);
        return _entity!;
    }

    public async Task<CaseEntity> UpdateStatusOnCaseAsync(int caseId, int statusid)
    {
        var _entity = await _context.Cases.FindAsync(caseId);
        if (_entity != null)
        {
            _entity.UpdateAt = DateTime.UtcNow;
            _entity.StatusId = statusid;
            _context.Update(_entity);
            await _context.SaveChangesAsync();
            
            return _entity;
        }

        return null!;
    }
}
