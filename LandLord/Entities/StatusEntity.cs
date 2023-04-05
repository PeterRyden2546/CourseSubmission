using System.ComponentModel.DataAnnotations;

namespace LandLord.Entities;

internal class StatusEntity
{
    [Key]
    public int Id { get; set; }

    [StringLength(20)]
    public string StatusCode { get; set; } = null!;

    public ICollection<CaseEntity> Cases = new HashSet<CaseEntity>();
}
