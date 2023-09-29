using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SampleProject.Database.Entity;
[Table("Contact")]
public class Contact
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Range(101110, long.MaxValue)]
    public long Id { get; set; }

    [Required]
    [MaxLength(32)]
    public string FirstName { get; set; }

    [MaxLength(32)]
    public string LastName { get; set; }

    [Required]
    public string Gender { get; set; }

    [MaxLength(64)]
    [EmailAddress]
    [Required]

    public string Email { get; set; }

    [Phone]
    public string PhoneNumber { get; set; }

    [Required]
    public string Reason { get; set; }

    [Required]
    public string Body { get; set; }
}
