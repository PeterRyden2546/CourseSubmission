using LandLord.Contexts;
using LandLord.Entities;
using Microsoft.EntityFrameworkCore;

namespace LandLord.Services;

internal class StatusService
{
    private readonly DataContext _context = new();

    public async Task AddingStatusCodeToDbAsync()
    {
        if(!await _context.Statuses.AnyAsync())
        {
            string[] _statuses = new string[] {"Ej påbörjad", "Pågående", "Väntar delar", "Avslutad"};

            foreach(var status in _statuses) 
            {
                await _context.AddAsync(new StatusEntity { StatusCode = status });
                await _context.SaveChangesAsync();
            }
        }
    }
}
