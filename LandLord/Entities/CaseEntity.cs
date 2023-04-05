using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LandLord.Entities;

internal class CaseEntity
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string FirstName { get; set; } = null!;

    [StringLength(50)]
    public string LastName { get; set; } = null!;

    [Column(TypeName = "char(13)")]
    public string? PhoneNumber { get; set; }

    [Column(TypeName = "varchar(70)")]
    public string Email { get; set; } = null!;

    [StringLength(2000)]
    public string Description { get; set; } = null!;

    public DateTime Created { get; set; } = DateTime.UtcNow;

    public DateTime UpdateAt { get; set; } = DateTime.UtcNow;

    public int StatusId { get; set; } = 1;
    public StatusEntity Status { get; set; } = null!;

    public int UserId { get; set; }
    public UserEntity User { get; set; } = null!;

    public ICollection<CommentEntity> Comments = new HashSet<CommentEntity>();
}
