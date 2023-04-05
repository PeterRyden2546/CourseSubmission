using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LandLord.Entities;

internal class CommentEntity
{
    [Key]
    public int Id { get; set; }

    [StringLength(1500)]
    public string Comment { get; set; } = null!;

    [StringLength(50)]
    public string Author { get; set; } = null!;

    public DateTime Created { get; set; } = DateTime.UtcNow;

    public int CaseId { get; set; }
    public CaseEntity Case { get; set; } = null!;
}
