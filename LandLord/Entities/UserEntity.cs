using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LandLord.Entities;

[Index(nameof(Email), IsUnique = true)]
internal class UserEntity
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

    public ICollection<CaseEntity> Cases = new HashSet<CaseEntity>();
}
